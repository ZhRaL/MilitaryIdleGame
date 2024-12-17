using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class TutorialComponent
{
    private TutorialManager _manager;
    private Dialogue _dialogue;
    public GameObject _gameObjectPrefab;
    private GameObject _gameObjectComponent;

    public string[] lines;
    
    public void DialogueComplete()
    {
        Object.Destroy(_gameObjectComponent);
        _manager.ShowNextDialogue();
    }

    public void StartDialogue(TutorialManager manager)
    {
        _manager = manager;
        _gameObjectComponent = Object.Instantiate(_gameObjectPrefab,_manager.rootCanvas);
        _gameObjectComponent.SetActive(true);
        
        _dialogue = _gameObjectComponent.GetComponentInChildren<Dialogue>();
        _dialogue.lines = lines;
        _dialogue.StartDialogue(_manager.textSpeed);
        _dialogue.OnDialogueEnded += DialogueComplete;
    }
    
}
