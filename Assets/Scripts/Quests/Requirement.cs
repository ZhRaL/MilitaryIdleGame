using System;
using System.Linq;
using Interfaces;
using Util;

namespace Quests
{
    [Serializable]
    public class Requirement
    {
        public int amount;
        public int levelAmount;
        public ObjectType reqObject;
        public ReqType reqType;

        public bool isFulFilled()
        {
            return reqType switch
            {
                ReqType.AMOUNT => checkAmount(),
                ReqType.LEVEL => checkLevel(),
                ReqType.AMOUNT_LEVEL => checkAmountLevel(),
                _ => throw new NotImplementedException()
            };
        }

        private bool checkAmountLevel()
        {
            var man = GameManager.INSTANCE.GetTopLevel(reqObject).GetItemManager(reqObject.defenseType);
            return man.Items.Count(x => x.Unlocked && x.Level >= levelAmount) >= amount;
        }

        private bool checkLevel()
        {
            var man = GameManager.INSTANCE.GetTopLevel(reqObject).GetItemManager(reqObject.defenseType);
            return man.Items.Exists(x => x.Level >= amount);
        }

        private bool checkAmount()
        {
            var man = GameManager.INSTANCE.GetTopLevel(reqObject).GetItemManager(reqObject.defenseType);
            return man.Items.Count(x => x.Unlocked) >= amount;
        }
    }
}