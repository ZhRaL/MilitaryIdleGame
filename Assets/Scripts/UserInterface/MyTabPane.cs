using System;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class MyTabPane : MonoBehaviour
{

    [SerializeField] private GameObject buttonParent;

    [SerializeField] private GameObject Screenparent;
    [SerializeField] private Image _image;

    private Button[] buttons;
    private Transform[] screens;
    
    private Transform currentScreen;
    private Button currentButton;

    private float inactiveButtonColorAlpha = .7f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (buttonParent.transform.childCount != Screenparent.transform.childCount)
            throw new ArgumentException("Amount of Buttons and Screens does not match!");
        
        buttons = buttonParent.transform.GetComponentsInChildren<Button>();
        screens = Screenparent.GetAllChildren().ToArray();

        currentScreen = screens[0];
        currentButton = buttons[0];

        hookButtons();
    }

    private void hookButtons()
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
            if (button != currentButton)
            {
                Image currImage = button.GetComponentInChildren<Image>();
                currImage.changeAlphaValue(inactiveButtonColorAlpha);
            }
            button.onClick.AddListener(() =>
            {
                currentScreen.gameObject.SetActive(false);
                currentButton.GetComponentInChildren<Image>().changeAlphaValue(inactiveButtonColorAlpha);
                currentButton.interactable = true;
                
                button.interactable = false;
                button.GetComponentInChildren<Image>().changeAlphaValue(1);
                
                Image _imgButton =  button.GetComponentInChildren<Image>();
                _image.color = _imgButton.color;

                currentScreen = screens[button.transform.GetSiblingIndex()];
                currentScreen.gameObject.SetActive(true);
                
                currentButton = button;
                
            });
        }
    }
    
}
