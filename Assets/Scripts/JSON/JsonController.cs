using System;
using System.Collections.Generic;

[Serializable]
public class JsonController {
    public List<JsonManageItem> SaveManagers = new();

    public void AddManager(JsonManageItem item) {
      SaveManagers.Add(item);
    }

    public JsonManageItem GetAt(int index)
    {
        if (index < SaveManagers.Count)
            throw new ArgumentException("Index was out of Bounds I guess, was: " + index);

        return SaveManagers[index];

    }
  
}
