using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughResearchPoints : MonoBehaviour
{
    public AnimationClip popupAnimation;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null && popupAnimation != null)
        {
            animator.Play(popupAnimation.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
