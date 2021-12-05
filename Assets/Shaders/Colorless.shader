Shader "Unlit/Colorless"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorlessFactor ("ColorlessFactor", Range(0.0, 1.0)) = 1.0
        _LightColor("LightColor", Color) = (1, 1, 1, 0)
        _MousePos("MousePos", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ColorlessFactor;
            fixed4 _LightColor;
            fixed4 _MousePos;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float magnifierRadius = 0.1 * _MousePos.zw;

                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = i.color.rgb * col.rgb;

                fixed3 YPrime = fixed3(0.299, 0.5959, 0.2115);
                fixed3 intensity = fixed3(1, 1, 1) * dot(col, YPrime);
                fixed3 displayCol = lerp(intensity, col.rgb, _ColorlessFactor);

                float4 screenPos = ComputeScreenPos(i.vertex);
                float2 screenUV = screenPos.xy / screenPos.w;

                // Flip and scale by 0.5 to account for NDC.
                float2 mouseCoords = _MousePos.xy * float2(1, -1) * 0.5;
                float distFromMouse = length(screenUV - mouseCoords);

                if (_LightColor.a > 0 && distFromMouse < magnifierRadius) {
                    displayCol *= _LightColor.rgb;
                }

                return fixed4(displayCol, col.a);
            }
            ENDCG
        }
    }
}
