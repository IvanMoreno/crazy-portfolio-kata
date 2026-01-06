using System.Globalization;

namespace Portfolio;

// Data Class
public class Asset
{
    private readonly DateTime _date;

    public Asset(string description, DateTime date, MeasurableValue value)
    {
        Description = description;
        _date = date;
        Value = value;
    }

    public string Description { get; }

    public DateTime Date => _date.Date;

    public MeasurableValue Value { get; set; }

    // Complicated boolean expression
    // Magic literal
    // Duplicated code
    // Long Method
    public MeasurableValue GetValue(DateTime now) {
        if (Description == "Unicorn") {
            return new PricelessValue();
        }
        if (Description == "French Wine") {
            if (Date.Subtract(now).TotalDays < 0) {
                if (Value.Get() < 200) 
                    return new MeasurableValue(Value.Get() + 20);
            }
            else {
                if (Value.Get() < 200) 
                    return new MeasurableValue(Value.Get() + 10);
            }
        }
        else if (Description == "Lottery Prediction") {
            if (Date.Subtract(now).TotalDays < 0) {
                return new MeasurableValue(Value.Get() - Value.Get());
            }
        
            if (Value.Get() < 800) {
                var result = new MeasurableValue(Value.Get() + 5);
        
                if (Date.Subtract(now).TotalDays < 11 && result.Get() < 800) 
                    result = new MeasurableValue(result.Get() + 20);
        
                if (Date.Subtract(now).TotalDays < 6 && result.Get() < 800) 
                    result = new MeasurableValue(result.Get() + 100);
        
                return result;
            }
        }
        else {
            if (Date.Subtract(now).TotalDays < 0) {
                if (Value.Get() > 0) {
                    return new MeasurableValue(Value.Get() - 20);
                }
            }
            else {
                if (Value.Get() > 0.0) {
                    return new MeasurableValue(Value.Get() - 10);
                }
            }
        }

        return Value;
    }
}

// Data Class
// Speculative Generality
public abstract class Value
{
    protected readonly int _value;

    protected Value(int value)
    {
        _value = value;
    }

    public int Get()
    {
        return _value;
    }
}

public class MeasurableValue
{
    readonly int _value;

    public MeasurableValue(int value)
    {
        _value = value;
    }

    public int Get()
    {
        return _value;
    }

    public MeasurableValue Add(MeasurableValue addend) {
        return new MeasurableValue(Get() + addend.Get());
    }

    public override string ToString()
    {
        return _value.ToString(CultureInfo.CurrentCulture);
    }
}

public class PricelessValue : MeasurableValue
{
    public PricelessValue() : base(int.MaxValue)
    {
    }
}