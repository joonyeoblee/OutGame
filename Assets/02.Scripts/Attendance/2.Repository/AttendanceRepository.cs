using UnityEngine;
public class AttendanceRepository : MonoBehaviour
{
    private static readonly string SAVE_KEY = "ATTENDANCE_DTO";

    public void Save(AttendanceDTO dto)
    {
        string json = JsonUtility.ToJson(dto);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
        Debug.Log("[Repository] Attendance data saved.");
    }

    public AttendanceDTO Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            Debug.Log("[Repository] No saved attendance data found.");
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        try
        {
            AttendanceDTO dto = JsonUtility.FromJson<AttendanceDTO>(json);
            Debug.Log("[Repository] Attendance data loaded.");
            return dto;
        }
        catch
        {
            Debug.LogWarning("[Repository] Failed to parse saved attendance data.");
            return null;
        }
    }
}
