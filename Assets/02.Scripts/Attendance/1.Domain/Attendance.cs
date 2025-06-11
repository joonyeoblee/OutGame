using System;
using System.Collections.Generic;
public class Attendance
{
    public string ID;

    public int ConsecutiveDays;
    public int NumberOfAttendances;

    public readonly int MaxAttendanceDays;

    public string LastAttendanceTime; // ISO 8601 형식으로 저장

    public List<AttendanceReward> Rewards { get; } = new List<AttendanceReward>();

    public Attendance(string id, int numberOfAttendances, int maxAttendanceDays, string lastAttendanceTime)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new Exception("id는 없을 수 없습니다");

        if (numberOfAttendances < 0)
            throw new Exception("현재 출석 일수는 0보다 작을 수 없습니다");

        if (numberOfAttendances > maxAttendanceDays)
            throw new Exception("현재 출석 일수는 최대 출석 일수 보다 클 수 없습니다");

        if (maxAttendanceDays < 0)
            throw new Exception("최대 출석 일수는 0보다 작을 수 없습니다");

        ID = id;
        MaxAttendanceDays = maxAttendanceDays;
        NumberOfAttendances = numberOfAttendances;
        LastAttendanceTime = lastAttendanceTime;
    }

    public void Attend(DateTime now)
    {
        DateTime lastTime;
        if (!DateTime.TryParse(LastAttendanceTime, out lastTime))
        {
            lastTime = now.AddDays(-2); // 첫 출석처럼 처리
        }

        if (lastTime.Date == now.Date)
            return;

        if ((now.Date - lastTime.Date).Days == 1)
            ConsecutiveDays += 1;
        else
            ConsecutiveDays = 1;

        if (ConsecutiveDays > MaxAttendanceDays)
            ConsecutiveDays = MaxAttendanceDays;
        else if (ConsecutiveDays < 0)
            ConsecutiveDays = 0;

        NumberOfAttendances += 1;
        if (NumberOfAttendances > MaxAttendanceDays)
            NumberOfAttendances = MaxAttendanceDays;
        else if (NumberOfAttendances < 0)
            NumberOfAttendances = 0;

        LastAttendanceTime = now.ToString("s"); // ISO 8601
    }

    public void AddReward(AttendanceReward reward)
    {
        Rewards.Add(reward);
    }

    public AttendanceDTO ToDTO()
    {
        return new AttendanceDTO
        {
            ID = ID,
            ConsecutiveDays = ConsecutiveDays,
            NumberOfAttendances = NumberOfAttendances,
            MaxAttendanceDays = MaxAttendanceDays,
            LastAttendanceTime = LastAttendanceTime,
            Rewards = Rewards.ConvertAll(r => r.ToDTO())
        };
    }
}
