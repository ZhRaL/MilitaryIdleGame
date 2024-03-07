using System;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public interface IManageItem
    {
        List<IItem> Items { Get; };

        DefenseType DefenseType { Get; };

        
        
        void Init(int[] levels);

        void PlaceSoldier();

        int GetLevelForItem(int index);

        void UpgradeItem(int index);

        UnityAction GetUpgradeMethod(int index);

        float GetAverageTime();

        int GetAmountOfUnlockedItems();
    }
}
