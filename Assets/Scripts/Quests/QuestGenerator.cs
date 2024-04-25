using System;
using System.Collections.Generic;
using Util;

namespace Quests
{
    public class QuestGenerator
    {
        private List<QuestModel> quests = new();
        private int currentID;

        private void Add(QuestModel model)
        {
            model.id = currentID++;
            quests.Add(model);
        }

        public List<QuestModel> GetAllQuests()
        {
            Initialize();
            return quests;
        }

        private void Initialize()
        {
            currentID = 0;
            // Items
            CreateItemQuests();

            CreateMissionItemQuests();

            // MissionItems
            var x = GetMissionItems();
        }

        private void CreateMissionItemQuests()
        {
            List<ObjectType> items = GetMissionItems();

            Create_Amount_Items(items, 2);
            Create_Amount_Items(items, 3);

            Create_Level_Items(items, 10);
            Create_Level_Items(items, 25);
            Create_Level_Items(items, 50);
            Create_Level_Items(items, 100);

            Create_LevelAmount_Items(items, 2, 25);
            Create_LevelAmount_Items(items, 3, 40);
        }

        private List<ObjectType> GetMissionItems()
        {
            List<ObjectType> li = new();
            li.Add(new ObjectType(GenericObjectType.JET_MONEY));
            li.Add(new ObjectType(GenericObjectType.JET_TIME));
            li.Add(new ObjectType(GenericObjectType.TANK_MONEY));
            li.Add(new ObjectType(GenericObjectType.TANK_TIME));
            li.Add(new ObjectType(GenericObjectType.SHIP_MONEY));
            li.Add(new ObjectType(GenericObjectType.SHIP_TIME));
            return li;
        }

        private void CreateItemQuests()
        {
            List<ObjectType> items = new() { ObjectType.Bat, ObjectType.Kit, ObjectType.Sle };

            Create_Level_Items(items, 10);
            Create_Level_Items(items, 25);
            Create_Level_Items(items, 50);
            Create_Level_Items(items, 100);

            Create_Amount_Items(items, 2);
            Create_Amount_Items(items, 3);
            Create_Amount_Items(items, 4);

            Create_LevelAmount_Items(items, 2, 25);
            Create_LevelAmount_Items(items, 3, 40);
            Create_LevelAmount_Items(items, 4, 70);
        }

        private void Create_Amount_Items(List<ObjectType> types, int amount)
        {
            foreach (var typ in types)
            {
                Create_Amount(typ, amount);
            }
        }

        private void Create_Level_Items(List<ObjectType> types, int level)
        {
            foreach (var typ in types)
            {
                Create_Level(typ, level);
            }
        }

        private void Create_LevelAmount_Items(List<ObjectType> types, int amount, int level)
        {
            foreach (var typ in types)
            {
                Create_LevelAmount(typ, amount, level);
            }
        }

        private void Create_LevelAmount(ObjectType objectType, int amount, int levelAmount)
        {
            var x = GetAllTSK(objectType);
            foreach (var type in x)
            {
                Add(CreateQuestModel_LevelAmount(type, amount, levelAmount));
            }
        }

        private void Create_Level(ObjectType objectType, int amount)
        {
            var x = GetAllTSK(objectType);
            foreach (var type in x)
            {
                Add(createQuestModel_Level(type, amount));
            }
        }

        private void Create_Amount(ObjectType objectType, int amount)
        {
            var x = GetAllTSK(objectType);
            foreach (var type in x)
            {
                Add(CreateQuestModel_Amount(type, amount));
            }
        }

        private QuestModel CreateQuestModel_Amount(ObjectType type, int amount)
        {
            Requirement req = new Requirement
            {
                amount = amount,
                reqObject = type,
                reqType = ReqType.AMOUNT
            };
            return new QuestModel(req, 3);
        }

        private QuestModel createQuestModel_Level(ObjectType type, int amount)
        {
            Requirement req = new Requirement
            {
                amount = amount,
                reqObject = type,
                reqType = ReqType.LEVEL
            };
            return new QuestModel(req, 3);
        }

        private QuestModel CreateQuestModel_LevelAmount(ObjectType type, int amount, int levelAmount)
        {
            Requirement req = new Requirement
            {
                amount = amount,
                levelAmount = levelAmount,
                reqObject = type,
                reqType = ReqType.AMOUNT
            };
            return new QuestModel(req, 3);
        }

        private List<ObjectType> GetAllTSK(ObjectType type)
        {
            List<ObjectType> toRet = new();
            foreach (DefenseType defenseType in Enum.GetValues(typeof(DefenseType)))
            {
                toRet.Add(new ObjectType
                {
                    defenseType = defenseType,
                    objectType = type.objectType
                });
            }

            return toRet;
        }
    }
}