namespace DefaultNamespace
{
    public class SoldierItem : Item
    {
        public Soldier Soldier { get; set; }

        public override JsonItem ToJson()
        {
            return new SoldierItemJO()
            {
                Name = Soldier.name,
                SpeedLevel = Soldier.LVL_Speed,
                MissionRewardLevel = Soldier.LVL_Reward,
                CritLevel = Soldier.LVL_Crit
            };
        }
    }
}
