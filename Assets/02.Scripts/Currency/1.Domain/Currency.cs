using System;
public enum ECurrencyType
{
    Gold,
    Diamond,
    Ruby,

    Count
}
public class Currency
{
    // 도메인 클래스의 장점:
    // 1. 표현력이 증가한다.
    // -> 화폐의 종류와 값 모두 표현할 수 있다.
    // 2. 무결성이 유지된다. ( 무결성: 데이터의 정확성/ 일관성 / 유효성)
    // -> 최종 값이: 0 미만 금지, 음수와 예산금지
    // 3. 데이터와 데이터를 다루는 로직이 뭉쳐있다. -> 응집도가 높다

    // 자기 서술적인 코드가 된다.(기획서에 의거한 코드가 된다.)
    // 도메인(기획서) 변경이 일어나면 코드에 반영하기 쉽다.

    // 화폐 '도메인' (콘텐츠, 지식, 문제, 기획서를 바탕으로 작성한다 : 기획자랑 말이 통해야한다.)
    public ECurrencyType Type { get; }

    public int Value { get; private set; }

    // 도메인은 '규칙'이 있다.
    public Currency(ECurrencyType type, int value)
    {
        if (value < 0)
        {
            // 치명적인 오류
            throw new Exception("Value는 0보다 작을 수 없습니다.");
        }
        Type = type;
        Value = value;
    }

    public void Add(int addedValue)
    {
        if (addedValue < 0)
        {
            // 치명적인 오류
            throw new Exception("추가값은 음수가 될 수 없습니다.");
        }

        Value += addedValue;
    }


    public bool TryBuy(int value)
    {
        if (value < 0)
        {
            return false;
        }

        if (Value < value)
        {
            return false;
        }

        Value -= value;

        return true;

    }
}
