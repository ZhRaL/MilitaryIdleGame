using System.Collections;
using System.Collections.Generic;
using Tech_Tree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelector : MonoBehaviour
{
    public Image UnlockableButton, LockedButton;
    
    public Image Icon, Unlock;
    public TMP_Text Title, Cost, Description;

    public void Select(Skill skill)
    {
        Icon.sprite = skill.Icon.sprite;
        Title.text = skill.Type.Title;
        Cost.text = string.Format(Cost.text, skill.Cost);
        Description.text = skill.Type.Description;
    }
}
