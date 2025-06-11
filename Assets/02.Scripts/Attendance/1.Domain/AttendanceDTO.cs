using System;
using System.Collections.Generic;
[Serializable]
public class AttendanceDTO
{
    public string ID;
    public int ConsecutiveDays;
    public int NumberOfAttendances;
    public int MaxAttendanceDays;
    public string LastAttendanceTime;
    public List<AttendanceRewardDTO> Rewards;
}
