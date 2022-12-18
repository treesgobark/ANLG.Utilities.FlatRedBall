using FrbCamera = FlatRedBall.Camera;
using MgVector2 = Microsoft.Xna.Framework.Vector2;
namespace ANLG.Utilities.FlatRedBall.Extensions;

public class CameraExtensions
{
    public static (MgVector2 minEdges, MgVector2 maxEdges) AbsoluteEdgesAt(FrbCamera camera, float absoluteZ = 0)
    {
        return (
            new MgVector2(camera.AbsoluteLeftXEdgeAt(absoluteZ), camera.AbsoluteBottomYEdgeAt(absoluteZ)),
            new MgVector2(camera.AbsoluteRightXEdgeAt(absoluteZ), camera.AbsoluteTopYEdgeAt(absoluteZ))
            );
    }
}
