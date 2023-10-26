using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Clickdetector : MonoBehaviour
{

    private void OnMouseDown()
    {
         logger.log("Clicked on: " + gameObject.name);
    }
}
