using ANLG.Utilities.FlatRedBall.Constants;
using ANLG.Utilities.FlatRedBall.Extensions;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using MgMatrix = Microsoft.Xna.Framework.Matrix;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Vector4 = Microsoft.Xna.Framework.Vector4;

namespace ANLG.Utilities.FlatRedBall.Tests.Extensions;

[TestFixture]
public class Vector2ExtensionsTests
{
    [Test]
    public void ToVec3_Vector2WithoutParameter_ReturnsVector3()
    {
        Assert.That(new Vector3(1, 2, 0), Is.EqualTo(new Vector2(1, 2).ToVec3()));
    }
    
    [Test]
    public void ToVec3_Vector2WithParameter_ReturnsVector3()
    {
        Assert.That(new Vector3(1, 2, 3), Is.EqualTo(new Vector2(1, 2).ToVec3(3)));
    }
    
    [Test]
    public void GetSlope_Vector2_ReturnsYOverX()
    {
        Assert.That(2.5f, Is.EqualTo(new Vector2(2, 5).GetSlope()));
    }
    
    [Test]
    public void GetQuadrant_PositivePositive_QuadrantI()
    {
        Assert.That(new Vector2(2, 5).GetQuadrant(), Is.EqualTo(Quadrant.One));
    }
    
    [Test]
    public void GetQuadrant_NegativePositive_QuadrantII()
    {
        Assert.That(new Vector2(-2, 5).GetQuadrant(), Is.EqualTo(Quadrant.Two));
    }
    
    [Test]
    public void GetQuadrant_NegativeNegative_QuadrantIII()
    {
        Assert.That(new Vector2(-2, -5).GetQuadrant(), Is.EqualTo(Quadrant.Three));
    }
    
    [Test]
    public void GetQuadrant_PositiveNegative_QuadrantIV()
    {
        Assert.That(new Vector2(2, -5).GetQuadrant(), Is.EqualTo(Quadrant.Four));
    }
    
    [Test]
    public void GetQuadrant_PositiveAndZero_QuadrantNone()
    {
        Assert.That(new Vector2(2, 0).GetQuadrant(), Is.EqualTo(Quadrant.Right));
    }
    
    [Test]
    public void GetQuadrant_ZeroAndPositive_QuadrantNone()
    {
        Assert.That(new Vector2(0, 5).GetQuadrant(), Is.EqualTo(Quadrant.Up));
    }
    
    [Test]
    public void GetQuadrant_NegativeAndZero_QuadrantNone()
    {
        Assert.That(new Vector2(-2, 0).GetQuadrant(), Is.EqualTo(Quadrant.Left));
    }
    
    [Test]
    public void GetQuadrant_ZeroAndNegative_QuadrantNone()
    {
        Assert.That(new Vector2(0, -5).GetQuadrant(), Is.EqualTo(Quadrant.Down));
    }
    
    [Test]
    public void GetQuadrant_ZeroZero_QuadrantNone()
    {
        Assert.That(new Vector2(0, 0).GetQuadrant(), Is.EqualTo(Quadrant.Zero));
    }

    [Test]
    public void GetClosestPoint_ReturnsClosestPoint()
    {
        Vector2 testPoint = new(1, 7);
        var pointCollection = new Vector2[]
        {
            new(0, 0),
            new(2, 5),
            new(-3, 8),
            new(-45, 301),
            new(45, -3),
        };

        Assert.That(new Vector2(2, 5), Is.EqualTo(testPoint.GetClosestPoint(pointCollection)));
    }

    [Test]
    public void GetCcwAngle_FromQuadrantI_ReturnsBetween0AndPiOver2()
    {
        const float expected = MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(new Vector2(2, 2).GetCcwAngle(), Is.EqualTo(expected).Within(0.01)!);
    }

    [Test]
    public void GetCcwAngle_FromQuadrantII_ReturnsBetweenPiOver2AndPi()
    {
        const float expected = 3 * MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(new Vector2(-2, 2).GetCcwAngle(), Is.EqualTo(expected).Within(0.01)!);
    }

    [Test]
    public void GetCcwAngle_FromQuadrantIII_ReturnsBetweenPiAnd3PiOver2()
    {
        const float expected = 5 * MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(new Vector2(-2, -2).GetCcwAngle(), Is.EqualTo(expected).Within(0.01)!);
    }

    [Test]
    public void GetCcwAngle_FromQuadrantIV_ReturnsBetween3PiOver2AndPi()
    {
        const float expected = 7 * MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(new Vector2(2, -2).GetCcwAngle(), Is.EqualTo(expected).Within(0.01)!);
    }

    [Test]
    public void GetClosestAngle_FromQuadrantI_ReturnsBetween0AndPiOver2()
    {
        const float expected = MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(new Vector2(2, 2).GetClosestAngle(), Is.EqualTo(expected).Within(0.01)!);
    }

    [Test]
    public void GetClosestAngle_FromQuadrantII_ReturnsBetweenPiOver2AndPi()
    {
        const float expected = 3 * MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(new Vector2(-2, 2).GetClosestAngle(), Is.EqualTo(expected).Within(0.01)!);
    }

    [Test]
    public void GetClosestAngle_FromQuadrantIII_ReturnsBetweenNegativePiAndNegativePiOver2()
    {
        const float expected = 3 * MathConstants.RotateCw * MathConstants.EighthTurn;
        Assert.That(new Vector2(-2, -2).GetClosestAngle(), Is.EqualTo(expected).Within(0.01)!);
    }

    [Test]
    public void GetClosestAngle_FromQuadrantIV_ReturnsBetweenNegativePiOver2And0()
    {
        const float expected = MathConstants.RotateCw * MathConstants.EighthTurn;
        Assert.That(new Vector2(2, -2).GetClosestAngle(), Is.EqualTo(expected).Within(0.01)!);
    }

    [Test]
    public void Extend_Vector2WithPositiveInput_ReturnsLongerVector()
    {
        var inputVector = new Vector2(3f, 4f);
        var inputMagnitudeSq = inputVector.Length();
        var inputLength = 5f;
        
        Assert.That(new Vector2(6f, 8f), Is.EqualTo(inputVector.Extend(inputLength)));
        Assert.That(inputMagnitudeSq + inputLength, Is.EqualTo(inputVector.Extend(inputLength).Length()).Within(0.01)!);
    }

    [Test]
    public void Transform_Vector2_ReturnsTransformedVector()
    {
        var inputVector = new Vector2(4.8f, -4.3f);
        var transform = MgMatrix.CreateRotationZ(MathConstants.EighthTurn);
        
        Assert.That(Vector2.Transform(inputVector, transform), Is.EqualTo(inputVector.Transform(transform)));
    }

    [Test]
    public void Translate_Vector2_ReturnsTranslatedVector()
    {
        Assert.That(new Vector2(6, 6), Is.EqualTo(new Vector2(5, 4).Translate(1, 2)));
    }

    [Test]
    public void Translate_Vector2WithoutParameters_ReturnsSameVector()
    {
        Assert.That(new Vector2(5, 4), Is.EqualTo(new Vector2(5, 4).Translate()));
    }

    [Test]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public void Test1()
    {
        Vector2 input2 = Vector2.One.Randomize();
        Vector3 input3 = Vector3.One.Randomize();
        Vector4 input4 = Vector4.One.Randomize();
        
        input2.Swizzle('y', 'x');
        input2.Swizzle('y', 'x', '0');
        input2.Swizzle('y', 'x', '1', '0');
        
        Console.WriteLine(GC.GetTotalMemory(true));
        Console.WriteLine(GC.GetTotalMemory(true));

        input2.Swizzle('y', 'x');
        input2.Swizzle('y', 'x', '0');
        input2.Swizzle('y', 'x', '1', '0');

        Console.WriteLine(GC.GetTotalMemory(true));
        Console.WriteLine(GC.GetTotalMemory(true));

        Console.WriteLine(input2);
        Console.WriteLine(input2.Swizzle('y', 'x'));
        Console.WriteLine(input2.Swizzle('y', 'x', '0'));
        Console.WriteLine(input2.Swizzle('y', 'x', '1', '0'));
        Console.WriteLine();
        
        Console.WriteLine(input3);
        Console.WriteLine(input3.Swizzle('z', 'x'));
        Console.WriteLine(input3.Swizzle('z', 'x', '0'));
        Console.WriteLine(input3.Swizzle('z', 'x', '1', '0'));
        Console.WriteLine();
        
        Console.WriteLine(input4);
        Console.WriteLine(input4.Swizzle('y', 'w'));
        Console.WriteLine(input4.Swizzle('y', 'w', '0'));
        Console.WriteLine(input4.Swizzle('y', 'w', '1', '0'));
        Console.WriteLine();
        
        Console.WriteLine(GC.GetTotalMemory(true));
        Console.WriteLine(GC.GetTotalMemory(true));
    }
}
