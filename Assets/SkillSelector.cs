using System;
using System.Collections;
using System.Collections.Generic;
using Tech_Tree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelector : MonoBehaviour
{
    public SkillManager SkillManager;
    public Image UnlockableButton, LockedButton, UnlockedButton;
    
    public Image Icon;
    public Image buttonImg;
    public TMP_Text Title, Cost, Description, ButtonText;
    public Button button;
    private Skill CurrentSkill;

    public void Select(Skill skill)
    {
        CurrentSkill?.Deactivate();
        skill.Activate();
        CurrentSkill = skill;
        Icon.sprite = skill.Icon.sprite;
        Title.text = skill.Type.Title;
        Cost.text = string.Format(Cost.text, skill.Cost);
        Description.text = skill.Type.Description;
        
        button.onClick.RemoveAllListeners();
        button.interactable = true;
        
        switch (SkillManager.GetState(skill))
        {
            case SkillManager.SkillState.UNLOCKED:
                InitUnlocked();
                break;
            case SkillManager.SkillState.LOCKED:
                InitLocked();
                break;
            case SkillManager.SkillState.UNLOCKABLE:
                InitUnlockable();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void InitUnlockable()
    {
        buttonImg.sprite = UnlockableButton.sprite;
        ButtonText.text = "UNLOCK";
        button.onClick.AddListener(() => SkillManager.TryUnlock(CurrentSkill));
    }
    
    private void InitUnlocked()
    {
        button.interactable = false;
        ButtonText.text = "UNLOCKED";
    }
    
    private void InitLocked()
    {
        button.interactable = false;
        buttonImg.sprite = LockedButton.sprite;
        ButtonText.text = "LOCKED";
    }
}
