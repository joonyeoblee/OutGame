using System.Collections.Generic;
using UnityEngine;
public class UI_Achievement : MonoBehaviour
{
    [SerializeField] private UI_AchievementSlot _slotPrefab;
    [SerializeField] private Transform _slotParent;
    [SerializeField] private List<UI_AchievementSlot> _slots;
    [SerializeField]
    private GameObject _achievePopup;
    private UI_Notification _notificationUI;
    private bool _isDisplayed;
    [SerializeField]
    private float _displayTime = 2f;
    private float _currentTime;

    private readonly HashSet<string> _alreadyClaimableIds = new HashSet<string>();

    private void Start()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Get();

        for (int i = _slots.Count; i < achievements.Count; i++)
        {
            UI_AchievementSlot newSlot = Instantiate(_slotPrefab, _slotParent);
            _slots.Add(newSlot);
        }

        Refresh();

        AchievementManager.Instance.OnDataChnaged += Refresh;
    }
    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Get();
        bool hasNewClaimable = false;
        AchievementDTO latestClaimable = null;

        for (int i = 0; i < achievements.Count; i++)
        {
            _slots[i].Refresh(achievements[i]);

            if (achievements[i].CanClaimReward() && !_alreadyClaimableIds.Contains(achievements[i].ID))
            {
                _alreadyClaimableIds.Add(achievements[i].ID);
                hasNewClaimable = true;
                latestClaimable = achievements[i]; // 마지막 발견된 달성 업적
            }
        }

        if (hasNewClaimable && latestClaimable != null)
        {
            DisplayAchievementPopup(latestClaimable);
        }
    }



    private void Update()
    {
        if (_isDisplayed)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _displayTime)
            {
                _currentTime = 0f;
                _isDisplayed = false;
                _achievePopup.SetActive(false);
            }
        }
    }

    private void DisplayAchievementPopup(AchievementDTO achievement)
    {
        _notificationUI = _achievePopup.GetComponent<UI_Notification>();
        _notificationUI.AchievementNameTextUI.text = achievement.Name;
        _notificationUI.AchievementDescriptionTextUI.text = achievement.Description;

        _achievePopup.SetActive(true);
        _isDisplayed = true;
    }
}
