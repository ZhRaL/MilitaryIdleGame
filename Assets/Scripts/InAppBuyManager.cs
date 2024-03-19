public class InAppBuyManager : MonoBehaviour 
{
    public static InAppBuyManager INSTANCE;

    private void Awake() {
      INSTANCE = this;
    }
    
  }
