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
        index++;
        if (index < TutorialComponents.Length)
        {
            TutorialComponents[index].StartDialogue(this);
        }
        // EndDialogue
    }

    public void ShowTutorial()
    {
        if(index < TutorialComponents.Length)
        TutorialComponents[index].StartDialogue(this);
    }
}