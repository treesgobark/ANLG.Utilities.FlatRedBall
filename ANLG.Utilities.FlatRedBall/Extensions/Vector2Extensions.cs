using ANLG.Utilities.FlatRedBall.Constants;
using MathHelper = Microsoft.Xna.Framework.MathHelper;
using FrbPoint = FlatRedBall.Math.Geometry.Point;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Vector4 = Microsoft.Xna.Framework.Vector4;
using MgMatrix = Microsoft.Xna.Framework.Matrix;
using static Microsoft.Xna.Framework.Vector2ExtensionMethods;

namespace ANLG.Utilities.FlatRedBall.Extensions;

///
public static class Vector2Extensions
{
    // /// <summary>
    // /// Allows deconstruction into (float x, float y)
    // /// </summary>
    // public static void Deconstruct(this Vector2 input, out float x, out float y)
    // {
    //     x = input.X;
    //     y = input.Y;
    // }
    
    /// <summary>
    /// Allows deconstruction into (float x, float y)
    /// </summary>
    public static void Deconstruct(this Vector2? input, out float x, out float y)
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
    /// Adds a dimension to a <see cref="Vector2"/>.
    /// </summary>
    /// <returns>A new <see cref="Vector3"/> of the form { <paramref name="input"/>.X, <paramref name="input"/>.Y, <paramref name="z"/> }</returns>
    public static Vector3 ToVec3(this Vector2 input, float z = 0f)
    {
        return new Vector3(input, z);
    }
    
    /// <summary>
    /// Adds two dimensions to a <see cref="Vector2"/>.
    /// </summary>
    /// <returns>A new <see cref="Vector3"/> of the form
    /// { <paramref name="input"/>.X, <paramref name="input"/>.Y, <paramref name="z"/>, <paramref name="w"/> }</returns>
    public static Vector4 ToVec4(this Vector2 input, float z = 0f, float w = 0f)
    {
        return new Vector4(input, z, w);
    }

    /// <summary>
    /// Returns the component at the given index. 0 is X and 1 is Y.
    /// </summary>
    public static float GetComponent(this Vector2 input, int index)
    {
        return index switch
        {
            0 => input.X,
            1 => input.Y,
            SwizzleExtensions.SwizzleZeroIndex => 0,
            SwizzleExtensions.SwizzleOneIndex => 1,
            _ => throw new IndexOutOfRangeException("Index must be 0 or 1"),
        };
    }

    /// <summary>
    /// Returns a new vector with the component at the given index set to the given value. 0 is X and 1 is Y.
    /// </summary>
    public static Vector2 SetComponent(this Vector2 input, int index, float value)
    {
        return index switch
        {
            0 => input with { X = value },
            1 => input with { Y = value },
            _ => throw new IndexOutOfRangeException("Index must be 0 or 1"),
        };
    }
    
    /// <summary>
    /// Sets the given vector's component at the given index to the given value. Mutates original vector. 0 is X, 1 is Y.
    /// </summary>
    public static void SetComponentMutate(this ref Vector2 input, int index, float value)
    {
        switch (index)
        {
            case 0:
                input.X = value;
                break;
            case 1:
                input.Y = value;
                break;
            default:
                throw new IndexOutOfRangeException("Index must be 0 or 1.");
        }
    }

    /// <summary>
    /// Returns the slope of this vector: Y/X
    /// </summary>
    public static float GetSlope(this Vector2 input)
    {
        return input.Y / input.X;
    }

    /// <summary>
    /// Returns which quadrant this vector lies in. Top-right quadrant is 1, and they ascend counter-clockwise through 4.
    ///   Vectors on the X or Y axis return 0.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Quadrant GetQuadrant(this Vector2 input)
    {
        return input switch
        {
            { X: > 0, Y: > 0 } => Quadrant.One,
            { X: < 0, Y: > 0 } => Quadrant.Two,
            { X: < 0, Y: < 0 } => Quadrant.Three,
            { X: > 0, Y: < 0 } => Quadrant.Four,
            { X: > 0, Y:   0 } => Quadrant.Right,
            { X:   0, Y: > 0 } => Quadrant.Up,
            { X: < 0, Y:   0 } => Quadrant.Left,
            { X:   0, Y: < 0 } => Quadrant.Down,
            { X:   0, Y:   0 } => Quadrant.Zero,
            { X: float.NaN } or { Y: float.NaN } => throw new ArgumentException("Vector components must not be NaN"),
        };
    }
    
    /// <summary>
    /// Returns the vector representation of the point in the given list
    ///   which is closest to this vector representation of a point.
    /// </summary>
    public static Vector2 GetClosestPoint(this Vector2 input, Vector2[] otherPoints)
    {
        float lowestDist = float.MaxValue;
        Vector2 closestPoint = input;
        for (int i = 0; i < otherPoints.Length; i++)
        {
            var thisDist = Vector2.DistanceSquared(input, otherPoints[i]);
            if (thisDist <= lowestDist)
            {
                closestPoint = otherPoints[i];
                lowestDist = thisDist;
            }
        }
        return closestPoint;
    }

    /// <summary>
    /// Returns the angle of this vector where 0 is the X unit vector. Ranges from 0 to 2*pi.
    ///   Returns <see cref="float.NaN"/> if this vector is (0, 0) or if either component is not a number.
    /// </summary>
    public static float GetCcwAngle(this Vector2 input)
    {
        return input.GetQuadrant() switch
        {
            Quadrant.One or Quadrant.Two or Quadrant.Up or Quadrant.Left => MathF.Acos(Vector2.Dot(Vector2.UnitX, input) / input.Length()),
            Quadrant.Three or Quadrant.Four or Quadrant.Down or Quadrant.Right => MathConstants.FullTurn - MathF.Acos(Vector2.Dot(Vector2.UnitX, input) / input.Length()),
            Quadrant.Zero => float.NaN,
            _ => throw new Exception(),
        };
    }

    /// <summary>
    /// Returns the angle of this vector where 0 is the X unit vector. Ranges from -pi to pi
    /// </summary>
    public static float GetClosestAngle(this Vector2 input)
    {
        return input.GetQuadrant() switch
        {
            Quadrant.One or Quadrant.Two or Quadrant.Up or Quadrant.Left => MathF.Acos(Vector2.Dot(Vector2.UnitX, input) / input.Length()),
            Quadrant.Three or Quadrant.Four or Quadrant.Down or Quadrant.Right => -MathF.Acos(Vector2.Dot(Vector2.UnitX, input) / input.Length()),
            Quadrant.Zero => float.NaN,
            _ => throw new Exception(),
        };
    }

    /// <summary>
    /// Increases the magnitude of a vector by the specified amount without changing its direction and returns the result as a new vector.
    ///   Does not mutate <paramref name="input"/>.
    /// </summary>
    public static Vector2 Extend(this Vector2 input, float distance)
    {
        var length = input.Length();
        return input.Scale(1 + distance / length, 1 + distance / length);
    }

    /// <summary>
    /// Returns the projection of this vector onto a target vector.
    ///   For a visualization of projection, see <a href="https://www.geogebra.org/m/XShfg9r8">here</a>.
    /// <br/>Value: proj_<paramref name="target"/> <paramref name="input"/>
    /// </summary>
    public static Vector2 ProjectOnto(this Vector2 input, Vector2 target)
    {
        var dot = Vector2.Dot(input, target);
        var result = dot / target.LengthSquared() * target;
        return result;
    }

    /// <summary>
    /// Performs a linear interpolation between <paramref name="input.lerpFrom"/> and <paramref name="input.lerpTo"/>.
    ///   Wrapper for <see cref="Vector2.Lerp(Vector2, Vector2, float)"/>
    /// </summary>
    public static Vector2 Lerp(this (Vector2 lerpFrom, Vector2 lerpTo) input, float t)
    {
        return Vector2.Lerp(input.lerpFrom, input.lerpTo, t);
    }

    /// <summary>
    /// Performs linear interpolation from the first vector to the second vector on its components individually,
    ///   using <paramref name="tValues"/>.X to lerp the X components and <paramref name="tValues"/>.Y to lerp the Y components.
    /// </summary>
    public static Vector2 PiecewiseLerp(this (Vector2 lerpFrom, Vector2 lerpTo) input, Vector2 tValues)
    {
        var xLerp = MathHelper.Lerp(input.lerpFrom.X, input.lerpTo.X, tValues.X);
        var yLerp = MathHelper.Lerp(input.lerpFrom.Y, input.lerpTo.Y, tValues.Y);
        return new Vector2(xLerp, yLerp);
    }

    #region Random

    /// <summary>
    /// Generates two random numbers greater than or equal to 0.0 and less than 1.0, then returns a copy of this vector whose
    ///   components have each been multiplied by one of those numbers.
    /// <br/>You may optionally provide an existing <see cref="Random"/> instance.
    ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided.
    /// <br/><br/>Common usage: <c>Vector2.One.Randomize()</c>
    /// </summary>
    public static Vector2 Randomize(this Vector2 input, bool canInvert = false, Random? random = null)
    {
        random ??= Random.Shared;
        if (canInvert)
        {
            return new Vector2(input.X * random.NextSingle() * random.NextSign(), input.Y * random.NextSingle() * random.NextSign());
        }
        return new Vector2(input.X * random.NextSingle(), input.Y * random.NextSingle());
    }

    /// <summary>
    /// Returns a new vector with random values between the two input vectors.
    ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    /// Common usage: <c>(Vector2.Zero, Vector2.One).Randomize()</c> <br/>
    /// </summary>
    public static Vector2 RandomizeBetween(this (Vector2 minValues, Vector2 maxValues) input, Random? random = null)
    {
        random ??= Random.Shared;
        return input.PiecewiseLerp(Vector2.One.Randomize(random: random));
    }

    /// <summary>
    /// Returns a new vector with the same magnitude, but a random angle. By default, the new angle could be any direction.
    ///   Providing a tolerance means that the new angle will be within that much in either direction from the current angle.
    ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    /// </summary>
    public static Vector2 RandomizeAngle(this Vector2 input, float tolerance = MathConstants.HalfTurn, Random? random = null)
    {
        random ??= Random.Shared;
        return input.RotatedBy(MathHelper.Lerp(-tolerance, tolerance, random.NextSingle()));
    }

    /// <summary>
    /// Returns a new vector with the same magnitude, but a random angle greater than or equal to
    ///   <paramref name="min"/> and less than <paramref name="max"/>.
    ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    /// </summary>
    public static Vector2 RandomizeAngleBetween(this Vector2 input, float min, float max, Random? random = null)
    {
        random ??= Random.Shared;
        return input.AtLength(MathHelper.Lerp(min, max, random.NextSingle()));
    }

    /// <summary>
    /// Returns a new vector with the same angle, but a random and lesser magnitude. If <paramref name="canInvert"/> is true,
    ///   the new vector also has a 50% chance to be pointing backward.
    ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    /// </summary>
    public static Vector2 RandomizeMagnitude(this Vector2 input, bool canInvert = false, Random? random = null)
    {
        random ??= Random.Shared;
        if (canInvert)
        {
            return random.NextSingle() * random.NextSign() * input;
        }
        return random.NextSingle() * input;
    }

    /// <summary>
    /// Returns a new vector with the same angle, but a random magnitude greater than or equal to <paramref name="min"/>
    ///   and less than <paramref name="max"/>.
    ///   If <paramref name="canInvert"/> is true, the new vector also has a 50% chance to be pointing backward.
    ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    /// </summary>
    public static Vector2 RandomizeMagnitudeBetween(this Vector2 input, float min, float max, bool canInvert = false, Random? random = null)
    {
        random ??= Random.Shared;
        if (canInvert)
        {
            return MathHelper.Lerp(min, max, random.NextSingle()) * random.NextSign() * input;
        }
        return MathHelper.Lerp(min, max, random.NextSingle()) * input;
    }

    #endregion

    #region Transforms

    /// <summary>
    /// Applies a transformation matrix to a vector. Wrapper for <see cref="Vector2.Transform(Vector2, MgMatrix)"/>.
    /// </summary>
    public static Vector2 Transform(this Vector2 input1, MgMatrix input2)
    {
        return Vector2.Transform(input1, input2);
    }

    /// <summary>
    /// Translates a Vector2 by the specified amounts on the 2 axes.
    /// </summary>
    /// <returns>A new Vector2 of the form { <paramref name="input1"/>.X + <paramref name="x"/>,
    ///   <paramref name="input1"/>.Y + <paramref name="y"/> }</returns>
    public static Vector2 Translate(this Vector2 input1, float x = 0f, float y = 0f)
    {
        return new Vector2(input1.X + x, input1.Y + y);
    }

    /// <summary>
    /// Scales a Vector2 by the specified amounts on the 2 axes.
    /// </summary>
    /// <returns>A new Vector2 of the form { <paramref name="input1"/>.X * <paramref name="x"/>,
    ///   <paramref name="input1"/>.Y * <paramref name="y"/> }</returns>
    public static Vector2 Scale(this Vector2 input1, float x = 1f, float y = 1f)
    {
        return new Vector2(input1.X * x, input1.Y * y);
    }

    // /// <summary>
    // /// Rotates a vector by <paramref name="radians"/>.
    // /// </summary>
    // /// <returns>A new Vector2 that has been rotated.</returns>
    // public static Vector2 Rotate(this Vector2 input1, float radians)
    // {
    //     return input1.Transform(MgMatrix.CreateRotationZ(radians));
    // }

    // /// <summary>
    // /// Normalizes a vector, i.e. sets this vector's magnitude to 0 while preserving its direction.
    // /// </summary>
    // /// <param name="input1"></param>
    // /// <returns>A Vector2 with magnitude 1 if the input vector's magnitude is nonzero. Returns the zero vector otherwise.</returns>
    // public static Vector2 NormalizeExtension(this Vector2 input1)
    // {
    //     var magnitude = input1.Length();
    //     if (magnitude == 0)
    //     {
    //         return Vector2.Zero;
    //     }
    //     return input1 / magnitude;
    // }

    /// <summary>
    /// Returns a new vector with the same direction but with the magnitude provided.
    /// </summary>
    public static Vector2 WithMagnitude(this Vector2 input, float magnitude)
    {
        var angle = input.GetCcwAngle();
        return magnitude * Vector2.UnitX.RotatedBy(angle);
    }

    /// <summary>
    /// Returns a new vector with the same magnitude but at the angle provided.
    /// </summary>
    public static Vector2 WithAngle(this Vector2 input, float angle)
    {
        var length = input.Length();
        return length * Vector2.UnitX.RotatedBy(angle);
    }

    /// <summary>
    /// Applies a given func to each component of this vector and returns the result.
    /// </summary>
    public static Vector2 MutatePiecewise(this Vector2 inputVector, Func<float, float> mutator)
    {
        return new Vector2(mutator(inputVector.X), mutator(inputVector.Y));
    }

    /// <summary>
    /// Moves this vector right the specified amount and returns the result.
    /// </summary>
    public static Vector2 Right(this Vector2 inputVector, float amount)
    {
        return inputVector with { X = inputVector.X + amount};
    }

    /// <summary>
    /// Moves this vector left the specified amount and returns the result.
    /// </summary>
    public static Vector2 Left(this Vector2 inputVector, float amount)
    {
        return inputVector with { X = inputVector.X - amount };
    }

    /// <summary>
    /// Moves this vector up the specified amount and returns the result.
    /// </summary>
    public static Vector2 Up(this Vector2 inputVector, float amount)
    {
        return inputVector with { Y = inputVector.Y + amount };
    }

    /// <summary>
    /// Moves this vector down the specified amount and returns the result.
    /// </summary>
    /// <param name="inputVector"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static Vector2 Down(this Vector2 inputVector, float amount)
    {
        return inputVector with { Y = inputVector.Y - amount };
    }

    #endregion

    #region Arithmetic: Vector3

    /// <summary>
    /// Adds a Vector3 to this Vector2. The Z component of the Vector3 is ignored.
    /// </summary>
    /// <returns>A new Vector3 of the form { <paramref name="input1"/>.X + <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y + <paramref name="input2"/>.Y, <paramref name="input2"/>.Z }</returns>
    public static Vector2 Add(this Vector2 input1, Vector3 input2)
    {
        return new Vector2(input1.X + input2.X, input1.Y + input2.Y);
    }

    /// <summary>
    /// Multiplies a Vector3 by this Vector2. The Z component of the Vector3 is ignored.
    /// </summary>
    /// <returns>A new Vector3 of the form { <paramref name="input1"/>.X * <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y * <paramref name="input2"/>.Y, <paramref name="input2"/>.Z }</returns>
    public static Vector2 Multiply(this Vector2 input1, Vector3 input2)
    {
        return new Vector2(input1.X * input2.X, input1.Y * input2.Y);
    }

    /// <summary>
    /// Subtracts a Vector3 from this Vector2.  The Z component of the Vector3 is ignored.
    /// </summary>
    /// <returns>A new Vector3 of the form { <paramref name="input1"/>.X - <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y - <paramref name="input2"/>.Y, 0f - <paramref name="input2"/>.Z }</returns>
    public static Vector2 Subtract(this Vector2 input1, Vector3 input2)
    {
        return new Vector2(input1.X - input2.X, input1.Y - input2.Y);
    }

    /// <summary>
    /// Divides this Vector2 by a Vector3. The Z component of the Vector3 is ignored.
    /// </summary>
    /// <returns>A new Vector3 of the form { <paramref name="input1"/>.X / <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y / <paramref name="input2"/>.Y, 1f / <paramref name="input2"/>.Z }</returns>
    public static Vector2 Divide(this Vector2 input1, Vector3 input2)
    {
        return new Vector2(input1.X / input2.X, input1.Y / input2.Y);
    }

    #endregion

    #region Arithmetic: Vector2

    /// <summary>
    /// Adds another vector to this one per-component. Equivalent to vector + vector
    /// </summary>
    /// <returns>A new Vector2 of the form { <paramref name="input1"/>.X + <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X + <paramref name="input2"/>.Y }</returns>
    public static Vector2 Add(this Vector2 input1, Vector2 input2)
    {
        return new Vector2(input1.X + input2.X, input1.Y + input2.Y);
    }

    /// <summary>
    /// Multiplies (does not perform dot product or cross product) this vector by another one per-component.
    /// </summary>
    /// <returns>A new Vector2 of the form { <paramref name="input1"/>.X * <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X * <paramref name="input2"/>.Y }</returns>
    public static Vector2 Multiply(this Vector2 input1, Vector2 input2)
    {
        return new Vector2(input1.X * input2.X, input1.Y * input2.Y);
    }

    /// <summary>
    /// Subtracts another vector from this vector per-component. Equivalent to vector - vector
    /// </summary>
    /// <returns>A new Vector2 of the form { <paramref name="input1"/>.X - <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X - <paramref name="input2"/>.Y }</returns>
    public static Vector2 Subtract(this Vector2 input1, Vector2 input2)
    {
        return new Vector2(input1.X - input2.X, input1.Y - input2.Y);
    }

    /// <summary>
    /// Divides this vector by another one per-component.
    /// </summary>
    /// <returns>A new Vector2 of the form { <paramref name="input1"/>.X / <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X / <paramref name="input2"/>.Y }</returns>
    public static Vector2 Divide(this Vector2 input1, Vector2 input2)
    {
        return new Vector2(input1.X / input2.X, input1.Y / input2.Y);
    }

    #endregion

    #region Arithmetic: Point

    /// <summary>
    /// Adds a point to this vector per-component.
    /// </summary>
    /// <returns>A new Vector2 of the form { <paramref name="input1"/>.X + <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X + <paramref name="input2"/>.Y }</returns>
    public static Vector2 Add(this Vector2 input1, FrbPoint input2)
    {
        return new Vector2(input1.X + (float)input2.X, input1.Y + (float)input2.Y);
    }

    /// <summary>
    /// Multiplies this vector by a point per-component.
    /// </summary>
    /// <returns>A new Vector2 of the form { <paramref name="input1"/>.X * <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X * <paramref name="input2"/>.Y }</returns>
    public static Vector2 Multiply(this Vector2 input1, FrbPoint input2)
    {
        return new Vector2(input1.X * (float)input2.X, input1.Y * (float)input2.Y);
    }

    /// <summary>
    /// Subtracts a point from this vector per-component.
    /// </summary>
    /// <returns>A new Vector2 of the form { <paramref name="input1"/>.X - <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X - <paramref name="input2"/>.Y }</returns>
    public static Vector2 Subtract(this Vector2 input1, FrbPoint input2)
    {
        return new Vector2(input1.X - (float)input2.X, input1.Y - (float)input2.Y);
    }

    /// <summary>
    /// Divides this vector by another one per-component.
    /// </summary>
    /// <returns>A new Vector2 of the form { <paramref name="input1"/>.X / <paramref name="input2"/>.Y,
    ///   <paramref name="input1"/>.X / <paramref name="input2"/>.Y }</returns>
    public static Vector2 Divide(this Vector2 input1, FrbPoint input2)
    {
        return new Vector2(input1.X / (float)input2.X, input1.Y / (float)input2.Y);
    }

    #endregion
}

public enum Quadrant
{
    Zero,
    One,
    Two,
    Three,
    Four,
    Right,
    Up,
    Left,
    Down,
}
