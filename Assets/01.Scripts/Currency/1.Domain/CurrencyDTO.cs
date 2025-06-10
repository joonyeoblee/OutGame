

// 계층 간 데이터 전송을 위해 도메인 모델 대신 사용되는 객체이다. 
public class CurrencyDTO
{
    public readonly ECurrencyType Type;
    public readonly int Value;


    public CurrencyDTO(Currency currency)
    {
        Type = currency.Type;
        Value = currency.Value;
    }

    public CurrencyDTO(ECurrencyType type, int value)
    {
        Type = type;
        Value = value;
    }


    public bool HaveEnough(int value)
    {
        return Value >= value;
    }
}
