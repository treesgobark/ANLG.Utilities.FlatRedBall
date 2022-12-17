using ANLG.Utilities.FlatRedBall.Extensions;
using MgRectangle = Microsoft.Xna.Framework.Rectangle;
using MgVector2 = Microsoft.Xna.Framework.Vector2;

namespace ANLG.Utilities.FlatRedBall.Tests.Extensions;

[TestFixture]
public class RectangleExtensionTests
{
    [Test]
    public void ToVec2_Rectangle_ReturnsVector2()
    {
        Assert.That(new MgVector2(4, 3), Is.EqualTo(new MgRectangle(-1, 1, 4, 3).ToVec2()));
    }
}
