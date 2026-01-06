using System.Globalization;

namespace Portfolio;

// Data Class
public class Asset
{
    private readonly DateTime _date;

    public Asset(string description, DateTime date, AssetValue value)
    {
        Description = description;
        _date = date;
        Value = value;
    }

    public string Description { get; }

    public DateTime Date => _date.Date;

    public AssetValue Value { get; set; }

    // Complicated boolean expression
    // Magic literal
    // Duplicated code
    // Long Method
    public AssetValue GetValue(DateTime now) {
        if (Description == "Unicorn") {
            return new PricelessValue();
        }
        if (Description == "French Wine") {
            if (Date.Subtract(now).TotalDays < 0) {
                if (Value.Get() < 200) 
                    return new AssetValue(Value.Get() + 20);
            }
            else {
                if (Value.Get() < 200) 
                    return new AssetValue(Value.Get() + 10);
            }
        }
        else if (Description == "Lottery Prediction") {
            if (Date.Subtract(now).TotalDays < 0) {
                return new AssetValue(Value.Get() - Value.Get());
            }
        
            if (Value.Get() < 800) {
                var result = new AssetValue(Value.Get() + 5);
        
                if (Date.Subtract(now).TotalDays < 11 && result.Get() < 800) 
                    result = new AssetValue(result.Get() + 20);
        
                if (Date.Subtract(now).TotalDays < 6 && result.Get() < 800) 
                    result = new AssetValue(result.Get() + 100);
        
                return result;
            }
        }
        else {
            if (Date.Subtract(now).TotalDays < 0) {
                if (Value.Get() > 0) {
                    return new AssetValue(Value.Get() - 20);
                }
            }
            else {
                if (Value.Get() > 0.0) {
                    return new AssetValue(Value.Get() - 10);
                }
            }
        }

        return Value;
    }
}

// Data Class
// Speculative Generality
public class AssetValue
{
    readonly int _value;

    public AssetValue(int value)
    {
        _value = value;
    }

    public int Get()
    {
        return _value;
    }

    public AssetValue Add(AssetValue addend) {
        return new AssetValue(Get() + addend.Get());
    }

    public override string ToString()
    {
        return _value.ToString(CultureInfo.CurrentCulture);
    }
}

public class PricelessValue : AssetValue
{
    public PricelessValue() : base(int.MaxValue)
    {
    }
}