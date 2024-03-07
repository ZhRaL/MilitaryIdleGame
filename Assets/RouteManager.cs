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
        float sumDistance=0;

        for (int i = 0; i < transform.childCount -1; ++i) {
            Transform prev = transform.GetChild(i);
            Transform curr = transform.GetChild(i+1);
            float distance = Vector3.Distance(prev.position, curr.position);
            sumDistance +=distance;
        }
        sumDistance += Vector3.Distance(transform.GetChild(0), transform.GetChild(transform.childCount-1));
        
        return sumDistance;
    }
}
