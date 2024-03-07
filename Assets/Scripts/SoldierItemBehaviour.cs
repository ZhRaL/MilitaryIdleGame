namespace DefaultNamespace
{
    public class SoldierItemBehaviour
    {
        public Soldier Soldier { get; set; }
        public Item Item { get; set; }

        public SoldierItemBehaviour(Item item)
        {
            Item = item;
        }
        
        public void Update()
        {
            
        }
        
        public void SoldierSitDown(Soldier soldier)
        {
        }

        public void SoldierGetUp()
        {
        }
    }
}