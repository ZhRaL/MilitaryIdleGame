using System;
using System.Collections;
using System.Collections.Generic;
using Tech_Tree;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Transform ConnectionParent;

    public GameObject ConnectionPrefab;
    
    public Transform SkillsParent;

    public List<Skill> UnlockedSkills;
    
    public SkillSelector Selector;

    public Action OnSkillUnlocked;

    void Start()
    {
        UnlockedSkills = new();
        // Load from Playerprefs;
        // change Color from Corner and Base Img or so for all Unlocked Skills!
        
        ConnectAll();
    }

    public void TryUnlock(Skill skill)
    {
        Skill req = skill.RequirementSkill;
        if (req != null)
        {
            if (UnlockedSkills.Contains(req))
            {
                UnlockedSkills.Add(skill);
                skill.Unlocked = true;
                OnSkillUnlocked?.Invoke();
            }
        }
    }

    public void Select(Skill skill)
    {
        Selector.Select(skill);
    }

    private void ConnectAll()
    {
        foreach (Transform child in SkillsParent)
        {
            Button button = child.gameObject.AddComponent<Button>();
            Skill skill = child.GetComponent<Skill>();
            button.onClick.AddListener(() => Select(skill));
            
            Skill req = child.GetComponent<Skill>().RequirementSkill;
            if (req != null)
            {
                Connect(child.transform,req.transform);
            }

        }
    }

    public void Connect(Transform pointA, Transform pointB)
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
    }

[Serializable]
    public class SkillType
    {
        public string Title;
        public string Description;
        public int Level;
        private float amount;
    }
}
