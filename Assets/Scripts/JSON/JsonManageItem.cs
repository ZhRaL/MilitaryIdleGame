using System;
using System.Collections.Generic;

[Serializable]
public class JsonManageItem {
    public List<JsonItem> SaveItems = new();

    public void AddItem(JsonItem item) {
      SaveItems.Add(item);
    }


    public JsonItem GetIndex(int i)
    {
        if (i < SaveItems.Count) return SaveItems[i];
        return null;
    }
}
