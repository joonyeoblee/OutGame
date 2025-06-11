using System;
public class AttendanceRewardDTO
{
    public string ID;
    public int GoalAttendanceDay;
    public RewardState State;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    public AttendanceRewardDTO(string id, int goalAttendanceDay, ECurrencyType rewardCurrencyType, int rewardAmount, RewardState state = RewardState.Claimable)
    {
        ID = id;
        GoalAttendanceDay = goalAttendanceDay;
        State = state;
        RewardCurrencyType = rewardCurrencyType;
        RewardAmount = rewardAmount;
    }
    public AttendanceRewardDTO()
    {
        throw new NotImplementedException();
    }
}
