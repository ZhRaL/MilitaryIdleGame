

namespace DefaultNameSpace {

  public class ShopItem : MonoBehaviour {
    public Reward Reward {get;set;}
    public Cost Cost {get;set;}

    public void Buy() {
    switch(Cost.type) {
      case Costs.MONEY:
        if(InAppBuyManager.INSTANCE.Collect(Cost.amount)) 
          Reward.Checkout();
        break;
      case Costs.ADVERTISMENT:
        if(AdManager.Show())
          Reward.Checkout();
        break;
      case Costs.GOLD:
        GameManager.INSTANCE.Gold -= Cost.amount;
        break;
      case Costs.BADGES:
        GameManager.INSTANCE.Badges -= Cost.amount;
        break;
      default:
        throw new ArgumentOutOfRangeException("Not a valid Type");
      }
    }
  }
}
