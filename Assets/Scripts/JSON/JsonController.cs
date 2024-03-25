
public class JsonController {
    public List<JsonManageItem> SaveManagers = new();

    public void AddManager(JsonManageItem item) {
      SaveManagers.Add(item);
    }
  
}
