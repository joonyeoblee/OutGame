using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class UI_Attendance : MonoBehaviour
{
    [FormerlySerializedAs("AttendaceRewardButtons")]
    [SerializeField]
    private UI_AttendanceReward[] AttendaceReward;
   
    private void Start()
    {
        AttendanceManager.Instance.OnDataChanged += Refresh;
    }
    public void Refresh()
    {
        List<AttendanceRewardDTO> data = AttendanceManager.Instance.RewardData;
        for (int i = 0; i < data.Count; i++)
        {
            AttendaceReward[i].Refresh(data[i]);
        }
    }

   


}
