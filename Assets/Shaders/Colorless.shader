Shader "Unlit/Colorless"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorlessFactor ("ColorlessFactor", Range(0.0, 1.0)) = 1.0
        _FilterRadius("FilterRadius", Range(0.0, 1.0)) = 0.2
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
            float _FilterRadius;
            fixed4 _LightColor;
            float4 _MousePos;
            float4 _MouseOrientation;

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
                float magnifierRadius = _FilterRadius * _ScreenParams.xy;
                fixed4 texCol = tex2D(_MainTex, i.uv);
                fixed3 col = i.color.rgb * texCol.rgb;

                float2 orientation = _MouseOrientation.xy;

                float4 screenPos = ComputeScreenPos(i.vertex);

                float2 screenCoord = screenPos.xy / screenPos.w;

                // Scale by 2 and flip if needed to account for NDC.
                screenCoord *= 2;

                #if UNITY_UV_STARTS_AT_TOP
                screenCoord.y = _ScreenParams.y - screenCoord.y;
                #endif

                float2 mouseCoords = _MousePos.xy;
                float distFromMouse = length(screenCoord - mouseCoords);

                fixed3 displayCol;

                if (_LightColor.a > 0 && distFromMouse < magnifierRadius) {
                    col *= _LightColor.rgb;
                    displayCol = col;
                } else {
                    fixed3 intensity = fixed3(1, 1, 1) * Luminance(col);
                    displayCol = lerp(intensity, col, _ColorlessFactor);
                }

                return fixed4(displayCol, texCol.a);
            }
            ENDCG
        }
    }
}
