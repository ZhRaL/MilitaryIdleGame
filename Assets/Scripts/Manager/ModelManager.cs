using UnityEngine;

namespace Manager
{
    public class ModelManager : MonoBehaviour
    {
        [Header("Ships")] public GameObject[] ShipPrefabs;
        [Header("Airplanes")] public GameObject[] JetPrefabs;
        [Header("Tanks")] public GameObject[] TankPrefabs;
    }
}