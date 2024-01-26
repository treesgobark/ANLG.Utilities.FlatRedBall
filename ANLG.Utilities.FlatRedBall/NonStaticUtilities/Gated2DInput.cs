using ANLG.Utilities.FlatRedBall.Extensions;
using FlatRedBall.Input;
using Microsoft.Xna.Framework;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

/// <summary>
/// Gates input direction to some number of discrete angles, starting with 0 radians.
///   Typically used to force analog sticks to only move in 4 or 8 directions. 
/// </summary>
public class Gated2DInput(I2DInput input, int angles) : I2DInput
{
    public float X => input
        .Position2D()
        .AtAngle(GatedAngle)
        .X;

    public float Y => input
        .Position2D()
        .AtAngle(GatedAngle)
        .Y;

    public float XVelocity => 64;
    public float YVelocity => 64;
    public float Magnitude => input.Magnitude;

    private float InputAngle => input.GetAngle() ?? 0;

    private float GatedAngle => float.Floor(InputAngle / MathHelper.TwoPi * angles + MathHelper.Pi / angles) / angles * MathHelper.TwoPi;
}