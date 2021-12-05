using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterController : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light;

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


    private void ChangeLightColor(Color color)
    {
        light.color = color;
    }
}
