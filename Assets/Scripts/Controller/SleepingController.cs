using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class SleepingController : MonoBehaviour, IController
{

    public Room roomArmy, roomAirF, roomMar;
    
    public int[] getState()
    {
        return roomArmy.getState()
            .Concat(roomAirF.getState())
            .Concat(roomMar.getState())
            .ToArray();
    }

    public void loadState(int[] state)
    {
        if (state.Length != 12) throw new ArgumentException("invalid amount");
        
        roomArmy.Init(state[..4]);
        roomAirF.Init(state[4..8]);
        roomMar.Init(state[8..12]);
    }

    public bool isObjectUnlocked(int i)
    {
        throw new NotImplementedException();
    }
    
    
}
