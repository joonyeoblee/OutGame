using System;
// ReSharper disable All
public enum EAchievementCondition
{
    GoldCollect,
    DiamondCollect,
    DroneKillCount,
    BossKillCount,
    PlayTime,
    Trigger
}

public class Achievement
{
    // 최종 목적 : 자기 서술형
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    private int _currentValue;
    public int CurrentValue => _currentValue;

    private bool _rewardClaimed;
    public bool RewardClaimed => _rewardClaimed;


    public Achievement(AchievementSO metaData, AchievementDTO saveData)
    {
        if (string.IsNullOrEmpty(metaData.ID))
        {
            throw new ArgumentException("id는 비어있을 수 없습니다");
        }
        if (string.IsNullOrEmpty(metaData.Name))
        {
            // try catch를 쓰라는게 아님 이 상황을 만들지 말아야함
            throw new ArgumentException("업적 이름은 비어있을 수 없습니다.");
        }
        if (string.IsNullOrEmpty(metaData.Description))
        {
            // try catch를 쓰라는게 아님 이 상황을 만들지 말아야함
            throw new ArgumentException("업적 설명은 비어있을 수 없습니다.");
        }
        if (metaData.GoalValue <= 0)
        {
            throw new Exception("업적 목표 값은 0보다 커야합니다");
        }
        if (metaData.RewardAmount <= 0)
        {
            throw new Exception("업적 보상 값은 0보다 커야합니다");
        }

        if (saveData != null && saveData.CurrentValue < 0)
        {
            throw new Exception("업적 진행 값은 0보다 커야합니다");
        }

        ID = metaData.ID;
        Name = metaData.Name;
        Description = metaData.Description;
        Condition = metaData.Condition;
        GoalValue = metaData.GoalValue;
        RewardCurrencyType = metaData.RewardCurrencyType;
        RewardAmount = metaData.RewardAmount;

        // Load();
        if (saveData != null)
        {
            _currentValue = saveData.CurrentValue;
            _rewardClaimed = saveData.RewardClaimed;
        }
    }
    

    public void Increase(int value)
    {
        if (value <= 0)
        {
            throw new Exception("증가 값은 0보다 커야합니다.");
        }

        _currentValue += value;
    }
    public bool CanClaimReward()
    {
        return _rewardClaimed == false && _currentValue >= GoalValue;
    }

    public bool TryClaimedReward()
    {
        if (CanClaimReward())
        {
            _rewardClaimed = true;
        }
        // 초기 값이 false니까 
        return _rewardClaimed;
    }
}
[Serializable]
public struct AchievementSaveData
{
    // DTO는 ReadOnly라 저장이 안됨
    // public List<AchievementSaveData> DataList;
    public string ID;
    public int CurrentValue;
    public bool RewardClaimed;

}