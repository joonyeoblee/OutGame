using TMPro;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;
public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public TextMeshProUGUI RewardTextUI;
    public Slider ProgressSlider;
    public TextMeshProUGUI ProgressTextUI;
    public Image AchievementIcon;
    public Button RewardClaimButton;

    private AchievementDTO _achievementDto;
    public void Refresh(AchievementDTO achivement)
    {
        _achievementDto = achivement;
        NameTextUI.text = achivement.Name;
        DescriptionTextUI.text = achivement.Description;
        RewardCountTextUI.text = achivement.RewardAmount.ToString();
        ProgressSlider.value = (float)achivement.CurrentValue / achivement.GoalValue;
        ProgressTextUI.text = $"{achivement.CurrentValue}/{achivement.GoalValue}";

        RewardClaimButton.interactable = achivement.CanClaimReward();
    }
    private void Start()
    {
        // EventManager.AddListener<>();
    }
    public void ClaimReward()
    {
        AchievementEvent newEvent = new AchievementEvent();
        newEvent.AchievementDTO = _achievementDto;
        bool Claimed = newEvent.AchievementDTO.CanClaimReward();
        EventManager.Broadcast(newEvent);

        if (Claimed)
        {
            // 성공 꽃가루 뿌려지고

        }
        // 부족 팝업
    }
}
