
using System;
using Interfaces;
using UnityEngine.Serialization;

[Serializable]
public class JsonItem : IDefaultable<JsonItem>
{
    [FormerlySerializedAs("Level")] public int Json_Level = 1;
    public JsonItem CreateDefault => new();
}
