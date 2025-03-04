using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using Tech_Tree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private string SKILL_SAVE_STRING = "UNLOCKED_SKILLS_LIST";
    private int _researchPoints;
    public TMP_Text CurrencyTx;
    
    public Transform ConnectionParent;

    public GameObject ConnectionPrefab;
    
    public Transform SkillsParent;

    public List<Skill> UnlockedSkills;
    
    public SkillSelector Selector;

    public Action OnSkillUnlocked;

    public Color connectionEstablishedColor;
    
    public int ResearchPoints
    {
        get => _researchPoints;
        set
        {
            _researchPoints = value;
            CurrencyTx.text = string.Format(CurrencyTx.text, _researchPoints);
        }
    }

    void Start()
    {
        UnlockedSkills = new();
        ResearchPoints = 0;
        
        OnSkillUnlocked += SaveUnlockedSkills;
        LoadUnlockedSkills();
        LoadUnlockableSkills();
        // Load from Playerprefs;   -> ID der Unlockten speichern
        // change Color from Corner and Base Img or so for all Unlocked Skills!
        
        ConnectAll();
    }

    private void LoadUnlockableSkills()
    {
        foreach (Skill skill in SkillsParent.GetComponentsInChildren<Skill>())
        {
            if(skill.Unlocked) 
                continue;
            
            if(skill.RequirementSkill.Unlocked=true)
                skill.Icon.sprite = skill.IconImage;
        }
    }

    private void LoadUnlockedSkills()
    {
        string s = PlayerPrefsHelper.GetString(SKILL_SAVE_STRING, "{\"list\":[0,2,13,19]}");
        IntListWrapper wrapper = JsonUtility.FromJson<IntListWrapper>(s);
        foreach (int id in wrapper.list)
        {
            Transform child = SkillsParent.GetChild(id);
            Skill skill = child.GetComponent<Skill>();
            if(skill!=null)
                UnlockSkill(skill);
        }
    }

    public void UnlockSkill(Skill skill)
    {
        UnlockedSkills.Add(skill);
        skill.Unlocked = true;
        skill.Icon.sprite = skill.IconImage;
    }

    private void SaveUnlockedSkills()
    {
        var list = UnlockedSkills.Select(skill => skill.SkillId).ToList();
        PlayerPrefsHelper.SetString(SKILL_SAVE_STRING,JsonUtility.ToJson(new IntListWrapper(list)));
    }

    public SkillState GetState(Skill skill)
    {
        if (UnlockedSkills.Contains(skill)) return SkillState.UNLOCKED;
        if (UnlockedSkills.Contains(skill.RequirementSkill)) return SkillState.UNLOCKABLE;
        return SkillState.LOCKED;
    }

    public bool TryUnlock(Skill skill)
    {
        Skill req = skill.RequirementSkill;
        if (req != null)
        {
            if (UnlockedSkills.Contains(req))
            {
                UnlockedSkills.Add(skill);
                skill.Unlocked = true;
                OnSkillUnlocked?.Invoke();
                return true;
            }
        }

        return false;
    }
    

    public void Select(Skill skill)
    {
        Selector.Select(skill);
    }

    private void ConnectAll()
    {
        foreach (Transform child in SkillsParent)
        {
            Skill skill = child.GetComponent<Skill>();
            if(skill==null) 
                continue;
            
            Button button = child.gameObject.AddComponent<Button>();
            button.onClick.AddListener(() => Select(skill));
            
            Skill req = skill.RequirementSkill;
            if (req != null)
            {
                var line = Connect(child.transform,req.transform);
                if (skill.Unlocked)
                {
                    Image img = line.GetComponent<Image>();
                    img.color = connectionEstablishedColor;
                    Rect r = new Rect(img.rectTransform.rect);
                    r.height *= 2;
                    img.rectTransform.sizeDelta = new Vector2(r.width, r.height);
                    // line.GetComponent<Outline>().effectColor = Color.white;
                    line.GetComponent<Outline>().enabled = true;
                }
                    
            }

        }
    }

    public GameObject Connect(Transform pointA, Transform pointB)
    {
        // Instanziere die Linie
        GameObject line = Instantiate(ConnectionPrefab, ConnectionParent);

        // Berechne die Mitte zwischen den beiden Punkten
        Vector3 midPoint = (pointA.position + pointB.position) / 2;

        // Setze die Position der Linie
        RectTransform rectTransform = line.GetComponent<RectTransform>();
        rectTransform.position = midPoint;

        // Berechne die Distanz zwischen den beiden Punkten
        float distance = Vector3.Distance(pointA.localPosition, pointB.localPosition);

        // Setze die Länge der Linie (nur auf der x-Achse skalieren)
         rectTransform.sizeDelta = new Vector2(distance, rectTransform.sizeDelta.y);

        // Berechne den Winkel der Linie
        Vector3 direction = pointB.position - pointA.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Setze die Rotation der Linie
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        
        return line;
    }

    [Serializable]
    public class SkillType
    {
        public string Title;
        public string Description;
        public int Level;
        private float amount;
    }

    public enum SkillState
    {
        UNLOCKED, LOCKED, UNLOCKABLE
    }
    private class IntListWrapper
    {
        public List<int> list;

        public IntListWrapper(List<int> list)
        {
            this.list = list;
        }
    }
}
