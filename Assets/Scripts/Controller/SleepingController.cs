using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class SleepingController : MonoBehaviour, IController
{
    [SerializeField] private Room roomArmy, roomAirF, roomMar;

    public IManageItem ArmyManager => roomArmy;
    public IManageItem AirforceManager => roomAirF;
    public IManageItem MarineManager => roomMar;

    public int[] getState()
    {
        return roomArmy.GetState()
            .Concat(roomAirF.GetState())
            .Concat(roomMar.GetState())
            .ToArray();
    }

    public void loadState(int[] state)
    {
        if (state.Length != 12)
            throw new ArgumentException("invalid amount");

        roomArmy.Init(state[..4]);
        roomAirF.Init(state[4..8]);
        roomMar.Init(state[8..12]);
    }
}