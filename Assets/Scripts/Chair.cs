using UnityEngine;

namespace DefaultNamespace
{
    public class Chair : MonoBehaviour
    {
        private bool _unlocked;
        public bool Unlocked
        {
            get { return _unlocked;}
            set
            {
                gameObject.SetActive(value);
                _unlocked = value;
            }
        }

        public bool Occupied { get; private set; }

        public void ToggleOccupation()
        {
            Occupied = !Occupied;
        }
    }
}