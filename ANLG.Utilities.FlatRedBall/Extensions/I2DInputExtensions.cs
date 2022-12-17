using FlatRedBall.Input;
using MgVector2 = Microsoft.Xna.Framework.Vector2;

namespace ANLG.Utilities.FlatRedBall.Extensions;

/// 
public static class I2DInputExtensions
{
    /// <summary>
    /// Allows deconstruction into (Vector2 position, Vector2 velocity, float magnitude)
    /// </summary>
    public static void Deconstruct(this I2DInput input, out MgVector2 position, out MgVector2 velocity, out float magnitude)
    {
        position = new MgVector2(input.X, input.Y);
        velocity = new MgVector2(input.XVelocity, input.YVelocity);
        magnitude = input.Magnitude;
    }
}
