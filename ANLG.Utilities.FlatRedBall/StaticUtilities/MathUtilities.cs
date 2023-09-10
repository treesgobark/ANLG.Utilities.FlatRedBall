namespace ANLG.Utilities.FlatRedBall.StaticUtilities;

/// 
public static class MathUtilities
{
    /// <summary>
    /// Takes a value <paramref name="x"/> and regulates it to the range 0 &lt;= <paramref name="x"/> &lt; <paramref name="m"/>
    ///   such that it cycles through the range. Equivalent to a mathematical modulus.
    ///   Will give equivalent values to the '%' operator for non-negative numbers.
    /// </summary>
    public static int Regulate(this int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }
    
    /// <summary><inheritdoc cref="Regulate(int,int)"/></summary>
    public static float Regulate(this float x, int m)
    {
        float r = x % m;
        return r < 0 ? r + m : r;
    }
    
    /// <summary><inheritdoc cref="Regulate(int,int)"/></summary>
    public static float Regulate(this float x, float m)
    {
        float r = x % m;
        return r < 0 ? r + m : r;
    }
    
    /// <summary><inheritdoc cref="Regulate(int,int)"/></summary>
    public static double Regulate(this double x, int m)
    {
        double r = x % m;
        return r < 0 ? r + m : r;
    }
    
    /// <summary><inheritdoc cref="Regulate(int,int)"/></summary>
    public static double Regulate(this double x, double m)
    {
        double r = x % m;
        return r < 0 ? r + m : r;
    }
}
