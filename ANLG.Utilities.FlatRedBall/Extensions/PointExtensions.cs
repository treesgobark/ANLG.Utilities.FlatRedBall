using FrbPoint = FlatRedBall.Math.Geometry.Point;
using MgVector2 = Microsoft.Xna.Framework.Vector2;

namespace ANLG.Utilities.FlatRedBall.Extensions;

///
public static class PointExtensions
{
    /// <summary>
    /// Allows deconstruction into (double x, double y)
    /// </summary>
    public static void Deconstruct(this FrbPoint input, out double x, out double y)
    {
        x = input.X;
        y = input.Y;
    }
    
    /// <summary>
    /// Allows deconstruction into (double x, double y)
    /// </summary>
    public static void Deconstruct(this FrbPoint? input, out double x, out double y)
    {
        if (input is null)
        {
            x = 0;
            y = 0;
            return;
        }
        
        x = input.Value.X;
        y = input.Value.Y;
    }
    
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
