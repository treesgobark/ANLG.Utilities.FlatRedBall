namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

public readonly struct Rotation : IComparable, IComparable<Rotation>, IEquatable<Rotation>
{
    public const float Epsilon = 0.01f;
    public const float DegreesToRadians = MathF.PI / 180f;
    public const float RadiansToDegrees = 180f / MathF.PI;
    
    private readonly float _radians;

    private Rotation(float radians)
    {
        _radians = radians;
    }

    public static Rotation FromRadians(float radians) => new(radians);
    public static Rotation FromDegrees(float degrees) => new(degrees * DegreesToRadians);
    
    public float TotalRadians => _radians;
    public float TotalDegrees => _radians * RadiansToDegrees;
    public float Radians => Regulate(_radians, MathF.PI * 2f);
    public float Degrees => Regulate(_radians, MathF.PI * 2f) * RadiansToDegrees;
    
    #region Operators
    
    public static Rotation operator +(Rotation r1, Rotation r2)
    {
        return new(r1._radians + r2._radians);
    }
    
    public static Rotation operator -(Rotation r1, Rotation r2)
    {
        return new(r1._radians - r2._radians);
    }
    
    public static Rotation operator *(Rotation r1, Rotation r2)
    {
        return new(r1._radians * r2._radians);
    }
    
    public static Rotation operator /(Rotation r1, Rotation r2)
    {
        return new(r1._radians / r2._radians);
    }
    
    #endregion
    
    /// <summary>
    /// Takes a value <paramref name="x"/> and regulates it to the range 0 &lt;= <paramref name="x"/> &lt; <paramref name="m"/>
    ///   such that it cycles through the range. Equivalent to a mathematical modulus.
    ///   Will give equivalent values to the '%' operator for non-negative numbers.
    /// </summary>
    private static float Regulate(float x, float m)
    {
        float r = x % m;
        return r < 0 ? r + m : r;
    }
    
    public int CompareTo(object? value)
    {
        if (value == null) return 1;
        
        if (value is not Rotation rotation)
        {
            throw new ArgumentException($"Cannot compare rotations to non-rotations. Provided type: {value.GetType().Name}");
        }
        
        return CompareTo(rotation);
    }

    public int CompareTo(Rotation other)
    {
        float t = other._radians;
        
        if (_radians > t)
        {
            return 1;
        }
        
        if (_radians < t)
        {
            return -1;
        }
        
        return 0;
    }

    public bool Equals(Rotation other)
    {
        return float.Abs(_radians - other._radians) < Epsilon;
    }

    public bool Equals(Rotation other, float epsilon)
    {
        return float.Abs(_radians - other._radians) < epsilon;
    }
}