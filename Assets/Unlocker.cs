using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour
{
    public GameObject toUnlock;

    public void unlock()
    {
        transform.gameObject.SetActive(false);
        toUnlock.SetActive(true);
    }
}
