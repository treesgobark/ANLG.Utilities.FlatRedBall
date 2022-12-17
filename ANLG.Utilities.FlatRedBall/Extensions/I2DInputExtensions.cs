using FlatRedBall.Input;
using MgVector2 = Microsoft.Xna.Framework.Vector2;

namespace ANLG.Utilities.FlatRedBall.Extensions;

/// 
public static class I2DInputExtensions
{
    /// <summary>
    /// Allows deconstruction into (Vector2 position, Vector2 velocity, float magnitude)
    /// </summary>
    public static void Deconstruct(this I2DInput? input, out MgVector2 position, out MgVector2 velocity, out float magnitude)
    {
        if (input is null)
        {
            position = MgVector2.Zero;
            velocity = MgVector2.Zero;
            magnitude = 0f;
            return;
        }
        
        position = new MgVector2(input.X, input.Y);
        velocity = new MgVector2(input.XVelocity, input.YVelocity);
        magnitude = input.Magnitude;
    }

    /// <summary>
    /// Returns the normalized position of this input using I2DInput's X and Y properties.
    ///   Returns <see cref="MgVector2.Zero"/> if magnitude is zero or if input is null.
    /// </summary>
    public static MgVector2 GetNormalizedPositionOrZero(this I2DInput? input)
    {
        return input switch
        {
            null or { Magnitude: 0 } => MgVector2.Zero,
            _ => new MgVector2(input.X, input.Y) / input.Magnitude,
        };
    }
}
