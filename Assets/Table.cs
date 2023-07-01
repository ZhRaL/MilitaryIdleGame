using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class Table : MonoBehaviour
{
    public Chair[] chairs;
    public int speed;
    public int unlockedChairs;
    public Chair GetNextFreeChair()
    {
        Debug.Log("y");
        return chairs.FirstOrDefault(chair => chair.Unlocked && chair.Occupied);
    }

    public void Init(int amount, int level)
    {
        if (amount >= chairs.Length)
        {
            Debug.Log("Amount greater than array Length");
            return;
        }
        
        for (int i = 0; i < amount; i++)
        {
            chairs[i].Unlocked = true;
        }

        unlockedChairs = amount;
        speed = level;
    }
}