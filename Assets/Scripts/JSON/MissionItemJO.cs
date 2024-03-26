
using System;
using Interfaces;

[Serializable]
public class MissionItemJO : JsonItem, IDefaultable<MissionItemJO> {

    public int TimeLevel  = 1;
    public int MoneyLevel  = 1;

    public MissionItemJO CreateDefault => new();
}
