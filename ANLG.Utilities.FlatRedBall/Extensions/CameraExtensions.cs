using FrbCamera = FlatRedBall.Camera;
using MgVector2 = Microsoft.Xna.Framework.Vector2;
namespace ANLG.Utilities.FlatRedBall.Extensions;

/// 
public static class CameraExtensions
{
    /// <summary>
    /// Returns the four absolute edges of this camera in the form of (Vector2(left, bottom), Vector2(right, top)).
    /// </summary>
    public static (MgVector2 minEdges, MgVector2 maxEdges) AbsoluteEdgesAt(this FrbCamera camera, float absoluteZ = 0)
    {
        return (
            new MgVector2(camera.AbsoluteLeftXEdgeAt(absoluteZ), camera.AbsoluteBottomYEdgeAt(absoluteZ)),
            new MgVector2(camera.AbsoluteRightXEdgeAt(absoluteZ), camera.AbsoluteTopYEdgeAt(absoluteZ))
            );
    }
    
    /// <summary>
    /// Returns the four absolute corners of this camera in vectors in the form of (topLeft, topRight, bottomLeft, bottomRight).
    /// </summary>
    public static (MgVector2 topLeft, MgVector2 topRight, MgVector2 bottomLeft, MgVector2 bottomRight) AbsoluteCornersAt(this FrbCamera camera, float absoluteZ = 0)
    {
        var left = camera.AbsoluteLeftXEdgeAt(absoluteZ);
        var bottom = camera.AbsoluteBottomYEdgeAt(absoluteZ);
        var right = camera.AbsoluteRightXEdgeAt(absoluteZ);
        var top = camera.AbsoluteTopYEdgeAt(absoluteZ);
        
        return (
            new MgVector2(left, top),
            new MgVector2(right, top),
            new MgVector2(left, bottom),
            new MgVector2(right, bottom)
            );
    }
}
