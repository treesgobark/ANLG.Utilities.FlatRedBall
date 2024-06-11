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
    public static readonly Rotation Zero = new(0f);
    
    /// <summary>
    /// Full rotation value in radians, unbound.
    /// </summary>
    public float TotalRadians => _radians;
    
    /// <summary>
    /// Full rotation value in degrees, unbound.
    /// </summary>
    public float TotalDegrees => _radians * RadiansToDegrees;
    
    /// <summary>
    /// Rotation value in radians, normalized to be between 0 and 2 pi.
    /// </summary>
    public float NormalizedRadians => NormalizePositive(_radians);
    
    /// <summary>
    /// Full rotation value in degrees, normalized to be between 0 and 360.
    /// </summary>
    public float NormalizedDegrees => NormalizePositive(_radians) * RadiansToDegrees;
    
    /// <summary>
    /// Full rotation value in radians, normalized to be between -pi and pi.
    /// </summary>
    public float CondensedRadians => NormalizeAroundZero(_radians);
    
    /// <summary>
    /// Full rotation value in degrees, normalized to be between -180 and 180.
    /// </summary>
    public float CondensedDegrees => NormalizeAroundZero(_radians) * RadiansToDegrees;
    
    public bool IsClockwise => _radians < 0;
    
    #region Operators
    
    public static Rotation operator +(Rotation r1, Rotation r2)
    {
        return new(r1._radians + r2._radians);
    }
    
    public static Rotation operator -(Rotation r1, Rotation r2)
    {
        return new(r1._radians - r2._radians);
    }
    
    public static Rotation operator *(float r1, Rotation r2)
    {
        return new(r1 * r2._radians);
    }
    
    public static Rotation operator *(Rotation r1, float r2)
    {
        return new(r1._radians * r2);
    }
    
    public static Rotation operator /(Rotation r1, float r2)
    {
        return new(r1._radians / r2);
    }
    
    #endregion
    
    private static float NormalizePositive(float x)
    {
        float r = x % (MathF.PI * 2f);
        return r < 0 ? r + MathF.PI * 2f : r;
    }

    private static float NormalizeAroundZero(float angle)
    {
        // Convert the angle to the range of -2π to 2π
        angle %= 2 * MathF.PI;

        // Adjust the angle to the range of -π to π
        if (angle > MathF.PI)
            return angle - 2 * MathF.PI;
        if (angle <= -Math.PI)
            return angle + 2 * MathF.PI;

        return angle;
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