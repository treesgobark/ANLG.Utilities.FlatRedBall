// ReSharper disable InconsistentNaming

using ANLG.Utilities.FlatRedBall.Constants;
using MathHelper = Microsoft.Xna.Framework.MathHelper;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Vector4 = Microsoft.Xna.Framework.Vector4;
using MgMatrix = Microsoft.Xna.Framework.Matrix;
using static Microsoft.Xna.Framework.Vector3ExtensionMethods;

namespace ANLG.Utilities.FlatRedBall.Extensions;

///
public static class Vector3Extensions
{
    // /// <summary>
    // /// Allows deconstruction into (float x, float y, float z)
    // /// </summary>
    // public static void Deconstruct(this Vector3 input, out float x, out float y, out float z)
    // {
    //     x = input.X;
    //     y = input.Y;
    //     z = input.Z;
    // }
    
    /// <summary>
    /// Allows deconstruction into (float x, float y, float z)
    /// </summary>
    public static void Deconstruct(this Vector3? input, out float x, out float y, out float z)
    {
        if (input is null)
        {
            x = 0f;
            y = 0f;
            z = 0f;
            return;
        }

        x = input.Value.X;
        y = input.Value.Y;
        z = input.Value.Z;
    }
    
    /// <summary>
    /// Adds a dimension to a <see cref="Vector3"/>.
    /// </summary>
    /// <returns>A new <see cref="Vector3"/> of the form
    /// { <paramref name="input"/>.X, <paramref name="input"/>.Y, <paramref name="input"/>.Z, <paramref name="w"/> }</returns>
    public static Vector4 ToVec4(this Vector3 input, float w = 0f)
    {
        return new Vector4(input, w);
    }

    /// <summary>
    /// Returns the component at the given index. 0 is X, 1 is Y, and 2 is Z.
    /// </summary>
    public static float GetComponent(this Vector3 input, int index)
    {
        return index switch
        {
            0 => input.X,
            1 => input.Y,
            2 => input.Z,
            SwizzleExtensions.SwizzleZeroIndex => 0,
            SwizzleExtensions.SwizzleOneIndex => 1,
            _ => throw new IndexOutOfRangeException("Index must be 0, 1, or 2."),
        };
    }

    /// <summary>
    /// Returns a new vector with the component at the given index set to the given value. 0 is X, 1 is Y, and 2 is Z.
    /// </summary>
    public static Vector3 SetComponent(this Vector3 input, int index, float value)
    {
        return index switch
        {
            0 => input with { X = value },
            1 => input with { Y = value },
            2 => input with { Z = value },
            _ => throw new IndexOutOfRangeException("Index must be 0, 1, or 2."),
        };
    }
    
    /// <summary>
    /// Sets the given vector's component at the given index to the given value. Mutates original vector. 0 is X, 1 is Y, 2 is Z.
    /// </summary>
    public static void SetComponentMutate(this ref Vector3 input, int index, float value)
    {
        switch (index)
        {
            case 0:
                input.X = value;
                break;
            case 1:
                input.Y = value;
                break;
            case 2:
                input.Z = value;
                break;
            default:
                throw new IndexOutOfRangeException("Index must be 0, 1, or 2.");
        }
    }
    
    /// <summary>
    /// Stuffs the X and Y components of this vector into the X and Y components of a vector, respectively.
    /// </summary>
    public static Vector2 XY(this Vector3 input)
    {
        return new Vector2(input.X, input.Y);
    }

    /// <summary>
    /// Stuffs the X and Z components of this vector into the X and Y components of a vector, respectively.
    /// </summary>
    public static Vector2 XZ(this Vector3 input)
    {
        return new Vector2(input.X, input.Z);
    }

    /// <summary>
    /// Stuffs the Y and Z components of this vector into the X and Y components of a vector, respectively.
    /// </summary>
    public static Vector2 YZ(this Vector3 input)
    {
        return new Vector2(input.Y, input.Z);
    }

    /// <summary>
    /// Returns the projection of this vector onto a target vector.
    ///   For a visualization of projection, see here: https://www.geogebra.org/m/XShfg9r8
    /// <remarks>proj_<paramref name="target"/> <paramref name="input"/></remarks>
    /// </summary>
    public static Vector3 ProjectOnto(this Vector3 input, Vector3 target)
    {
        var dot = Vector3.Dot(input, target);
        var result = dot / target.LengthSquared() * target;
        return result;
    }

    /// <summary>
    /// Performs a linear interpolation between <paramref name="input.lerpFrom"/> and <paramref name="input.lerpTo"/>.
    ///   Wrapper for <see cref="Vector3.Lerp(Vector3, Vector3, float)"/>
    /// </summary>
    public static Vector3 Lerp(this (Vector3 lerpFrom, Vector3 lerpTo) input, float t)
    {
        return Vector3.Lerp(input.lerpFrom, input.lerpTo, t);
    }

    /// <summary>
    /// Performs linear interpolation from the first vector to the second vector on its components individually,
    ///   using <paramref name="tValues"/>.X to lerp the X components, <paramref name="tValues"/>.Y to lerp the Y components,
    ///   and <paramref name="tValues"/>.Z to lerp the Z components.
    /// </summary>
    public static Vector3 PiecewiseLerp(this (Vector3 lerpFrom, Vector3 lerpTo) input, Vector3 tValues)
    {
        var xLerp = MathHelper.Lerp(input.lerpFrom.X, input.lerpTo.X, tValues.X);
        var yLerp = MathHelper.Lerp(input.lerpFrom.Y, input.lerpTo.Y, tValues.Y);
        var zLerp = MathHelper.Lerp(input.lerpFrom.Z, input.lerpTo.Z, tValues.Z);
        return new Vector3(xLerp, yLerp, zLerp);
    }

    #region Random

    /// <summary>
    /// Generates two random numbers greater than or equal to 0.0 and less than 1.0, then returns a copy of this vector whose
    ///   components have each been multiplied by one of those numbers.
    /// <br/>You may optionally provide an existing <see cref="Random"/> instance.
    ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided.
    /// <br/><br/>Common usage: <c>Vector2.One.Randomize()</c>
    /// </summary>
    public static Vector3 Randomize(this Vector3 input, bool canInvert = false, Random? random = null)
    {
        random ??= Random.Shared;
        if (canInvert)
        {
            return new Vector3(input.X * random.NextSingle() * random.NextSign(), 
                input.Y * random.NextSingle() * random.NextSign(),
                input.Z * random.NextSingle() * random.NextSign());
        }
        return new Vector3(input.X * random.NextSingle(), input.Y * random.NextSingle(), input.Z * random.NextSingle());
    }

    /// <summary>
    /// Returns a new vector with random values between the two input vectors.
    ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    /// Common usage: <c>(Vector2.Zero, Vector2.One).Randomize()</c> <br/>
    /// </summary>
    public static Vector3 RandomizeBetween(this (Vector3 minValues, Vector3 maxValues) input, Random? random = null)
    {
        random ??= Random.Shared;
        return input.PiecewiseLerp(Vector3.One.Randomize(random: random));
    }

    /// <summary>
    /// Returns a new vector with the same magnitude, but a random angle. By default, the new angle could be any direction.
    ///   Providing a tolerance means that the new angle will be within that much in either direction from the current angle.
    ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    /// </summary>
    public static Vector3 RandomizeAngle(this Vector3 input, float tolerance = MathConstants.HalfTurn, Random? random = null)
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
    public static Vector3 RandomizeAngleBetween(this Vector3 input, float min, float max, Random? random = null)
    {
        random ??= Random.Shared;
        return input.AtAngle(MathHelper.Lerp(min, max, random.NextSingle()));
    }

    /// <summary>
    /// Returns a new vector with the same angle, but a random and lesser magnitude. If <paramref name="canInvert"/> is true,
    ///   the new vector also has a 50% chance to be pointing backward.
    ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    /// </summary>
    public static Vector3 RandomizeMagnitude(this Vector3 input, bool canInvert = false, Random? random = null)
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
    public static Vector3 RandomizeMagnitudeBetween(this Vector3 input, float min, float max, bool canInvert = false, Random? random = null)
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
    /// Applies a transformation matrix to a vector. Wrapper for <see cref="Vector3.Transform(Vector3, MgMatrix)"/>.
    /// </summary>
    public static Vector3 Transform(this Vector3 input1, MgMatrix input2)
    {
        return Vector3.Transform(input1, input2);
    }

    /// <summary>
    /// Translates a vector by the specified amounts on the 3 axes.
    /// </summary>
    /// <returns>A new Vector3 of the form { <paramref name="input1"/>.X + <paramref name="x"/>,
    ///   <paramref name="input1"/>.Y + <paramref name="y"/>, <paramref name="input1"/>.Z + <paramref name="z"/> }</returns>
    public static Vector3 Translate(this Vector3 input1, float x = 0f, float y = 0f, float z = 0f)
    {
        return new Vector3(input1.X + x, input1.Y + y, input1.Z + z);
    }

    /// <summary>
    /// Scales this vector by the specified amounts on the 3 axes.
    /// </summary>
    /// <returns>A new Vector3 of the form { <paramref name="input1"/>.X * <paramref name="x"/>,
    ///   <paramref name="input1"/>.Y * <paramref name="y"/>, <paramref name="input1"/>.Z * <paramref name="z"/> }</returns>
    public static Vector3 Scale(this Vector3 input1, float x = 1f, float y = 1f, float z = 1f)
    {
        return new Vector3(input1.X * x, input1.Y * y, input1.Z * z);
    }

    /// <summary>
    /// Rotates this vector by <paramref name="radians"/> about the X axis.
    /// </summary>
    /// <returns>A new Vector3 that has been rotated.</returns>
    public static Vector3 RotateX(this Vector3 input1, float radians)
    {
        return input1.Transform(MgMatrix.CreateRotationX(radians));
    }

    /// <summary>
    /// Rotates this vector by <paramref name="radians"/> about the Y axis.
    /// </summary>
    /// <returns>A new Vector3 that has been rotated.</returns>
    public static Vector3 RotateY(this Vector3 input1, float radians)
    {
        return input1.Transform(MgMatrix.CreateRotationY(radians));
    }

    /// <summary>
    /// Rotates this vector by <paramref name="radians"/> about the Z axis.
    /// This is essentially the same as rotating this vector in 2D space, Z will be unaffected.
    /// </summary>
    /// <returns>A new Vector3 that has been rotated.</returns>
    public static Vector3 RotateZ(this Vector3 input1, float radians)
    {
        return input1.Transform(MgMatrix.CreateRotationZ(radians));
    }

    /// <summary>
    /// Normalizes a vector, i.e. sets this vector's magnitude to 0 while preserving its direction.
    /// </summary>
    /// <param name="input"></param>
    /// <returns>A Vector3 with magnitude 1 if the input vector's magnitude is nonzero. Returns the zero vector otherwise.</returns>
    public static Vector3 GetNormalized(this Vector3 input)
    {
        var magnitude = input.Length();
        if (magnitude == 0)
        {
            return Vector3.Zero;
        }
        return input / magnitude;
    }

    /// <summary>
    /// Applies a given func to each component of this vector and returns the result.
    /// </summary>
    /// <param name="inputVector"></param>
    /// <param name="mutator"></param>
    /// <returns></returns>
    public static Vector3 MutatePiecewise(this Vector3 inputVector, Func<float, float> mutator)
    {
        return new Vector3(mutator(inputVector.X), mutator(inputVector.Y), mutator(inputVector.Z));
    }

    /// <summary>
    /// Moves this vector right the specified amount and returns the result.
    /// </summary>
    /// <param name="inputVector"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static Vector3 Right(this Vector3 inputVector, float amount)
    {
        return inputVector with { X = inputVector.X + amount };
    }

    /// <summary>
    /// Moves this vector left the specified amount and returns the result.
    /// </summary>
    /// <param name="inputVector"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static Vector3 Left(this Vector3 inputVector, float amount)
    {
        return inputVector with { X = inputVector.X - amount };
    }

    /// <summary>
    /// Moves this vector up the specified amount and returns the result.
    /// </summary>
    /// <param name="inputVector"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static Vector3 Up(this Vector3 inputVector, float amount)
    {
        return inputVector with { Y = inputVector.Y + amount };
    }

    /// <summary>
    /// Moves this vector down the specified amount and returns the result.
    /// </summary>
    /// <param name="inputVector"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static Vector3 Down(this Vector3 inputVector, float amount)
    {
        return inputVector with { Y = inputVector.Y - amount };
    }

    /// <summary>
    /// Moves this vector backward the specified amount and returns the result.
    /// </summary>
    /// <param name="inputVector"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static Vector3 Backward(this Vector3 inputVector, float amount)
    {
        return inputVector with { Z = inputVector.Z + amount };
    }

    /// <summary>
    /// Moves this vector forward the specified amount and returns the result.
    /// </summary>
    /// <param name="inputVector"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static Vector3 Forward(this Vector3 inputVector, float amount)
    {
        return inputVector with { Z = inputVector.Z - amount };
    }

    #endregion

    #region Arithmetic: Vector3

    /// <summary>
    /// Adds another vector to this one per-component. Equivalent to vector + vector
    /// </summary>
    /// <returns>A new vector of the form { <paramref name="input1"/>.X + <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y + <paramref name="input2"/>.Y, <paramref name="input1"/>.Z + <paramref name="input2"/> }</returns>
    public static Vector3 Add(this Vector3 input1, Vector3 input2)
    {
        return new Vector3(input1.X + input2.X, input1.Y + input2.Y, input1.Z + input2.Z);
    }

    /// <summary>
    /// Multiplies (does not perform dot product or cross product) this vector by another one per-component.
    /// </summary>
    /// <returns>A new Vector3 of the form { <paramref name="input1"/>.X * <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y * <paramref name="input2"/>.Y, <paramref name="input1"/>.Z * <paramref name="input2"/> }</returns>
    public static Vector3 Multiply(this Vector3 input1, Vector3 input2)
    {
        return new Vector3(input1.X * input2.X, input1.Y * input2.Y, input1.Z * input2.Z);
    }


    /// <summary>
    /// Subtracts another vector from this vector per-component. Equivalent to vector - vector
    /// </summary>
    /// <returns>A new Vector3 of the form { <paramref name="input1"/>.X - <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y - <paramref name="input2"/>.Y, <paramref name="input1"/>.Z - <paramref name="input2"/> }</returns>
    public static Vector3 Subtract(this Vector3 input1, Vector3 input2)
    {
        return new Vector3(input1.X - input2.X, input1.Y - input2.Y, input1.Z - input2.Z);
    }

    /// <summary>
    /// Divides this vector by another one per-component.
    /// </summary>
    /// <returns>A new Vector3 of the form { <paramref name="input1"/>.X / <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y / <paramref name="input2"/>.Y, <paramref name="input1"/>.Z / <paramref name="input2"/> }</returns>
    public static Vector3 Divide(this Vector3 input1, Vector3 input2)
    {
        return new Vector3(input1.X / input2.X, input1.Y / input2.Y, input1.Z) / input2.Z;
    }

    /// <summary>
    /// Returns the distance between two points represented as vectors. Distance is never negative.
    /// </summary>
    public static float Distance(this Vector3 input1, Vector3 input2)
    {
        return Vector3.Distance(input1, input2);
    }

    /// <summary>
    /// Returns the squared distance between two points represented as vectors. Distance is never negative.
    /// </summary>
    public static float DistanceSquared(this Vector3 input1, Vector3 input2)
    {
        return Vector3.DistanceSquared(input1, input2);
    }

    #endregion

    #region Arithmetic: Vector2

    /// <summary>
    /// Adds a 2-dimensional vector to this 3-dimensional vector.
    ///   The Z component of the 2D vector is assumed to be the additive identity (0f).
    /// </summary>
    /// <returns>A new vector of the form: <br/> { <paramref name="input1"/>.X + <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y + <paramref name="input2"/>.Y, <paramref name="input1"/>.Z }</returns>
    public static Vector3 Add(this Vector3 input1, Vector2 input2)
    {
        return new Vector3(input1.X + input2.X, input1.Y + input2.Y, input1.Z);
    }

    /// <summary>
    /// Multiples this 3-dimensional vector by a 2-dimensional vector.
    ///   The Z component of the 2D vector is assumed to be the multiplicative identity (1f).
    /// </summary>
    /// <returns>A new Vector3 of the form: <br/> { <paramref name="input1"/>.X * <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y * <paramref name="input2"/>.Y, <paramref name="input1"/>.Z }</returns>
    public static Vector3 Multiply(this Vector3 input1, Vector2 input2)
    {
        return new Vector3(input1.X * input2.X, input1.Y * input2.Y, input1.Z);
    }

    /// <summary>
    /// Subtracts a 2-dimensional vector from this 3-dimensional vector.
    ///   The Z component of the 2D vector is assumed to be the additive identity (0f).
    /// </summary>
    /// <returns>A new Vector3 of the form: <br/> { <paramref name="input1"/>.X - <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y - <paramref name="input2"/>.Y, <paramref name="input1"/>.Z }</returns>
    public static Vector3 Subtract(this Vector3 input1, Vector2 input2)
    {
        return new Vector3(input1.X - input2.X, input1.Y - input2.Y, input1.Z);
    }

    /// <summary>
    /// Divides this 3-dimensional vector by a 2-dimensional vector.
    ///   The Z component of the 2D vector is assumed to be the multiplicative identity (1f).
    /// </summary>
    /// <returns>A new Vector3 of the form: <br/> { <paramref name="input1"/>.X / <paramref name="input2"/>.X,
    ///   <paramref name="input1"/>.Y / <paramref name="input2"/>.Y, 1f / <paramref name="input1"/>.Z }</returns>
    public static Vector3 Divide(this Vector3 input1, Vector2 input2)
    {
        return new Vector3(input1.X / input2.X, input1.Y / input2.Y, input1.Z);
    }

    #endregion
}
