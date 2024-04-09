using UnityEngine;

public class AdManager : MonoBehaviour {
  public static AdManager INSTANCE;

  private void Awake() {
    INSTANCE = this;
  }

  public void SpecialFunc()
  {
    Debug.Log("I am Special");
  }

  public bool Show() {
    return false;
  }

}
