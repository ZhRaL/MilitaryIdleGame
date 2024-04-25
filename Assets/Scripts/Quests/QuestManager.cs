using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<Quest> Quests=new();

    private List<int> activeQuestIds=new();
    
    // Start is called before the first frame update
    void Start()
    {
        Quests = new QuestGenerator().GetAllQuests();
        Initialize();
    }

    private void Initialize()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isComplete(Quest quest)
    {
        IController controller = GameManager.INSTANCE.GetTopLevel(quest.Requirement.reqObject);
        IManageItems manager = controller.GetManager(quest.Requirement.reqObject.DefenseType);
        
        switch(quest.Requirement.ReqType) {
            case AMOUNT => return manager.GetAmountOfUnlockedItems() >= quest.Requirement.Amount;
            case LEVEL => return manager.GetHighestLevel() >= quest.Requirement.Amount;
            case AMOUNT => {
                bool amount = manager.GetAmountOfUnlockedItems() >= quest.Requirement.Amount;
                

            }
            


        }
        return false;
    }
    
}
