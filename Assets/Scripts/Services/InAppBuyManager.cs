using System.Threading.Tasks;
using UnityEngine;

public class InAppBuyManager
{
  public static InAppBuyManager INSTANCE;

  public async Task<bool> Collect(int amount)
  {
    Debug.Log("Trying to start InAPP Buy for " + amount + " $");
    await Task.Delay(1000);
    return true;
  }

}
