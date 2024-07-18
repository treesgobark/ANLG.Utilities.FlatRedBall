namespace ANLG.Utilities.Core.NonStaticUtilities;

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
    
    private static readonly Rotation ZeroField = new(0f);
    private static readonly Rotation FullTurnField = new(2f * MathF.PI);
    private static readonly Rotation HalfTurnField = new(MathF.PI);
    private static readonly Rotation QuarterTurnField = new(MathF.PI / 2f);
    private static readonly Rotation ThreeQuartersTurnField = new(3f * MathF.PI / 2f);
    private static readonly Rotation EighthTurnField = new(MathF.PI / 4f);
    
    public static Rotation Zero => ZeroField;
    public static Rotation FullTurn => FullTurnField;
    public static Rotation HalfTurn => HalfTurnField;
    public static Rotation QuarterTurn => QuarterTurnField;
    public static Rotation ThreeQuartersTurn => ThreeQuartersTurnField;
    public static Rotation EighthTurn => EighthTurnField;
    
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

    /// <summary>
    /// Snaps this rotation to a number of equidistant angles around a circle equal to <paramref name="angles"/>.
    /// <paramref name="halfAngleOffset"/> rotates the snap positions by half the angle between positions.
    /// </summary>
    public Rotation Snap(int angles, bool halfAngleOffset = false)
    {
        float newAngle;
        
        if (halfAngleOffset)
        {
            newAngle = (float)(double.Floor(_radians / (2 * Math.PI) * angles + Math.PI / angles)
                / angles * (2 * Math.PI));
        }
        else
        {
            newAngle = (float)(double.Floor(_radians / (2 * Math.PI) * angles) / angles * (2 * Math.PI));
        }
        
        return FromRadians(newAngle);
    }

    /// <summary>
    /// Returns the index of the "sector" of the unit circle that the current rotation value is in,
    ///   given the number of sectors.
    /// <paramref name="halfAngleOffset"/> causes the sectors to rotate by half the arc of a sector.
    /// </summary>
    public int GetSector(int sectors, bool halfAngleOffset = false)
    {
        float offset = halfAngleOffset ? MathF.PI / sectors : 0f;
        
        float sector = NormalizePositive(NormalizedRadians + offset) / (2f * MathF.PI / sectors);

        return (int)sector;
    }
}