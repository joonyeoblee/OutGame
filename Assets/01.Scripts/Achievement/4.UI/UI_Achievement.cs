using System.Collections.Generic;
using UnityEngine;
public class UI_Achievement : MonoBehaviour
{
    [SerializeField]
    private List<UI_AchievementSlot> _slots;
    [SerializeField]
    private GameObject _achievePopup;
    private bool _isDisplayed;
    [SerializeField]
    private float _displayTime = 2f;
    private float _currentTime;

    private readonly HashSet<string> _alreadyClaimableIds = new HashSet<string>();

    private void Start()
    {
        Refresh();

        AchievementManager.Instance.OnDataChnaged += Refresh;
    }
    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Get();
        bool hasNewClaimable = false;

        for (int i = 0; i < achievements.Count; i++)
        {
            _slots[i].Refresh(achievements[i]);

            if (achievements[i].CanClaimReward() && !_alreadyClaimableIds.Contains(achievements[i].ID))
            {
                _alreadyClaimableIds.Add(achievements[i].ID);
                hasNewClaimable = true;
            }
        }

        if (hasNewClaimable)
        {
            DisplayAchievementPopup();
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

    private void DisplayAchievementPopup()
    {
        _achievePopup.SetActive(true);
        _isDisplayed = true;
    }
}
