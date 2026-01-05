using System.Globalization;

namespace Portfolio;

// Data Class
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

    public void UpdateValue(DateTime now) {
        if (this.Date.Subtract(now).TotalDays < 0)
        {
            if (this.Description != "French Wine")
            {
                if (this.Description != "Lottery Prediction")
                {
                    if (this.Value.Get() > 0) {
                        this.Value = new MeasurableValue(this.Value.Get() - 20);
                    }
                }
                else
                {
                    this.Value = new MeasurableValue(this.Value.Get() - this.Value.Get());
                }
            }
            else
            {
                if (this.Value.Get() < 200) this.Value = new MeasurableValue(this.Value.Get() + 20);
            }
        }
        else
        {
            if (this.Description != "French Wine" && this.Description != "Lottery Prediction")
            {
                if (this.Value.Get() > 0.0) {
                    this.Value = new MeasurableValue(this.Value.Get() - 10);
                }
            }
            else
            {
                if (this.Description == "Lottery Prediction")
                {
                    if (this.Value.Get() < 800)
                    {
                        this.Value = new MeasurableValue(this.Value.Get() + 5);

                        if (this.Date.Subtract(now).TotalDays < 11)
                            if (this.Value.Get() < 800)
                                this.Value = new MeasurableValue(this.Value.Get() + 20);

                        if (this.Date.Subtract(now).TotalDays < 6)
                            if (this.Value.Get() < 800)
                                this.Value = new MeasurableValue(this.Value.Get() + 100);
                    }
                }
                else
                {
                    if (this.Value.Get() < 200) this.Value = new MeasurableValue(this.Value.Get() + 10);
                }
            }
        }
    }
}

// Data Class
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

// Dead Code
public class NoValue : Value
{
    public NoValue() : base(0)
    {
    }
}