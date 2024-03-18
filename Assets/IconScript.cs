using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class IconScript : MonoBehaviour
{
    public Image iconImage, upgradeArrowImage;
    public TMP_Text tx_level;
    public Item Item;
    private protected GimmeAName Parent;

    private protected delegate void parentSelected(IconScript script);
    private protected delegate void parentDoubleSelected(IconScript script,bool isMoney);

    private protected parentSelected SingleSelect;
    private protected parentDoubleSelected DoubleSelect;

    public virtual void InitializePreview(GimmeAName parent, Item item)
    {
        Item = item;
        Parent = parent;

        IconProvider iconProvider = GameManager.INSTANCE.DataProvider.IconProvider;

        iconImage.sprite = iconProvider.GetIcon(item.ToUpgradeType());
        tx_level.text = "" + item.Level;

        if (Upgradable(item))
            upgradeArrowImage.gameObject.SetActive(true);
        else upgradeArrowImage.gameObject.SetActive(false);

        AddButton();
    }

    protected virtual bool Upgradable(Item item)
    {
        int costs = GameManager.INSTANCE.DataProvider.GetCost(item.ObjectType.defenseType, item.ObjectType, item.Index);
        return GameManager.INSTANCE.gold >= costs;
    }

    protected virtual void AddButton()
    {
        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(() => Parent.Selected(this));
        button.onClick.AddListener(highlightMe);
    }

    public void highlightMe()
    {
        HighLightManager.highlight(GetComponent<Image>());
    }
}