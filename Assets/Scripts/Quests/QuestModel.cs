

using Quests;
using Util;

public struct QuestModel{
  public Requirement Requirement;
  public int RewardAmount;
  public int id;

  public QuestModel(Requirement requirement, int rewardAmount) : this()
  {
    Requirement = requirement;
    RewardAmount = rewardAmount;
  }
}
