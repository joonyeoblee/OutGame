using System;
using System.Collections.Generic;
using UnityEngine;
public class AttendanceManager : Singleton<AttendanceManager>
{
    private const string AddressableKey = "Assets/02.Scripts/Attendance/0.Data/AttendanceData.csv";
    private CSVLoader _csvLoader;
    public List<AttendanceRewardData> RewardData { get; private set; }
    public Action OnDataChanged;

    private int _testDay = 12;
    protected override void Awake()
    {
        base.Awake();
        _csvLoader = new CSVLoader();
        RewardData = new List<AttendanceRewardData>();
        _csvLoader.Load(AddressableKey, OnDataLoaded);
    }
    private void OnDataLoaded()
    {
        RewardData = _csvLoader.GetAll();
        Debug.Log($"[Manager] Attendance data loaded: {RewardData.Count} entries.");
        OnDataChanged?.Invoke();
    }
    
}
