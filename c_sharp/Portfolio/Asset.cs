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

    public Value GetValue(DateTime now) {
        if (Expired(now)) {
            return ExpiredValue();
        }
        else {
            return NonExpiredValue(now);
        }
    }

    bool Expired(DateTime now) {
        return Date.Subtract(now).TotalDays < 0;
    }

    Value NonExpiredValue(DateTime now) {
        if (Description == "Lottery Prediction") {
            if (Value.Get() < 800) {
                var baseValue = new MeasurableValue(Value.Get() + 5);

                if (Date.Subtract(now).TotalDays < 11)
                    if (baseValue.Get() < 800)
                        baseValue = new MeasurableValue(baseValue.Get() + 20);

                if (Date.Subtract(now).TotalDays < 6)
                    if (baseValue.Get() < 800)
                        baseValue = new MeasurableValue(baseValue.Get() + 100);

                return baseValue;
            }

            return Value;
        }

        if (Description == "French Wine") {
            if (Value.Get() < 200)
                return new MeasurableValue(Value.Get() + 10);
        }

        if (Description == "Unicorn") {
            // Nothing
        }
        else {
            if (Value.Get() > 0.0) {
                return new MeasurableValue(Value.Get() - 10);
            }
        }

        return Value;
    }

    Value ExpiredValue() {
        if (Description != "French Wine")
        {
            if (Description != "Lottery Prediction")
            {
                if (Value.Get() > 0)
                {
                    if (Description != "Unicorn") {
                        return new MeasurableValue(Value.Get() - 20);
                    }
                }
            }
            else {
                return new MeasurableValue(Value.Get() - Value.Get());
            }
        }
        else
        {
            if (Value.Get() < 200) 
                return new MeasurableValue(Value.Get() + 20);
        }

        return Value;
    }

    public bool IsUnicorn(DateTime now) {
        if (Description != "Unicorn") 
            return false;
        
        if (Date.Subtract(now).TotalDays < 0) {
            return Value.Get() > 0;
        }

        return true;
    }

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