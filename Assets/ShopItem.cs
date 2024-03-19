

namespace DefaultNameSpace {

  public class ShopItem : MonoBehaviour {
    public Reward Reward {get;set;}
    public Cost Cost {get;set;}

    public void Buy() {
      if(Validate()) 
        Reward.Checkout();
    }

    private bool Validate() {
      switch(Cost.type) {
        case Costs.MONEY:
          return InAppBuyManager.INSTANCE.Collect(Cost.amount);
        case Costs.ADVERTISMENT:
          return AdManager.Show();
        case Costs.GOLD:
          GameManager.INSTANCE.Gold -= Cost.amount;
          return true;
        case Costs.BADGES:
          GameManager.INSTANCE.Badges -= Cost.amount;
          return true;
          
        default:
          throw new ArgumentOutOfRangeException("Not a valid Type");
      }
      return false;
    }

    
  }
}
