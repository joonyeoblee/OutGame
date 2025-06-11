using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttendanceReward : MonoBehaviour
{
    public TextMeshProUGUI AttendanceText;
    public Button AttendanceButton;
    public TextMeshProUGUI RewardText;
    public Image RewardImage;

    public void Attendance()
    {
        // if(AttendanceManager.Instance.TryClaimReward())
        {
            // 아이콘 어둡게 혹은 체크
        }
    }
    public void Refresh()
    {
        
    }
}
