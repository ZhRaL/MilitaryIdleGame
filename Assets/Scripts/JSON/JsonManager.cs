using System;
using System.Collections.Generic;

[Serializable]
public class Jsonmanager {
    public List<JsonController> Controllers = new();

    public void AddManager(JsonController item) {
      Controllers.Add(item);
    }

    public string GetSaveString() {
      // return Jsonhelper.ToString(this);
      return null;
    }
}
