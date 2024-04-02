using System;
using System.Collections.Generic;
using Interfaces;

[Serializable]
public class JsonManageItem<T>
{
    public List<T> SaveItems = new();

    public void AddItem(T item)
    {
        SaveItems.Add(item);
    }

    public Object GetIndex(int i)
    {
        if (i < SaveItems.Count)
            return SaveItems[i];
        return null;
    }

    public static JsonManageItem<T> Default(IDefaultable<T> gen)
    {
        JsonManageItem<T> ji = new JsonManageItem<T>();
        ji.AddItem(gen.CreateADefault);
        return ji;
    }
}