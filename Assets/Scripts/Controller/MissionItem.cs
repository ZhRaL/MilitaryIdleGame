namespace DefaultNamespace
{
    public abstract class MissionItem : Item
    {
        public int MoneyLevel { get; set; }

        public void MoneyUpgrade()
        {
            MoneyLevel++;
        }
    }
}