using MgVector2 = Microsoft.Xna.Framework.Vector2;
using MgRectangle = Microsoft.Xna.Framework.Rectangle;

namespace ANLG.Utilities.FlatRedBall.Extensions;

///
public static class RectangleExtensions
{
    /// <summary>
    /// Returns the vector representation of a rectangle.
    ///   This is the vector from the bottom left point to the top left point of the rectangle
    /// </summary>
    public static MgVector2 ToVec2(this MgRectangle input)
    {
        return new MgVector2(input.Width, input.Height);
    }
}