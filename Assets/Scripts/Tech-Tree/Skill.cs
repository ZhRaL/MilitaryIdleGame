using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tech_Tree
{
    public class Skill : MonoBehaviour
    {
        public Image Icon;
        public Image BaseImg;
        public Image ConnectionImage;
        public Image Corner;
        
        public int Cost;
        public bool Unlocked;
        
        public Skill RequirementSkill;
        public SkillManager.SkillType Type;
        public int SkillId;

        private void Awake()
        {
            SkillId = transform.GetSiblingIndex();
        }

        public void Init()
        {
            
        }
    }
}