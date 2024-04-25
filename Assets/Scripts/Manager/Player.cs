using System;

namespace Manager
{
    [Serializable]
    public class Player
    {
        public List<int> activeQuests = new(){0,1,2};
        public bool AdFreeMode = false;
        
    }
}
