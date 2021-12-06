using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterController : MonoBehaviour
{
    public Material filterMaterial;

    private static FilterController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void SetGreenFilter()
    {
        ChangeLightColor(Color.green);
    }

    public void SetRedFilter()
    {
        ChangeLightColor(Color.red);
    }
    public void SetBlueFilter()
    {
        ChangeLightColor(Color.blue);
    }

    public void SetClearColor()
    {
        ChangeLightColor(new Color(1, 1, 1, 0));
    }

    private void ChangeLightColor(Color color)
    {
        filterMaterial.SetColor("_LightColor", color);
    }

    public void SetColorlessFactor(float factor)
    {
        filterMaterial.SetFloat("_ColorlessFactor", factor);
    }

    private void Update()
    {
        float mouseRatioX = Input.mousePosition.x;
        float mouseRatioY = Input.mousePosition.y;

        var mousePos = new Vector4(mouseRatioX, mouseRatioY,
            Screen.width, Screen.height);

        filterMaterial.SetVector("_MousePos", mousePos);
    }
}
