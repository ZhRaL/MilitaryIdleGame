using System;
using System.Collections.Generic;

[Serializable]
public class JsonController
{
    public List<JsonManageItem> SaveManagers = new();

    public static JsonController Default()
    {
        JsonController controller = new JsonController();
        controller.AddManager(JsonManageItem.Default());
        controller.AddManager(JsonManageItem.Default());
        controller.AddManager(JsonManageItem.Default());
        return controller;
    }

    public void AddManager(JsonManageItem item)
    {
        SaveManagers.Add(item);
    }

    public JsonManageItem GetAt(int index)
    {
        if (index >= SaveManagers.Count)
            return null;

        return SaveManagers[index];
    }
}