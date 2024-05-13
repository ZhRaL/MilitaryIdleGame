using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Interfaces;
using Quests;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<QuestModel> Quests = new();

    private List<int> activeQuestIds = new();

    public GameObject QuestPrefab;
    public GameObject questParent;
    public TMP_Text tx_redeemableQuest;

    // Start is called before the first frame update
    void Start()
    {
        Quests = new QuestGenerator().GetAllQuests();
        Initialize();
    }

    public void CheckRedeems()
    {
        int count = 0;
        foreach (var id in activeQuestIds)
        {
            var qm = GetQuestModel(id);
            if (qm.Requirement.isFulFilled()) count++;
        }

        setRedeemText(count);
    }

    private void setRedeemText(int set)
    {
        var parent = tx_redeemableQuest.transform.parent.gameObject;
        if (set <= 0)
        {
            parent.SetActive(false);
            return;
        }
        parent.SetActive(true);
        tx_redeemableQuest.text = "" + set;

    }

    private void Initialize()
    {
        activeQuestIds = GameManager.INSTANCE.Player.activeQuests;
        activeQuestIds.ForEach(InitQuest);
        CheckRedeems();
    }

    private void InitQuest(int id)
    {
        GameObject go = Instantiate(QuestPrefab,questParent.transform);
        Quest quest = go.GetComponent<Quest>();
        quest.Init(CompleteQuest, GetQuestModel(id));
    }

    private QuestModel GetQuestModel(int id)
    {
        return Quests.FirstOrDefault(x => x.id == id);
    }

    private void CompleteQuest(Quest quest)
    {
        if (!isComplete(quest.model))
            throw new ArgumentException("Quest is not Completed!");
        Reward(quest.rewardAmount);
        Destroy(quest.gameObject);
        NewQuest();
    }

    private void NewQuest()
    {
        int max = activeQuestIds.Max();
        if (++max < Quests.Count) 
            InitQuest(max);
    }

    private void Reward(int amount)
    {
        GameManager.INSTANCE.Badges += amount;
    }

    private bool isComplete(QuestModel quest)
    {
        IController controller = GameManager.INSTANCE.GetTopLevel(quest.Requirement.reqObject);
        var manager = controller.GetItemManager(quest.Requirement.reqObject.defenseType);

        switch (quest.Requirement.reqType)
        {
            case ReqType.AMOUNT: return manager.GetAmountOfUnlockedItems() >= quest.Requirement.amount;
            case ReqType.LEVEL: return manager.GetHighestLevel() >= quest.Requirement.amount;
            case ReqType.AMOUNT_LEVEL:
                bool amount = manager.GetAmountOfUnlockedItems() >= quest.Requirement.amount;
                break;
        }

        return false;
    }
}