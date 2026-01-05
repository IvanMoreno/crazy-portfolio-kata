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

    // Complicated boolean expression
    // Magic literal
    // Duplicated code
    // Long Method
    public void UpdateValue(DateTime now) {
        if (Description == "French Wine") {
            if (Date.Subtract(now).TotalDays < 0) {
                if (Value.Get() < 200) 
                    Value = new MeasurableValue(Value.Get() + 20);
            }
            else {
                if (Value.Get() < 200) 
                    Value = new MeasurableValue(Value.Get() + 10);
            }
        }
        else {
            if (Description == "Lottery Prediction") {
                NewMethod(now);
            }
            else {
                NewMethod(now);
            }
        }
    }

    void NewMethod(DateTime now) {
        if (Date.Subtract(now).TotalDays < 0) {
            if (Description != "Lottery Prediction") {
                if (Value.Get() > 0) {
                    Value = new MeasurableValue(Value.Get() - 20);
                }
            }
            else {
                Value = new MeasurableValue(Value.Get() - Value.Get());
            }
        }
        else {
            if (Description == "Lottery Prediction") {
                if (Description == "Lottery Prediction") {
                    if (Value.Get() < 800) {
                        Value = new MeasurableValue(Value.Get() + 5);

                        if (Date.Subtract(now).TotalDays < 11)
                            if (Value.Get() < 800)
                                Value = new MeasurableValue(Value.Get() + 20);

                        if (Date.Subtract(now).TotalDays < 6)
                            if (Value.Get() < 800)
                                Value = new MeasurableValue(Value.Get() + 100);
                    }
                }
                else {
                    if (Value.Get() < 200) Value = new MeasurableValue(Value.Get() + 10);
                }
            }
            else {
                if (Value.Get() > 0.0) {
                    Value = new MeasurableValue(Value.Get() - 10);
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