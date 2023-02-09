using MgVector2 = Microsoft.Xna.Framework.Vector2;
using MgVector3 = Microsoft.Xna.Framework.Vector3;
using MgVector4 = Microsoft.Xna.Framework.Vector4;

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

    public static MgVector2 Swizzle(this MgVector2 input, char component0, char component1)
    {
        MgVector2 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        
        return result;
    }

    public static MgVector3 Swizzle(this MgVector2 input, char component0, char component1, char component2)
    {
        MgVector3 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        
        return result;
    }

    public static MgVector4 Swizzle(this MgVector2 input, char component0, char component1, char component2, char component3)
    {
        MgVector4 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        result.W = GetVecComponentFromFormatChar(input, component3);
        
        return result;
    }

    #endregion

    #region Vector3 Swizzles

    public static MgVector2 Swizzle(this MgVector3 input, char component0, char component1)
    {
        MgVector2 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        
        return result;
    }

    public static MgVector3 Swizzle(this MgVector3 input, char component0, char component1, char component2)
    {
        MgVector3 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        
        return result;
    }

    public static MgVector4 Swizzle(this MgVector3 input, char component0, char component1, char component2, char component3)
    {
        MgVector4 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        result.W = GetVecComponentFromFormatChar(input, component3);
        
        return result;
    }

    #endregion

    #region Vector4 Swizzles

    public static MgVector2 Swizzle(this MgVector4 input, char component0, char component1)
    {
        MgVector2 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        
        return result;
    }

    public static MgVector3 Swizzle(this MgVector4 input, char component0, char component1, char component2)
    {
        MgVector3 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        
        return result;
    }

    public static MgVector4 Swizzle(this MgVector4 input, char component0, char component1, char component2, char component3)
    {
        MgVector4 result = default;
        
        result.X = GetVecComponentFromFormatChar(input, component0);
        result.Y = GetVecComponentFromFormatChar(input, component1);
        result.Z = GetVecComponentFromFormatChar(input, component2);
        result.W = GetVecComponentFromFormatChar(input, component3);
        
        return result;
    }

    #endregion

    private static float GetVecComponentFromFormatChar(MgVector2 input, char component)
    {
        bool exists = IndexLookup.TryGetValue(component, out int inputVectorIndex);
        if (!exists) throw new ArgumentException($"Format string contains invalid character: '{component}'.");

        return input.GetComponent(inputVectorIndex);
    }

    private static float GetVecComponentFromFormatChar(MgVector3 input, char component)
    {
        bool exists = IndexLookup.TryGetValue(component, out int inputVectorIndex);
        if (!exists) throw new ArgumentException($"Format string contains invalid character: '{component}'.");

        return input.GetComponent(inputVectorIndex);
    }

    private static float GetVecComponentFromFormatChar(MgVector4 input, char component)
    {
        bool exists = IndexLookup.TryGetValue(component, out int inputVectorIndex);
        if (!exists) throw new ArgumentException($"Format string contains invalid character: '{component}'.");

        return input.GetComponent(inputVectorIndex);
    }
}
