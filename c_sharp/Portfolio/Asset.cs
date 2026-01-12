using System.Globalization;

namespace Portfolio;

public abstract class Asset
{
    private readonly DateTime _date;

    Asset(string description, DateTime date, Value value)
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

    protected abstract Value ExpiredValue();
    protected abstract Value NonExpiredValue(DateTime now);
    public abstract bool IsUnicorn(DateTime now);

    class FrenchWine : Asset {
        public FrenchWine(string description, DateTime date, Value value) : base(description, date, value) { }

        protected override Value NonExpiredValue(DateTime now) {
            if (Value.Get() < 200)
                return Value.Measurable(Value.Get() + 10);

            return Value;
        }

        public override bool IsUnicorn(DateTime now) {
            return false;
        }

        protected override Value ExpiredValue() {
            if (Value.Get() < 200)
                return Value.Measurable(Value.Get() + 20);

            return Value;
        }
    }

    class LotteryPrediction : Asset {
        public LotteryPrediction(string description, DateTime date, Value value) : base(description, date, value) { }

        protected override Value NonExpiredValue(DateTime now) {
            if (Value.Get() < 800) {
                var baseValue = Value.Measurable(Value.Get() + 5);

                if (Date.Subtract(now).TotalDays < 11)
                    if (baseValue.Get() < 800)
                        baseValue = Value.Measurable(baseValue.Get() + 20);

                if (Date.Subtract(now).TotalDays < 6)
                    if (baseValue.Get() < 800)
                        baseValue = Value.Measurable(baseValue.Get() + 100);

                return baseValue;
            }

            return Value;
        }

        public override bool IsUnicorn(DateTime now) {
            return false;
        }

        protected override Value ExpiredValue() {
            return Value.Measurable(Value.Get() - Value.Get());
        }
    }

    class Unicorn : Asset {
        public Unicorn(string description, DateTime date, Value value) : base(description, date, value) { }

        protected override Value NonExpiredValue(DateTime now) {
            return Value.Priceless();
        }

        public override bool IsUnicorn(DateTime now) {
            if (Expired(now)) {
                return Value.Get() > 0;
            }

            return true;
        }

        protected override Value ExpiredValue() {
            if (Value.Get() > 0) {
                return Value.Priceless();
            }

            return Value;
        }
    }
    
    class Default : Asset {
        public Default(string description, DateTime date, Value value) : base(description, date, value) { }

        protected override Value NonExpiredValue(DateTime now) {
            if (Value.Get() > 0.0) {
                return Value.Measurable(Value.Get() - 10);
            }

            return Value;
        }

        public override bool IsUnicorn(DateTime now) {
            return false;
        }

        protected override Value ExpiredValue() {
            if (Value.Get() > 0) {
                return Value.Priceless();
            }

            return Value;
        }
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
    
    public static Asset Create(string description, DateTime date, Value value) {
        if (description == "French Wine")
            return new FrenchWine(description, date, value);
        
        if (description == "Lottery Prediction") 
            return new LotteryPrediction(description, date, value);

        if (description == "Unicorn")
            return new Unicorn(description, date, value);
        
        return new Default(description, date, value);
    }
}

public class Value
{
    readonly int _value;

    Value(int value)
    {
        _value = value;
    }

    public int Get()
    {
        return _value;
    }

    public static Value Measurable(int value) {
        return new Value(value);
    }

    public static Value Priceless() {
        return new Value(int.MaxValue);
    }
    
    public override string ToString()
    {
        return _value.ToString(CultureInfo.CurrentCulture);
    }
}