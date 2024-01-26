using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Math;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ANLG.Utilities.FlatRedBall.Extensions;

/// 
public static class I2DInputExtensions
{
    /// <summary>
    /// Allows deconstruction into (Vector2 position, Vector2 velocity, float magnitude)
    /// </summary>
    public static void Deconstruct(this I2DInput? input, out Vector2 position, out Vector2 velocity, out float magnitude)
    {
        if (input is null)
        {
            position = Vector2.Zero;
            velocity = Vector2.Zero;
            magnitude = 0f;
            return;
        }
        
        position = new Vector2(input.X, input.Y);
        velocity = new Vector2(input.XVelocity, input.YVelocity);
        magnitude = input.Magnitude;
    }

    /// <summary>
    /// Returns the normalized position of this input using I2DInput's X and Y properties.
    ///   Returns <see cref="Vector2.Zero"/> if magnitude is zero or if input is null.
    /// </summary>
    public static Vector2 GetNormalizedPositionOrZero(this I2DInput? input)
    {
        return input switch
        {
            null or { Magnitude: 0 } => Vector2.Zero,
            _ => new Vector2(input.X, input.Y) / input.Magnitude,
        };
    }

    /// <summary>
    /// Returns the angle in radians of the input object, where 0 is to the right, rotating counterclockwise.
    /// Returns null if the X and Y values are 0 (meaning the input device is centered)
    /// </summary>
    /// <param name="instance">The I2DInput instance</param>
    /// <returns>The angle, or null if X and Y are 0</returns>
    public static float GetAngleOrZero(this I2DInput instance)
    {
        if(instance.X == 0 && instance.Y == 0)
        {
            return 0;
        }
        else
        {
            return MathFunctions.NormalizeAngle((float)Math.Atan2(instance.Y, instance.X));
        }
    }
}
