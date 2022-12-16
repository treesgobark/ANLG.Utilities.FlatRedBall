namespace ANLG.Utilities.Constants;

/// <summary>
/// A collection of constants commonly used for mathematics and physics.
/// </summary>
public static class MathConstants
{
    /// <summary>
    /// The approximate real-world acceleration due to gravity on Earth.
    /// </summary>
    /// <remarks>meters/(seconds^2)</remarks>
    public const float GravityAcceleration = -9.81f;
    
    /// <summary>
    /// The real-world gravitational constant. It's the big G in Newton's Law of Universal Gravitation.
    /// </summary>
    /// <remarks>Newtons*meters^2/(kilograms^2)</remarks>
    public const float GravitationalConstant = .0000000000667408f;
    
    /// <summary>
    /// The real-world density of air at sea level.
    /// </summary>
    /// <remarks>kilograms/(meters^3)</remarks>
    public const float AirDensity = 1.225f; // real world value: 1.225 kg/m^3

    /// <summary>
    /// Counter-clockwise rotation is represented by a positive value.
    /// </summary>
    /// <example><see cref="RotateCcw"/> * <see cref="QuarterTurn"/> is the value of a 90 degree counter-clockwise rotation in radians.</example>
    public const int RotateCcw = 1;

    /// <summary>
    /// Clockwise rotation is represented by a negative value.
    /// </summary>
    /// <example><see cref="RotateCw"/> * <see cref="EighthTurn"/> is the value of a 45 degree clockwise rotation in radians.</example>
    public const int RotateCw = -1;

    /// <summary>
    /// Represents a full 360-degree rotation in radians.
    /// </summary>
    public const float FullTurn = 2 * MathF.PI;
    
    /// <summary>
    /// Represents a 270-degree rotation in radians.
    /// </summary>
    public const float ThreeQuartersTurn = 3 * MathF.PI / 2;
    
    /// <summary>
    /// Represents a 180-degree rotation in radians.
    /// </summary>
    public const float HalfTurn = MathF.PI;
    
    /// <summary>
    /// Represents a 90-degree rotation in radians.
    /// </summary>
    public const float QuarterTurn = MathF.PI / 2;
    
    /// <summary>
    /// Represents a 45-degree rotation in radians.
    /// </summary>
    public const float EighthTurn = MathF.PI / 4;
}
