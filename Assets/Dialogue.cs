using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    public TMP_Text textComponent;
    public string[] lines;
    private float textSpeed;

    private int index;
    public UnityAction OnDialogueEnded;
    private float lineDelay = .5f;
    private float clickCounter=0f;

    // Update is called once per frame
    void Update()
    {
        clickCounter += Time.deltaTime;
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && clickCounter > lineDelay)
        {
            clickCounter = 0;
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue(float textSpeed)
    {
        this.textSpeed = textSpeed;
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            OnDialogueEnded?.Invoke();
        }
    }
}