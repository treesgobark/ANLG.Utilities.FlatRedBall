using FrbPoint = FlatRedBall.Math.Geometry.Point;
using MgVector2 = Microsoft.Xna.Framework.Vector2;

namespace ANLG.Utilities.Extensions;

///
public static class PointExtensions
{
    #region Arithmetic: FrbPoint

    /// <summary>
    /// Add another point to this one per-component.
    /// </summary>
    /// <returns>A new Point of the form { <paramref name="input1"/>.X + <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X + <paramref name="input2"/>.Y }</returns>
    public static FrbPoint Add(this FrbPoint input1, FrbPoint input2)
    {
        return new FrbPoint(input1.X + input2.X, input1.Y + input2.Y);
    }

    /// <summary>
    /// Multiply this point by another one per-component.
    /// </summary>
    /// <returns>A new Point of the form { <paramref name="input1"/>.X * <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X * <paramref name="input2"/>.Y }</returns>
    public static FrbPoint Multiply(this FrbPoint input1, FrbPoint input2)
    {
        return new FrbPoint(input1.X * input2.X, input1.Y * input2.Y);
    }

    /// <summary>
    /// Subtract another point from this one per-component.
    /// </summary>
    /// <returns>A new Point of the form { <paramref name="input1"/>.X - <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X - <paramref name="input2"/>.Y }</returns>
    public static FrbPoint Subtract(this FrbPoint input1, FrbPoint input2)
    {
        return new FrbPoint(input1.X - input2.X, input1.Y - input2.Y);
    }

    /// <summary>
    /// Divide this point by another one per-component.
    /// </summary>
    /// <returns>A new Point of the form { <paramref name="input1"/>.X / <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X / <paramref name="input2"/>.Y }</returns>
    public static FrbPoint Divide(this FrbPoint input1, FrbPoint input2)
    {
        return new FrbPoint(input1.X / input2.X, input1.Y / input2.Y);
    }

    #endregion

    #region Arithmetic: MgVector2

    /// <summary>
    /// Add a vector to this point per-component.
    /// </summary>
    /// <returns>A new Point of the form { <paramref name="input1"/>.X + <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X + <paramref name="input2"/>.Y }</returns>
    public static FrbPoint Add(this FrbPoint input1, MgVector2 input2)
    {
        return new FrbPoint(input1.X + input2.X, input1.Y + input2.Y);
    }

    /// <summary>
    /// Multiply this point by a vector per-component.
    /// </summary>
    /// <returns>A new Point of the form { <paramref name="input1"/>.X * <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X * <paramref name="input2"/>.Y }</returns>
    public static FrbPoint Multiply(this FrbPoint input1, MgVector2 input2)
    {
        return new FrbPoint(input1.X * input2.X, input1.Y * input2.Y);
    }

    /// <summary>
    /// Subtract a vector from this point per-component.
    /// </summary>
    /// <returns>A new Point of the form { <paramref name="input1"/>.X - <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X - <paramref name="input2"/>.Y }</returns>
    public static FrbPoint Subtract(this FrbPoint input1, MgVector2 input2)
    {
        return new FrbPoint(input1.X - input2.X, input1.Y - input2.Y);
    }

    /// <summary>
    /// Divide this point by a vector per-component.
    /// </summary>
    /// <returns>A new Point of the form { <paramref name="input1"/>.X / <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X / <paramref name="input2"/>.Y }</returns>
    public static FrbPoint Divide(this FrbPoint input1, MgVector2 input2)
    {
        return new FrbPoint(input1.X / input2.X, input1.Y / input2.Y);
    }

    #endregion
}
