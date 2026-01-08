using System.Globalization;

namespace Portfolio;

public class Asset
{
    private readonly DateTime _date;

    public Asset(string description, DateTime date, Value value)
    {
        Description = description;
        _date = date;
        Value = value;
    }

    public string Description { get; }

    public DateTime Date => _date.Date;

    public Value Value { get; set; }

    bool Equals(Asset other) {
        return _date.Equals(other._date) && Description == other.Description && Value.Get().Equals(other.Value.Get());
    }

    public override bool Equals(object? obj) {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Asset)obj);
    }

    public override int GetHashCode() {
        return HashCode.Combine(_date, Description, Value);
    }
    
    public override string ToString() {
        return $"{nameof(_date)}: {_date}, {nameof(Description)}: {Description}, {nameof(Value)}: {Value}";
    }

    public void GetValue(DateTime now) {
        if (Date.Subtract(now).TotalDays < 0)
        {
            if (Description != "French Wine")
            {
                if (Description != "Lottery Prediction")
                {
                    if (Value.Get() > 0)
                    {
                        if (Description != "Unicorn")
                        {
                            Value = new MeasurableValue(Value.Get() - 20);
                        }
                    }
                }
                else
                {
                    Value = new MeasurableValue(Value.Get() - Value.Get());
                }
            }
            else
            {
                if (Value.Get() < 200) Value = new MeasurableValue(Value.Get() + 20);
            }
        }
        else
        {
            if (Description != "French Wine" && Description != "Lottery Prediction")
            {
                if (Value.Get() > 0.0)
                {
                    if (Description != "Unicorn")
                    {
                        Value = new MeasurableValue(Value.Get() - 10);
                    }
                }
                else
                {
                    if (Description == "Unicorn") {
                    }
                }
            }
            else
            {
                if (Description == "Lottery Prediction")
                {
                    if (Value.Get() < 800)
                    {
                        Value = new MeasurableValue(Value.Get() + 5);

                        if (Date.Subtract(now).TotalDays < 11)
                            if (Value.Get() < 800)
                                Value = new MeasurableValue(Value.Get() + 20);

                        if (Date.Subtract(now).TotalDays < 6)
                            if (Value.Get() < 800)
                                Value = new MeasurableValue(Value.Get() + 100);
                    }
                }
                else
                {
                    if (Value.Get() < 200) Value = new MeasurableValue(Value.Get() + 10);
                }
            }
        }
    }
}

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

public class MeasurableValue : Value
{
    public MeasurableValue(int value) : base(value)
    {
    }

    public override string ToString()
    {
        return _value.ToString(CultureInfo.CurrentCulture);
    }
}

public class PricelessValue : Value
{
    public PricelessValue() : base(int.MaxValue)
    {
    }
}

public class NoValue : Value
{
    public NoValue() : base(0)
    {
    }
}