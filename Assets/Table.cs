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
        return chairs.FirstOrDefault(chair => chair.Unlocked && !chair.Occupied);
    }

    public void Init(int amount, int level)
    {
        if (amount >= chairs.Length)
        {
            Debug.LogError("Amount greater than array Length");
            return;
        }
        
        for (int i = 0; i < chairs.Length; i++)
        {
            if(i<amount)
            chairs[i].Unlocked = true;
            else chairs[i].gameObject.SetActive(false);
        }

        unlockedChairs = amount;
        speed = level;
    }

    public float getWaitingAmount()
    {
        return 4f;
    }
}