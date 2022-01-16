using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickdetector : MonoBehaviour
{

    private void OnMouseDown()
    {
        Debug.Log("Clicked on: " + gameObject.name);
    }
}
