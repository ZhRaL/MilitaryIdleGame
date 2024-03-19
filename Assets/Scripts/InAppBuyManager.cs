public class InAppBuyManager : MonoBehaviour 
{
    public static InAppBuyManager INSTANCE;

    private void Awake() {
      INSTANCE = this;
    }

    public bool Collect(int amount) {
        // TODO
        return true;
    }
    
  }
