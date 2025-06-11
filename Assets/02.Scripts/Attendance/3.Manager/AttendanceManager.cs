using System;
using System.Collections.Generic;
using UnityEngine;
public class AttendanceManager : Singleton<AttendanceManager>
{
    private const string AddressableKey = "Assets/02.Scripts/Attendance/0.Data/AttendanceData.csv";
    private CSVLoader _csvLoader;
    private AttendanceRepository _repository;

    public List<AttendanceRewardDTO> RewardData { get; private set; }
    public Attendance Attendance { get; private set; } // 실제 출석 객체
    public Action OnDataChanged;

    public int TestDay = 11;

    protected override void Awake()
    {
        base.Awake();
        _csvLoader = new CSVLoader();
        _repository = new AttendanceRepository(); // 혹은 FindObjectOfType 또는 DI로 주입
        RewardData = new List<AttendanceRewardDTO>();
        _csvLoader.Load(AddressableKey, OnDataLoaded);
    }

    private void OnDataLoaded()
    {
        RewardData = _csvLoader.GetAll();
        Debug.Log($"[Manager] Attendance data loaded: {RewardData.Count} entries.");

        LoadAttendance();

        OnDataChanged?.Invoke();
    }

    public void SaveAttendance()
    {
        if (Attendance == null) return;
        AttendanceDTO dto = Attendance.ToDTO();
        _repository.Save(dto);
    }

    public void LoadAttendance()
    {
        AttendanceDTO dto = _repository.Load();

        if (dto != null)
        {
            Attendance = new Attendance(
                dto.ID,
                dto.NumberOfAttendances,
                dto.MaxAttendanceDays,
                dto.LastAttendanceTime
            );
            Attendance.ConsecutiveDays = dto.ConsecutiveDays;

            // 보상 데이터 복원 (필요 시)
            foreach(AttendanceRewardDTO rewardDto in dto.Rewards)
            {
                Attendance.AddReward(new AttendanceReward(rewardDto));
            }

            Debug.Log("[Manager] Attendance data restored from saved file.");
        }
        else
        {
            // 초기화 (새로 시작할 경우)
            string initialTime = DateTime.MinValue.ToString("s"); // ISO 8601 형식
            Attendance = new Attendance("player_001", 0, 30, initialTime);
            Debug.Log("[Manager] No saved attendance data. Created new instance.");
        }
    }

    public bool TryClaimReward(AttendanceRewardDTO attendanceRewardDto)
    {
        if (TestDay >= attendanceRewardDto.GoalAttendanceDay)
        {
            CurrencyManager.Instance.Add(attendanceRewardDto.RewardCurrencyType, attendanceRewardDto.RewardAmount);
            return true;
        }
        return false;
    }
}
