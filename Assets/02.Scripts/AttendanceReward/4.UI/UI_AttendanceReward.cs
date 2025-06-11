using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_AttendanceReward : MonoBehaviour
{
    public TextMeshProUGUI AttendanceText;
    public Button AttendanceButton;
    public TextMeshProUGUI RewardText;
    public Image RewardImage;
    public Sprite[] Images;

    private AttendanceRewardDTO m_AttendanceRewardDto;
    public void Attendance()
    {
        if (AttendanceManager.Instance.TryClaimReward(m_AttendanceRewardDto))
        {
            // 아이콘 어둡게 혹은 체크
            RewardImage.color = new Color(RewardImage.color.r, RewardImage.color.g, RewardImage.color.b, 0.5f);
            AttendanceButton.interactable = false;
        }
    }
    public void Refresh(AttendanceRewardDTO dto)
    {
        m_AttendanceRewardDto = dto;
        AttendanceText.text = dto.ID;
        RewardImage.sprite = Images[(int)dto.RewardCurrencyType];
        RewardText.text = dto.RewardAmount.ToString();
    }
}
