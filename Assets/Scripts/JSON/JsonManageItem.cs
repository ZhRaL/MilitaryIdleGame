using System;
using System.Collections.Generic;

[Serializable]
public class JsonManageItem {
    public List<JsonItem> SaveItems = new(){};

    public void AddItem(JsonItem item) {
      SaveItems.Add(item);
    }

    public JsonItem GetIndex(int i)
    {
        if (i < SaveItems.Count) 
            return SaveItems[i];
        return null;
    }

    public static JsonManageItem Default()
    {
        JsonManageItem ji = new JsonManageItem();
        ji.AddItem(new JsonItem());
        return ji;
    }
}
