using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitImg : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
