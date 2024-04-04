using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavBar : MonoBehaviour
{
    public MyTuple[] tuple;
    private GameObject ActiveItem;
    public Color highlightColor;
    private Color savedHighlightColor;
    public RectTransform content;

    private float pos1 = 0.3775112f;

    private void Start()
    {
        foreach (var navItem in tuple)
        {
            init(navItem.NavItem);
        }
    }

    private void init(GameObject ob)
    {
        Button btn = ob.AddComponent<Button>();
        btn.onClick.AddListener(() => ClickOnNavItem(ob));
    }

    private void ClickOnNavItem(GameObject ob)
    {
        RectTransform targetTransform = GetTransform(ob);
        
        // caluclate effective Height ?!
        float eff = 1338.313f;
        float contentPosY = Mathf.Lerp(0, eff, 1-pos1);
        
        // Aktualisieren Sie die vertikale Position des Inhalts
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, contentPosY);
        
        SetActive(ob);
    }

    public void SetActive(GameObject tf)
    {
        Hightlight(tf);
        ActiveItem = tf;
        
    }

    private void Hightlight(GameObject ob)
    {
        if (ActiveItem)
        {
            Image old = ActiveItem.GetComponent<Image>();
            old.color = savedHighlightColor;
        }
        
        Image ig = ob.GetComponent<Image>();
        ig.color = highlightColor;

    }

    private RectTransform GetTransform(GameObject ob)
    {
        foreach (var myTuple in tuple)
        {
            if (myTuple.NavItem == ob) return myTuple.ItemContainer;
        }

        return null;
    }

    [Serializable]
    public class MyTuple
    {
        public GameObject NavItem;
        public RectTransform ItemContainer;
    }
}
