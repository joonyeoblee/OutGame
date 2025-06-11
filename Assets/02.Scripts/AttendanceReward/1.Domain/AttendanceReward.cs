public enum RewardState
{
    NotAvailable,
    Claimable,
    Claimed
}

public class AttendanceReward
{
    public readonly string ID;
    public readonly int GoalAttendanceDay; // 나 3일차 보상임

    public readonly RewardState State;

    public readonly ECurrencyType RewardCurrencyType;
    public readonly int RewardAmount;

    public AttendanceReward(string id, int goalAttendanceDay, RewardState state, ECurrencyType rewardCurrencyType, int rewardAmount)
    {
        
        
        ID = id;
        GoalAttendanceDay = goalAttendanceDay;
        State = state;
        RewardCurrencyType = rewardCurrencyType;
        RewardAmount = rewardAmount;
    }
}

[System.Serializable]
public class AttendanceRewardData
{
    public string ID;
    public int GoalAttendanceDay;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;
}
