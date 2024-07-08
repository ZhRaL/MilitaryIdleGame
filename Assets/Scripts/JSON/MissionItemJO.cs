
using System;
using Interfaces;

[Serializable]
public class MissionItemJO : JsonItem, IDefaultable<MissionItemJO>
{

    public int MoneyLevel = 1;

    public new MissionItemJO CreateADefault => new();
}
