using System.Globalization;

namespace Portfolio;

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

    public Value Add(Value addend) {
        return Measurable(Get() + addend.Get());
    }
}