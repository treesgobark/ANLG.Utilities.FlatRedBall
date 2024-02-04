using FlatRedBall;
using Microsoft.Xna.Framework;

namespace ANLG.Utilities.FlatRedBall.Extensions;

public static class PositionedObjectExtensions
{
    public static Vector3 GetForwardVector3(this PositionedObject obj)
    {
        return Vector2ExtensionMethods.FromAngle(obj.RotationZ).ToVec3();
    }
    
    public static Vector2 GetForwardVector2(this PositionedObject obj)
    {
        return Vector2ExtensionMethods.FromAngle(obj.RotationZ);
    }
}