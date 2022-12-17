using ANLG.Utilities.FlatRedBall.Constants;
using FrbPoint = FlatRedBall.Math.Geometry.Point;
using MgVector2 = Microsoft.Xna.Framework.Vector2;
using MgVector3 = Microsoft.Xna.Framework.Vector3;
using MgMatrix = Microsoft.Xna.Framework.Matrix;

namespace ANLG.Utilities.FlatRedBall.Extensions;

///
public static class MgVector2Extensions
{
    /// <summary>
    /// Allows deconstruction into (float x, float y)
    /// </summary>
    public static void Deconstruct(this MgVector2 input, out float x, out float y)
    {
        x = input.X;
        y = input.Y;
    }
    
    /// <summary>
    /// Allows deconstruction into (float x, float y)
    /// </summary>
    public static void Deconstruct(this MgVector2? input, out float x, out float y)
    {
        if (input is null)
        {
            x = 0f;
            y = 0f;
            return;
        }
        
        x = input.Value.X;
        y = input.Value.Y;
    }
    
    /// <summary>
    /// Adds a dimension to a <see cref="MgVector2"/>.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="z"></param>
    /// <returns>A new <see cref="MgVector3"/> of the form { <paramref name="input"/>.X, <paramref name="input"/>.Y, <paramref name="z"/> }</returns>
    public static MgVector3 ToVec3(this MgVector2 input, float z = 0f)
    {
        return new MgVector3(input, z);
    }

    /// <summary>
    /// Returns the slope of this vector: Y/X
    /// </summary>
    public static float GetSlope(this MgVector2 input)
    {
        return input.Y / input.X;
    }

    /// <summary>
    /// Returns which quadrant this vector lies in. Top-right quadrant is 1, and they ascend counter-clockwise through 4.
    ///   Vectors on the X or Y axis return 0.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static int GetQuadrant(this MgVector2 input)
    {
        return input switch
        {
            { X: 0 } or { Y: 0 } => 0,
            { X: > 0, Y: > 0 } => 1,
            { X: < 0, Y: > 0 } => 2,
            { X: < 0, Y: < 0 } => 3,
            { X: > 0, Y: < 0 } => 4,
            _ => throw new Exception("This should be unreachable."),
        };
    }

    /// <summary>
    /// Returns the vector representation of the point in the given list
    ///   which is closest to this vector representation of a point.
    /// </summary>
    public static MgVector2 GetClosestPoint(this MgVector2 input, MgVector2[] otherPoints)
    {
        float lowestDist = float.MaxValue;
        MgVector2 closestPoint = input;
        for (int i = 0; i < otherPoints.Length; i++)
        {
            var thisDist = MgVector2.DistanceSquared(input, otherPoints[i]);
            if (thisDist <= lowestDist)
            {
                closestPoint = otherPoints[i];
                lowestDist = thisDist;
            }
        }
        return closestPoint;
    }

    /// <summary>
    /// Returns the angle of this vector where 0 is the X unit vector. Ranges from 0 to 2*pi
    /// </summary>
    public static float GetCcwAngle(this MgVector2 input)
    {
        var quadrant = GetQuadrant(input);
        if (quadrant is 1 or 2)
        {
            return MathF.Acos(MgVector2.Dot(MgVector2.UnitX, input) / input.Length());
        }
        return MathConstants.FullTurn - MathF.Acos(MgVector2.Dot(MgVector2.UnitX, input) / input.Length());
    }

    /// <summary>
    /// Returns the angle of this vector where 0 is the X unit vector. Ranges from -pi to pi
    /// </summary>
    public static float GetClosestAngle(this MgVector2 input)
    {
        var quadrant = GetQuadrant(input);
        if (quadrant is 1 or 2)
        {
            return MathF.Acos(MgVector2.Dot(MgVector2.UnitX, input) / input.Length());
        }
        return -MathF.Acos(MgVector2.Dot(MgVector2.UnitX, input) / input.Length());
    }

    /// <summary>
    /// Increases the magnitude of a vector by the specified amount without changing its direction and returns the result as a new vector.
    ///   Does not mutate <paramref name="input"/>.
    /// </summary>
    public static MgVector2 Extend(this MgVector2 input, float distance)
    {
        var length = input.Length();
        return input.Scale(1 + distance / length, 1 + distance / length);
    }

    #region Transforms

    /// <summary>
    /// Applies a transformation matrix to a vector. Wrapper for <see cref="MgVector2.Transform(MgVector2, MgMatrix)"/>.
    /// </summary>
    public static MgVector2 Transform(this MgVector2 input1, MgMatrix input2)
    {
        return MgVector2.Transform(input1, input2);
    }

    /// <summary>
    /// Translates a Vector2 by the specified amounts on the 2 axes.
    /// </summary>
    /// <returns>A new MgVector2 of the form { <paramref name="input1"/>.X + <paramref name="x"/>,
    ///   <paramref name="input1"/>.Y + <paramref name="y"/> }</returns>
    public static MgVector2 Translate(this MgVector2 input1, float x = 0f, float y = 0f)
    {
        return new MgVector2(input1.X + x, input1.Y + y);
    }

    /// <summary>
    /// Scales a Vector2 by the specified amounts on the 2 axes.
    /// </summary>
    /// <returns>A new MgVector2 of the form { <paramref name="input1"/>.X * <paramref name="x"/>,
    ///   <paramref name="input1"/>.Y * <paramref name="y"/> }</returns>
    public static MgVector2 Scale(this MgVector2 input1, float x = 1f, float y = 1f)
    {
        return new MgVector2(input1.X * x, input1.Y * y);
    }

    /// <summary>
    /// Rotates a vector by <paramref name="radians"/>.
    /// </summary>
    /// <returns>A new MgVector2 that has been rotated.</returns>
    public static MgVector2 Rotate(this MgVector2 input1, float radians)
    {
        return input1.Transform(MgMatrix.CreateRotationZ(radians));
    }

    /// <summary>
    /// Normalizes a vector, i.e. sets this vector's magnitude to 0 while preserving its direction.
    /// </summary>
    /// <param name="input1"></param>
    /// <returns>A MgVector2 with magnitude 1 if the input vector's magnitude is nonzero. Returns the zero vector otherwise.</returns>
    public static MgVector2 NormalizeExtension(this MgVector2 input1)
    {
        var magnitude = input1.Length();
        if (magnitude == 0)
        {
            return MgVector2.Zero;
        }
        return input1 / magnitude;
    }

    /// <summary>
    /// Applies a given func to each component of this vector and returns the result.
    /// </summary>
    /// <param name="inputMgVector"></param>
    /// <param name="mutator"></param>
    /// <returns></returns>
    public static MgVector2 MutatePiecewise(this MgVector2 inputMgVector, Func<float, float> mutator)
    {
        return new MgVector2(mutator(inputMgVector.X), mutator(inputMgVector.Y));
    }

    /// <summary>
    /// Moves this vector right the specified amount and returns the result.
    /// </summary>
    /// <param name="inputMgVector"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static MgVector2 Right(this MgVector2 inputMgVector, float amount)
    {
        return inputMgVector with { X = inputMgVector.X + amount};
    }

    /// <summary>
    /// Moves this vector left the specified amount and returns the result.
    /// </summary>
    /// <param name="inputMgVector"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static MgVector2 Left(this MgVector2 inputMgVector, float amount)
    {
        return inputMgVector with { X = inputMgVector.X - amount };
    }

    /// <summary>
    /// Moves this vector up the specified amount and returns the result.
    /// </summary>
    /// <param name="inputMgVector"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static MgVector2 Up(this MgVector2 inputMgVector, float amount)
    {
        return inputMgVector with { Y = inputMgVector.Y + amount };
    }

    /// <summary>
    /// Moves this vector down the specified amount and returns the result.
    /// </summary>
    /// <param name="inputMgVector"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static MgVector2 Down(this MgVector2 inputMgVector, float amount)
    {
        return inputMgVector with { Y = inputMgVector.Y - amount };
    }

    #endregion

    #region Arithmetic: MgVector3

    /// <summary>
    /// Adds a MgVector3 to this MgVector2. The Z component of the MgVector3 is ignored.
    /// </summary>
    /// <returns>A new MgVector3 of the form { <paramref name="input1"/>.X + <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y + <paramref name="input2"/>.Y, <paramref name="input2"/>.Z }</returns>
    public static MgVector2 Add(this MgVector2 input1, MgVector3 input2)
    {
        return new MgVector2(input1.X + input2.X, input1.Y + input2.Y);
    }

    /// <summary>
    /// Multiplies a MgVector3 by this MgVector2. The Z component of the MgVector3 is ignored.
    /// </summary>
    /// <returns>A new MgVector3 of the form { <paramref name="input1"/>.X * <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y * <paramref name="input2"/>.Y, <paramref name="input2"/>.Z }</returns>
    public static MgVector2 Multiply(this MgVector2 input1, MgVector3 input2)
    {
        return new MgVector2(input1.X * input2.X, input1.Y * input2.Y);
    }

    /// <summary>
    /// Subtracts a MgVector3 from this MgVector2.  The Z component of the MgVector3 is ignored.
    /// </summary>
    /// <returns>A new MgVector3 of the form { <paramref name="input1"/>.X - <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y - <paramref name="input2"/>.Y, 0f - <paramref name="input2"/>.Z }</returns>
    public static MgVector2 Subtract(this MgVector2 input1, MgVector3 input2)
    {
        return new MgVector2(input1.X - input2.X, input1.Y - input2.Y);
    }

    /// <summary>
    /// Divides this MgVector2 by a MgVector3. The Z component of the MgVector3 is ignored.
    /// </summary>
    /// <returns>A new MgVector3 of the form { <paramref name="input1"/>.X / <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y / <paramref name="input2"/>.Y, 1f / <paramref name="input2"/>.Z }</returns>
    public static MgVector2 Divide(this MgVector2 input1, MgVector3 input2)
    {
        return new MgVector2(input1.X / input2.X, input1.Y / input2.Y);
    }

    #endregion

    #region Arithmetic: MgVector2

    /// <summary>
    /// Adds another vector to this one per-component. Equivalent to vector + vector
    /// </summary>
    /// <returns>A new MgVector2 of the form { <paramref name="input1"/>.X + <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X + <paramref name="input2"/>.Y }</returns>
    public static MgVector2 Add(this MgVector2 input1, MgVector2 input2)
    {
        return new MgVector2(input1.X + input2.X, input1.Y + input2.Y);
    }

    /// <summary>
    /// Multiplies (does not perform dot product or cross product) this vector by another one per-component.
    /// </summary>
    /// <returns>A new MgVector2 of the form { <paramref name="input1"/>.X * <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X * <paramref name="input2"/>.Y }</returns>
    public static MgVector2 Multiply(this MgVector2 input1, MgVector2 input2)
    {
        return new MgVector2(input1.X * input2.X, input1.Y * input2.Y);
    }

    /// <summary>
    /// Subtracts another vector from this vector per-component. Equivalent to vector - vector
    /// </summary>
    /// <returns>A new MgVector2 of the form { <paramref name="input1"/>.X - <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X - <paramref name="input2"/>.Y }</returns>
    public static MgVector2 Subtract(this MgVector2 input1, MgVector2 input2)
    {
        return new MgVector2(input1.X - input2.X, input1.Y - input2.Y);
    }

    /// <summary>
    /// Divides this vector by another one per-component.
    /// </summary>
    /// <returns>A new MgVector2 of the form { <paramref name="input1"/>.X / <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X / <paramref name="input2"/>.Y }</returns>
    public static MgVector2 Divide(this MgVector2 input1, MgVector2 input2)
    {
        return new MgVector2(input1.X / input2.X, input1.Y / input2.Y);
    }

    #endregion

    #region Arithmetic: Point

    /// <summary>
    /// Adds a point to this vector per-component.
    /// </summary>
    /// <returns>A new MgVector2 of the form { <paramref name="input1"/>.X + <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X + <paramref name="input2"/>.Y }</returns>
    public static MgVector2 Add(this MgVector2 input1, FrbPoint input2)
    {
        return new MgVector2(input1.X + (float)input2.X, input1.Y + (float)input2.Y);
    }

    /// <summary>
    /// Multiplies this vector by a point per-component.
    /// </summary>
    /// <returns>A new MgVector2 of the form { <paramref name="input1"/>.X * <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X * <paramref name="input2"/>.Y }</returns>
    public static MgVector2 Multiply(this MgVector2 input1, FrbPoint input2)
    {
        return new MgVector2(input1.X * (float)input2.X, input1.Y * (float)input2.Y);
    }

    /// <summary>
    /// Subtracts a point from this vector per-component.
    /// </summary>
    /// <returns>A new MgVector2 of the form { <paramref name="input1"/>.X - <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X - <paramref name="input2"/>.Y }</returns>
    public static MgVector2 Subtract(this MgVector2 input1, FrbPoint input2)
    {
        return new MgVector2(input1.X - (float)input2.X, input1.Y - (float)input2.Y);
    }

    /// <summary>
    /// Divides this vector by another one per-component.
    /// </summary>
    /// <returns>A new MgVector2 of the form { <paramref name="input1"/>.X / <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X / <paramref name="input2"/>.Y }</returns>
    public static MgVector2 Divide(this MgVector2 input1, FrbPoint input2)
    {
        return new MgVector2(input1.X / (float)input2.X, input1.Y / (float)input2.Y);
    }

    #endregion
}
