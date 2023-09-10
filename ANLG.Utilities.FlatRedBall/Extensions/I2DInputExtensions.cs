using FlatRedBall;
using FlatRedBall.Input;
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
}
