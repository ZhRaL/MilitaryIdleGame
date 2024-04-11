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
        
        var height = targetTransform.rect.height;
        height *= (ob.transform.GetSiblingIndex() / 2);
        
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, height);
        
        SetActive(ob);
    }

    public void SetActive(GameObject tf)
    {
        Hightlight(tf);

    }

    private void Hightlight(GameObject ob)
    {
        if (ActiveItem)
        {
            Image old = ActiveItem.GetComponent<Image>();
            old.color = savedHighlightColor;
        }
        
        Image ig = ob.GetComponent<Image>();
        savedHighlightColor = ig.color;
        ig.color = highlightColor;
        ActiveItem = ob;
    }

    public void OnValueChange(Vector2 value)
    {
        var height = (int) tuple[0].ItemContainer.rect.height -30;
        var sliderValue = value.y;

        // half height cause of the correct hightlighting of NavBar, inclluding height fo spearator
        var currentHeight = height/2 + content.anchoredPosition.y;
        Debug.Log($"Height is {height} and current ist {content.anchoredPosition}");
        var index = (int)((int) currentHeight / height);
        
        // Separator
        
        Hightlight(tuple[index].NavItem);
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
