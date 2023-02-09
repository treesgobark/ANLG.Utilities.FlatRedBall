using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Vector4 = Microsoft.Xna.Framework.Vector4;

namespace ANLG.Utilities.FlatRedBall.Extensions;

public static class SwizzleExtensions
{
    // desired syntax: vec.Swizzle('x', 'y', 'x') -> new Vector3(vec.X, vec.Y, vec.X)
    
    public const int SwizzleZeroIndex = -1;
    public const int SwizzleOneIndex = -2;
    
    private static readonly Dictionary<char, int> IndexLookup = new()
    {
        { 'x', 0 }, { 'X', 0 },
        { 'y', 1 }, { 'Y', 1 },
        { 'z', 2 }, { 'Z', 2 },
        { 'w', 3 }, { 'W', 3 },
                    
        { 'r', 0 }, { 'R', 0 },
        { 'g', 1 }, { 'G', 1 },
        { 'b', 2 }, { 'B', 2 },
        { 'a', 3 }, { 'A', 3 },
                    
        { 'u', 0 }, { 'U', 0 },
        { 'v', 1 }, { 'V', 1 },
        
        { '0', SwizzleZeroIndex },
        { '1', SwizzleOneIndex },
    };

    #region Vector2 Swizzles

    public static Vector2 Swizzle(this Vector2 input, char component0, char component1)
    {
        Vector2 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        
        return result;
    }

    public static Vector3 Swizzle(this Vector2 input, char component0, char component1, char component2)
    {
        Vector3 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        
        return result;
    }

    public static Vector4 Swizzle(this Vector2 input, char component0, char component1, char component2, char component3)
    {
        Vector4 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        result.W = GetVecComponentFromFormatChar(input, component3);
        
        return result;
    }

    #endregion

    #region Vector3 Swizzles

    public static Vector2 Swizzle(this Vector3 input, char component0, char component1)
    {
        Vector2 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        
        return result;
    }

    public static Vector3 Swizzle(this Vector3 input, char component0, char component1, char component2)
    {
        Vector3 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        
        return result;
    }

    public static Vector4 Swizzle(this Vector3 input, char component0, char component1, char component2, char component3)
    {
        Vector4 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        result.W = GetVecComponentFromFormatChar(input, component3);
        
        return result;
    }

    #endregion

    #region Vector4 Swizzles

    public static Vector2 Swizzle(this Vector4 input, char component0, char component1)
    {
        Vector2 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        
        return result;
    }

    public static Vector3 Swizzle(this Vector4 input, char component0, char component1, char component2)
    {
        Vector3 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        
        return result;
    }

    public static Vector4 Swizzle(this Vector4 input, char component0, char component1, char component2, char component3)
    {
        Vector4 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        result.W = GetVecComponentFromFormatChar(input, component3);
        
        return result;
    }

    #endregion

    private static float GetVecComponentFromFormatChar(Vector2 input, char component)
    {
        bool exists = IndexLookup.TryGetValue(component, out int inputVectorIndex);
        if (!exists) throw new ArgumentException($"Format string contains invalid character: '{component}'.");

        return input.GetComponent(inputVectorIndex);
    }

    private static float GetVecComponentFromFormatChar(Vector3 input, char component)
    {
        bool exists = IndexLookup.TryGetValue(component, out int inputVectorIndex);
        if (!exists) throw new ArgumentException($"Format string contains invalid character: '{component}'.");

        return input.GetComponent(inputVectorIndex);
    }

    private static float GetVecComponentFromFormatChar(Vector4 input, char component)
    {
        bool exists = IndexLookup.TryGetValue(component, out int inputVectorIndex);
        if (!exists) throw new ArgumentException($"Format string contains invalid character: '{component}'.");

        return input.GetComponent(inputVectorIndex);
    }
}
