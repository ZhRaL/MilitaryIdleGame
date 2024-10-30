using System.Threading.Tasks;
using UnityEngine;

public class AdManager : MonoBehaviour
{
  public static AdManager INSTANCE;
  public bool isAdFree;

  private void Awake()
  {
    INSTANCE = this;
  }

  public void SpecialFunc()
  {
    Debug.Log("I am Special");
  }

  public void BuyAdFree()
  {
    Debug.Log("Buying AdFree Mode...");
  }

  public async Task<bool> ShowAsync()
  {
    if (isAdFree)
      return true;
    Debug.Log("Showing Ad");
    bool adShown = await ShowAd();
    return adShown;
  }

  private async Task<bool> ShowAd()
  {
    // Simuliere asynchrone Werbeanzeige
    await Task.Delay(1000); // Warte 1 Sekunde als Beispiel
    return true; // Angenommen, die Werbung wurde erfolgreich angezeigt
  }

}
