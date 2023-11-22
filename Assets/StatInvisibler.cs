using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatInvisibler : MonoBehaviour
{
    public Image img;
    public TMP_Text tx1, tx2;

    void Start()
    {
        colorize();
    }

    public void colorize()
    {
        img.color = Color.clear;
        tx1.color = Color.clear;
        tx2.color = Color.clear;
    }
}