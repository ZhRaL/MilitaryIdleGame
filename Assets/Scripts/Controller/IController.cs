using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public interface IController
    {
        int[] getState();
        void loadState(int[] state);

        bool isObjectUnlocked(int i);
    }
}