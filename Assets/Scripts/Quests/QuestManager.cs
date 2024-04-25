using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<QuestModel> Quests=new();

    private List<int> activeQuestIds=new();

    public GameObject QuestPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        Quests = new QuestGenerator().GetAllQuests();
        Initialize();
    }

    private void Initialize()
    {
        
    }

    private void InitQuest(int id) {
        GameObject go = Instantiate(QuestPrefab);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteQuest(int id) {
        Quest quest = Quests.Where(x=>x.id == id);
        if(!isComplete(quest)) 
            throw new ArgumentException("Quest is not Completed!");
        
        Reward();
        Destroy(quest.gameObject);
           
        
    }

    private void Reward(int amount) {
        GameManager.INSTANCE.Badges += amount;
    }

    private bool isComplete(QuestModel quest)
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
