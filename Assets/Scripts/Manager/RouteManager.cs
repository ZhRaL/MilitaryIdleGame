using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class RouteManager : MonoBehaviour
{
    private Transform armyRouter, airforceRouter, marineRouter;

    public float getRouteLength(DefenseType type)
    {
        return type switch
        {
            DefenseType.ARMY => calculate(armyRouter),
            DefenseType.AIRFORCE => calculate(airforceRouter),
            DefenseType.MARINE => calculate(marineRouter)
        };
    }

    private float calculate(Transform router)
    {
        float sumDistance=0;

        for (int i = 0; i < router.childCount -1; ++i) {
            Transform prev = router.GetChild(i);
            Transform curr = router.GetChild(i+1);
            float distance = Vector3.Distance(prev.position, curr.position);
            sumDistance +=distance;
        }
        sumDistance += Vector3.Distance(router.GetChild(0).position, 
            router.GetChild(router.transform.childCount-1).position);
        
        return sumDistance;
    }
}
