using UnityEngine;
public class UI_Attendance : MonoBehaviour
{
    [SerializeField]
    private UI_AttendanceReward[] AttendaceRewardButtons;
    public Sprite[] Images;

    private void Start()
    {
        AttendanceManager.Instance.OnDataChanged += Refresh;
        Refresh();
    }
    public void Refresh()
    {
        for (int i = 0; i < AttendanceManager.Instance.RewardData.Count; i++)
        {
            AttendaceRewardButtons[i].AttendanceText.text = AttendanceManager.Instance.RewardData[i].ID;
            AttendaceRewardButtons[i].RewardImage.sprite = Images[(int)AttendanceManager.Instance.RewardData[i].RewardCurrencyType];
            AttendaceRewardButtons[i].RewardText.text = AttendanceManager.Instance.RewardData[i].RewardAmount.ToString();
        }
    }

    public Sprite Get(string type)
    {
        switch (type)
        {
            case "Gold":
                return Images[0];
            case "Diamond":
                return Images[1];
            case "Ruby":
                return Images[2];
        }
        return null;
    }


}
