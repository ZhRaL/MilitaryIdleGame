using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditTextScript : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text tx;
    public RecruitTemplate template;
    
    public void EditName()
    {
        inputField.gameObject.SetActive(true);
        inputField.text = tx.text;

        inputField.onSubmit.AddListener(Change);
    }

    public void Change(string text)
    {
        tx.text = text;
        template.ChangeSoldierName(text);
        inputField.gameObject.SetActive(false);
    }
}
