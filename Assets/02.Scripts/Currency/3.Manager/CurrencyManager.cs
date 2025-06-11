using System;
using System.Collections.Generic;
using System.Linq;
using Unity.FPS.Game;
using UnityEngine;

// 아키텍처: 설계 그 잡채(설계마다 철학이 있다.)
// 디자인 패턴: 설계를 구현하는 과정에서 쓰이는 패턴

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;

    // 도메인에 변화가 있을 때 호출되는 액션
    public event Action OnDataChanged;

    private CurrencyRepository _repository;

    // 마틴 아저씨: 미리하는 성능 최적화의 90%는 의미가 없다.
    // public event Action OnGoldChanged;
    // public event Action OnDiamondChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        // 생성
        _currencies = new Dictionary<ECurrencyType, Currency>((int)ECurrencyType.Count);

        // 레포지토리(깃허브)
        _repository = new CurrencyRepository();

        List<CurrencyDTO> loadedCurrencies = _repository.Load(AccountManager.Instance?.CurrentAccount.Email);
        if (loadedCurrencies == null)
        {
            for (int i = 0; i < (int)ECurrencyType.Count; ++i)
            {
                ECurrencyType type = (ECurrencyType)i;

                // 골드, 다이아몬드 등을 0 값으로 생성후 딕셔너리에 삽입
                Currency currency = new Currency(type, 0);
                _currencies.Add(type, currency);
            }
        }
        else
        {
            foreach(CurrencyDTO data in loadedCurrencies)
            {
                Debug.Log(data.Type + data.Value);
                Currency currency = new Currency(data.Type, data.Value);
                _currencies.Add(currency.Type, currency);
            }
        }


    }
    private void Start()
    {
        EventManager.AddListener<CurrencyEvent>(OnEnemyKilled);
    }
    private List<CurrencyDTO> ToDtoList()
    {
        return _currencies.ToList().ConvertAll(currency => new CurrencyDTO(currency.Value));
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }

    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);

        // 다양한 이유로 여기에 규칙이 들어가기도한다.

        _repository.Save(ToDtoList(), AccountManager.Instance?.CurrentAccount.Email);
        OnDataChanged?.Invoke();
    }


    public bool TryBuy(ECurrencyType type, int value)
    {
        if (!_currencies[type].TryBuy(value))
        {
            return false;
        }

        _repository.Save(ToDtoList(), AccountManager.Instance.CurrentAccount.Email);

        OnDataChanged?.Invoke();

        return true;
    }
    private void OnEnemyKilled(CurrencyEvent cvt)
    {
        Add(cvt.Currency, cvt.Amount);
    }
}
