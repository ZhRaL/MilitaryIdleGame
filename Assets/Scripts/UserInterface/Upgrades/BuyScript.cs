using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class BuyScript : MonoBehaviour
{
    public Button button;
    public TMP_Text title, description, cost;
    public Image icon;
    public GameObject Screen;
    public TMP_InputField inputField;

    
    private void Start()
    {
        GameManager.INSTANCE.OnMoneyChanged += checkBalance;
    }
    
    private void checkBalance()
    {
        if (GameManager.INSTANCE.Gold > 10)
            button.interactable = true;
        else button.interactable = false;
    }
    
    public void Init(UpgradeDto dto,DefenseType type)
    {
        title.text = dto.title;
        description.text = dto.description;
        cost.text = dto.upgradeCost.ToString();
        icon.sprite = dto.Icon;

        
        if (GameManager.INSTANCE.Gold < dto.upgradeCost)
        {
            button.interactable = false;
            button.onClick.RemoveAllListeners();
        }
        else
        {
            button.interactable = true;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => GameManager.INSTANCE.Gold -= dto.upgradeCost);
            button.onClick.AddListener(() => SoldierController.INSTANCE.GetPlatoon(type).createSoldier(1,1,1, inputField.text));
            button.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                Screen.SetActive(false);
                Screen.SetActive(true);
            });
        }
    }
    
    
}
