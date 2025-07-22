namespace ANLG.Utilities.Core;

/// <summary>
/// 1 > ∞ > -1 > -∞ > 0
/// </summary>
public struct Priority : IComparable<Priority>
{
    private int _value;
    
    public Priority(int value)
    {
        _value = value;
    }
    
    public static implicit operator Priority(int value) => new(value);
    
    private static readonly Priority FirstField = new(1);
    private static readonly Priority LastField = new(0);
    
    public static Priority First => FirstField;
    public static Priority Last => LastField;
    
    public Priority Next => Shift(1);
    public Priority Previous => Shift(-1);

    public Priority Shift(int amount)
    {
        return _value switch
        {
            0   => this,
            > 0 => new Priority(int.Clamp(_value - amount, 1,            int.MaxValue)),
            _   => new Priority(int.Clamp(_value + amount, int.MinValue, -1))
        };
    }

    public int CompareTo(Priority other)
    {
        return (_value, other._value) switch
        {
            (0, 0)               => 0,
            (_, 0) or (> 0, < 0) => -1,
            (0, _) or (< 0, > 0) => 1,
            _                    => int.Abs(_value).CompareTo(int.Abs(other._value))
        };
    }
}