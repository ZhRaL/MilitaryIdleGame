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


        public void Init()
        {
            
        }
    }
}