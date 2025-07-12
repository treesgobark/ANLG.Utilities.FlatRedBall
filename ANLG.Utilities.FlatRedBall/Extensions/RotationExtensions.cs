using ANLG.Utilities.Core;
using Microsoft.Xna.Framework;

namespace ANLG.Utilities.FlatRedBall.Extensions;

public static class RotationExtensions
{
    /// <summary>
    /// Converts a rotation into a vector 2 of unit length.
    /// </summary>
    public static Vector2 ToVector2(this Rotation rotation)
    {
        return Vector2.UnitX.RotatedBy(rotation.NormalizedRadians);
    }
    
    /// <summary>
    /// Converts a rotation into a vector 2 of unit length, then appends a Z component to it.
    /// </summary>
    public static Vector3 ToVector3(this Rotation rotation, float z = 0f)
    {
        return Vector2.UnitX.RotatedBy(rotation.NormalizedRadians).ToVec3(z);
    }
}