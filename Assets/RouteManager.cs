using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    Transform armyRouter, airforceRouter, marineRouter

    public float getRouteLength(DefenseType type)
    {
        switch(type) {
            case ARMY: return calculate(armyRouter);
            case AIRFORCE: return calculate(airforceRouter);
            case MARINE: return calculate(marineRouter);
        }
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
        sumDistance += Vector3.Distance(router.GetChild(0), transform.router(transform.childCount-1));
        
        return sumDistance;
    }
}
