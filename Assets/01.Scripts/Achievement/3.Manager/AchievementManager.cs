using System;
using System.Collections.Generic;
using System.Linq;
using Unity.FPS.Game;
using UnityEngine;
// ReSharper disable All
public class AchievementManager : Singleton<AchievementManager>
{
    [SerializeField]
    private List<AchievementSO> _metaDatas;

    private List<Achievement> _achievements;
    public List<Achievement> Achievements => _achievements;
    public Action OnDataChanged { get; set; }

    private AchievementRepository _achievementRepository;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    private void Init()
    {
        _achievementRepository = new AchievementRepository();

        _achievements = new List<Achievement>();
        List<AchievementDTO> saveDatas = _achievementRepository.Load();
        foreach(AchievementSO metaData in _metaDatas)
        {
            Achievement duplicatedAchievement = FindByID(metaData.ID);
            if (duplicatedAchievement != null)
            {
                throw new Exception($"업적 ID{metaData.ID}가 중복됩니다");
            }
            AchievementDTO saveData = saveDatas?.Find(x => x.ID == metaData.ID) ?? null;
            Achievement achievement = new Achievement(metaData, saveData);
            _achievements.Add(achievement);
        }
    }
    protected override void Start()
    {
        base.Start();
        EventManager.AddListener<EnemyKillEvent>(OnEnemyKilled);
        EventManager.AddListener<CurrencyEvent>(OnCurrencyChanged);
        EventManager.AddListener<AchievementEvent>(TryClaimReward);
    }

    private Achievement FindByID(string id)
    {
        return _achievements.Find(achievement => achievement.ID == id);
    }
    private List<AchievementDTO> ToDtoList()
    {
        return _achievements.ToList().ConvertAll(achivement => new AchievementDTO(achivement));
    }

    public List<AchievementDTO> Get()
    {
        return ToDtoList();
    }
    public void Increase(EAchievementCondition condition, int value)
    {
        foreach(Achievement achievement in _achievements)
        {
            if (achievement.Condition == condition)
            {
                achievement.Increase(value);
            }
        }

        _achievementRepository.Save(ToDtoList());
        OnDataChanged?.Invoke();
    }

    private void OnEnemyKilled(EnemyKillEvent evt)
    {
        Increase(EAchievementCondition.DroneKillCount, 1);
    }

    private void OnCurrencyChanged(CurrencyEvent cvt)
    {
        switch (cvt.Currency)
        {
            case ECurrencyType.Gold:
                Increase(EAchievementCondition.GoldCollect, cvt.Amount);
                break;
            case ECurrencyType.Diamond:
                Increase(EAchievementCondition.DiamondCollect, cvt.Amount);
                break;
        }
    }


    public void TryClaimReward(AchievementEvent achievementEvent)
    {
        AchievementDTO achievementDto = achievementEvent.AchievementDTO;
        Achievement achievement = FindByID(achievementDto.ID);
        if (achievement == null)
        {
            return;
        }

        if (achievement.TryClaimedReward())
        {
            CurrencyEvent currencyEvent = Events.CurrencyEvent;
            currencyEvent.Currency = achievement.RewardCurrencyType;
            currencyEvent.Amount = achievement.RewardAmount;
            EventManager.Broadcast(currencyEvent);

            OnDataChanged?.Invoke();

        }
    }
}
