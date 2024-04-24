using System;
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
    }
}