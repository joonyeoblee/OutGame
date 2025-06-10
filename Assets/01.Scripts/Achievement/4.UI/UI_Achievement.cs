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
    private void Start()
    {
        Refresh();

        AchievementManager.Instance.OnDataChnaged += Refresh;
    }
    private void Refresh()
    {
        List<AchievementDTO> achivements = AchievementManager.Instance.Get();

        for (int i = 0; i < achivements.Count; i++)
        {
            _slots[i].Refresh(achivements[i]);
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
