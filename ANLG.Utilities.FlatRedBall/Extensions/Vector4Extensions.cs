using MgVector2 = Microsoft.Xna.Framework.Vector2;
using MgVector3 = Microsoft.Xna.Framework.Vector3;
using MgVector4 = Microsoft.Xna.Framework.Vector4;

namespace ANLG.Utilities.FlatRedBall.Extensions;

public static class Vector4Extensions
{
    /// <summary>
    /// Returns the component at the given index. 0 is X, 1 is Y, 2 is Z, and 3 is W.
    /// </summary>
    public static float GetComponent(this MgVector4 input, int index)
    {
        return index switch
        {
            0 => input.X,
            1 => input.Y,
            2 => input.Z,
            3 => input.W,
            SwizzleExtensions.SwizzleZeroIndex => 0,
            SwizzleExtensions.SwizzleOneIndex => 1,
            _ => throw new IndexOutOfRangeException("Index must be 0, 1, 2, or 3."),
        };
    }
    
    /// <summary>
    /// Returns a new vector with the component at the given index set to the given value. 0 is X, 1 is Y, 2 is Z, and 3 is W.
    /// </summary>
    public static MgVector4 SetComponent(this MgVector4 input, int index, float value)
    {
        return index switch
        {
            0 => input with { X = value },
            1 => input with { Y = value },
            2 => input with { Z = value },
            3 => input with { W = value },
            _ => throw new IndexOutOfRangeException("Index must be 0, 1, 2, or 3."),
        };
    }
    
    /// <summary>
    /// Sets the given vector's component at the given index to the given value. Mutates original vector. 0 is X, 1 is Y, 2 is Z, and 3 is W.
    /// </summary>
    public static void SetComponentMutate(this ref MgVector4 input, int index, float value)
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
            case 3:
                input.W = value;
                break;
            default:
                throw new IndexOutOfRangeException("Index must be 0, 1, 2, or 3.");
        }
    }

    #region Random

    /// <summary>
    /// Generates two random numbers greater than or equal to 0.0 and less than 1.0, then returns a copy of this vector whose
    ///   components have each been multiplied by one of those numbers.
    /// <br/>You may optionally provide an existing <see cref="Random"/> instance.
    ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided.
    /// <br/><br/>Common usage: <c>Vector2.One.Randomize()</c>
    /// </summary>
    public static MgVector4 Randomize(this MgVector4 input, bool canInvert = false, Random? random = null)
    {
        random ??= Random.Shared;
        if (canInvert)
        {
            return new MgVector4(input.X * random.NextSingle() * random.NextSign(), 
                input.Y * random.NextSingle() * random.NextSign(),
                input.Z * random.NextSingle() * random.NextSign(),
                input.W * random.NextSingle() * random.NextSign());
        }
        return new MgVector4(input.X * random.NextSingle(), input.Y * random.NextSingle(),
            input.Z * random.NextSingle(), input.W * random.NextSingle());
    }

    // /// <summary>
    // /// Returns a new vector with random values between the two input vectors.
    // ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    // ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    // /// Common usage: <c>(Vector2.Zero, Vector2.One).Randomize()</c> <br/>
    // /// </summary>
    // public static MgVector4 RandomizeBetween(this (MgVector4 minValues, MgVector4 maxValues) input, Random? random = null)
    // {
    //     random ??= Random.Shared;
    //     return input.PiecewiseLerp(MgVector4.One.Randomize(random: random));
    // }
    //
    // /// <summary>
    // /// Returns a new vector with the same magnitude, but a random angle. By default, the new angle could be any direction.
    // ///   Providing a tolerance means that the new angle will be within that much in either direction from the current angle.
    // ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    // ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    // /// </summary>
    // public static MgVector4 RandomizeAngle(this MgVector4 input, float tolerance = MathConstants.HalfTurn, Random? random = null)
    // {
    //     random ??= Random.Shared;
    //     return input.RotatedBy(MathHelper.Lerp(-tolerance, tolerance, random.NextSingle()));
    // }
    //
    // /// <summary>
    // /// Returns a new vector with the same magnitude, but a random angle greater than or equal to
    // ///   <paramref name="min"/> and less than <paramref name="max"/>.
    // ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    // ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    // /// </summary>
    // public static MgVector4 RandomizeAngleBetween(this MgVector4 input, float min, float max, Random? random = null)
    // {
    //     random ??= Random.Shared;
    //     return input.AtAngle(MathHelper.Lerp(min, max, random.NextSingle()));
    // }
    //
    // /// <summary>
    // /// Returns a new vector with the same angle, but a random and lesser magnitude. If <paramref name="canInvert"/> is true,
    // ///   the new vector also has a 50% chance to be pointing backward.
    // ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    // ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    // /// </summary>
    // public static MgVector4 RandomizeMagnitude(this MgVector4 input, bool canInvert = false, Random? random = null)
    // {
    //     random ??= Random.Shared;
    //     if (canInvert)
    //     {
    //         return random.NextSingle() * random.NextSign() * input;
    //     }
    //     return random.NextSingle() * input;
    // }
    //
    // /// <summary>
    // /// Returns a new vector with the same angle, but a random magnitude greater than or equal to <paramref name="min"/>
    // ///   and less than <paramref name="max"/>.
    // ///   If <paramref name="canInvert"/> is true, the new vector also has a 50% chance to be pointing backward.
    // ///   <br/>You may optionally provide an existing <see cref="Random"/> instance.
    // ///   Random instance falls back to <see cref="Random.Shared"/> if none is provided. <br/>
    // /// </summary>
    // public static MgVector4 RandomizeMagnitudeBetween(this MgVector4 input, float min, float max, bool canInvert = false, Random? random = null)
    // {
    //     random ??= Random.Shared;
    //     if (canInvert)
    //     {
    //         return MathHelper.Lerp(min, max, random.NextSingle()) * random.NextSign() * input;
    //     }
    //     return MathHelper.Lerp(min, max, random.NextSingle()) * input;
    // }

    #endregion
}
