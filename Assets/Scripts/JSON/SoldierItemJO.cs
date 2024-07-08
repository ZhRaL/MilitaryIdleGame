using System;
using Interfaces;
using Util;

[Serializable]
public class SoldierItemJO : JsonItem, IDefaultable<SoldierItemJO>
{
    public string Name = "John Doe";
    public int SpeedLevel = 1;
    public int MissionRewardLevel = 1;
    public int CritLevel = 1;

    public new SoldierItemJO CreateADefault => new();
}