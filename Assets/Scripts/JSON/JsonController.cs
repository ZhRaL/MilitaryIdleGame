using System;
using System.Collections.Generic;
using Interfaces;

[Serializable]
public class JsonController<T>
{
    public List<JsonManageItem<T>> SaveManagers = new();

    public static JsonController<T> Default(IDefaultable<T> gen)
    {
        JsonController<T> controller = new JsonController<T>();
        controller.AddManager(JsonManageItem<T>.Default(gen));
        controller.AddManager(JsonManageItem<T>.Default(gen));
        controller.AddManager(JsonManageItem<T>.Default(gen));
        return controller;
    }

    public void AddManager(JsonManageItem<T> item)
    {
        
        SaveManagers.Add(item);
    }

    public JsonManageItem<T> GetAt(int index)
    {
        if (index >= SaveManagers.Count)
            return null;

        return SaveManagers[index];
    }
}