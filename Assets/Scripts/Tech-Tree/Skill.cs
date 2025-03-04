using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

namespace Tech_Tree
{
    public class Skill : MonoBehaviour
    {
        public Image Icon;
        public Image BaseImg;
        public Image ConnectionImage;
        public Image Corner;

        public Sprite IconImage;
        
        public Color unlocked, pulseStart, pulseEnd;
        
        public int Cost;
        public bool Unlocked;
        
        public Skill RequirementSkill;
        public SkillManager.SkillType Type;
        public int SkillId;
        
        private bool isPulsing;
        public float duration = 5f;
        public float colorTime = 0f;
        private bool isReversing = false;
        private bool ScaleIsReversing = false;
        public float scaleDuration=3f;
        public float scaleTime = 0f;
        private float scaleStart=100, scaleEnd=110;
        private RectTransform rect;
        
        private void Awake()
        {
            SkillId = transform.GetSiblingIndex();
            rect = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if(isPulsing) Pulse();
        }

        private void Pulse()
        {
            colorTime += Time.deltaTime / duration;
            scaleTime += Time.deltaTime / scaleDuration;
            
            float t = isReversing ? 1 - colorTime : colorTime;
            BaseImg.color = Color.Lerp(pulseStart, pulseEnd, t);
            
            float tScale = ScaleIsReversing ? 1 - scaleTime : scaleTime;
            var sizeValue = Mathf.Lerp(scaleStart, scaleEnd,tScale);
            rect.sizeDelta = new Vector2(sizeValue, sizeValue);

            if (colorTime >= 1)
            {
                isReversing = !isReversing;
                colorTime = 0f;
            }

            if (scaleTime >= 1)
            {
                ScaleIsReversing = !ScaleIsReversing;
                scaleTime=0f;
            }
        }

        public void Unlock()
        {
            Corner.color = unlocked;
        }

        public void Activate()
        {
            isPulsing = true;
        }

        public void Deactivate()
        {
            isPulsing = false;
            colorTime = 0f;
            isReversing = false;
        }
        
    }
}