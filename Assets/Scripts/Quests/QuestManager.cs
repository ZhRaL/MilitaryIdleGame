using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<Quest> Quests;

    private List<int> activeQuestIds;
    
    // Start is called before the first frame update
    void Start()
    {
        Quests = new QuestGenerator().GetAllQuests();
        Initialize();
    }

    private void Initialize()
    {
      //  foreach (var quest in Quests)
      //  {
      //      
      //  }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isComplete(Quest quest)
    {
        GameManager.INSTANCE.GetTopLevel(quest.Requirement.reqObject);
        return false;
    }
    
}
