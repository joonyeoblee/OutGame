using System;
[Serializable]
public class AchievementDTO
{
    public string ID;
    public string Name;
    public string Description;
    public EAchievementCondition Condition;
    public int GoalValue;
    public int RewardAmount;
    public ECurrencyType RewardCurrencyType;

    public int CurrentValue;
    public bool RewardClaimed;

    // 기본 생성자 (JSON 역직렬화 등을 위해 필요)
    public AchievementDTO() { }

    // Achievement 인스턴스로부터 DTO 생성
    public AchievementDTO(Achievement achievement)
    {
        ID = achievement.ID;
        Name = achievement.Name;
        Description = achievement.Description;
        Condition = achievement.Condition;
        GoalValue = achievement.GoalValue;
        RewardAmount = achievement.RewardAmount;
        RewardCurrencyType = achievement.RewardCurrencyType;
        CurrentValue = achievement.CurrentValue;
        RewardClaimed = achievement.RewardClaimed;
    }

    // 모든 필드를 직접 받아서 DTO 생성
    public AchievementDTO(
        string id,
        string name,
        string description,
        EAchievementCondition condition,
        int goalValue,
        int rewardAmount,
        ECurrencyType rewardCurrencyType,
        int currentValue,
        bool rewardClaimed)

    {
        ID = id;
        Name = name;
        Description = description;
        Condition = condition;
        GoalValue = goalValue;
        RewardAmount = rewardAmount;
        RewardCurrencyType = rewardCurrencyType;
        CurrentValue = currentValue;
        RewardClaimed = rewardClaimed;
    }
    public AchievementDTO(
        string id,
        int currentValue,
        bool rewardClaimed)
    {
        ID = id;
        CurrentValue = currentValue;
        RewardClaimed = rewardClaimed;
    }
    public bool CanClaimReward()
    {
        return RewardClaimed == false && CurrentValue >= GoalValue;
    }


}
