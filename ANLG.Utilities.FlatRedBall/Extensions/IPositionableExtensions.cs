using FlatRedBall.Math;
using Microsoft.Xna.Framework;

namespace ANLG.Utilities.FlatRedBall.Extensions;

public static class IPositionableExtensions
{
    public static Vector3 PositionAsVec3(this IPositionable positionable)
    {
        return new Vector3(positionable.X, positionable.Y, positionable.Z);
    }
    
    public static Vector3 VelocityAsVec3(this IPositionable positionable)
    {
        return new Vector3(positionable.XVelocity, positionable.YVelocity, positionable.ZVelocity);
    }
    
    public static Vector3 AccelerationAsVec3(this IPositionable positionable)
    {
        return new Vector3(positionable.XAcceleration, positionable.YAcceleration, positionable.ZAcceleration);
    }
}