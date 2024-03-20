using UnityEngine;

public class AdManager : MonoBehaviour {
  public static AdManager INSTANCE;

  private void Awake() {
    INSTANCE = this;
  }

  public bool Show() {
    return false;
  }

}
