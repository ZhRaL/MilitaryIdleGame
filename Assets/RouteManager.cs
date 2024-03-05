using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    private float length;

    public float getRouteLength()
    {
        return (length != null) ? length : calculate();
    }

    private float calculate()
    {
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
            print("For loop: " + transform.GetChild(i));
        
        return -1;
    }
}