using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterController : MonoBehaviour
{
    public Material filterMaterial;

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

    public void SetWhiteColor()
    {
        ChangeLightColor(Color.white);
    }


    private void ChangeLightColor(Color color)
    {
        filterMaterial.SetColor("_LightColor", color);
    }
}
