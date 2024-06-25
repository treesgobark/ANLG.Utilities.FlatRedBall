using ANLG.Utilities.Core.NonStaticUtilities;
using FlatRedBall.Math;

namespace ANLG.Utilities.FlatRedBall.Extensions;

public static class IRotatableExtensions
{
    public static Rotation GetRotationZ(this IRotatable rotatable)
    {
        return Rotation.FromRadians(rotatable.RotationZ);
    }
}