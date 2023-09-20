using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public interface IController
    {
        int[] getState();
        void loadState(int[] state);

        bool isObjectUnlocked(int i);
        int getLevelLevel(int index);
        int getTimeLevel(int index);
        void upgrade_Level(int index);
        void upgrade_Time(int index);
    }
}