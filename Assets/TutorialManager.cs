using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public float textSpeed;
    public TutorialComponent[] TutorialComponents;
    public Transform rootCanvas;
    public int index = 0;

    public void ShowNextDialogue()
    {
        if (index < TutorialComponents.Length - 1)
        {
            TutorialComponents[++index].StartDialogue(this);
        }
        // EndDialogue
    }

    public void ShowTutorial()
    {
        TutorialComponents[index].StartDialogue(this);
    }
}