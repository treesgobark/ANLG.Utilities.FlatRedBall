using ANLG.Utilities.FlatRedBall.Constants;
using ANLG.Utilities.FlatRedBall.Extensions;
using MgMatrix = Microsoft.Xna.Framework.Matrix;
using MgVector2 = Microsoft.Xna.Framework.Vector2;
using MgVector3 = Microsoft.Xna.Framework.Vector3;

namespace ANLG.Utilities.FlatRedBall.Tests.Extensions;

[TestFixture]
public class Vector2ExtensionsTests
{
    [Test]
    public void ToVec3_Vector2WithoutParameter_ReturnsVector3()
    {
        Assert.That(new MgVector3(1, 2, 0), Is.EqualTo(new MgVector2(1, 2).ToVec3()));
    }
    
    [Test]
    public void ToVec3_Vector2WithParameter_ReturnsVector3()
    {
        Assert.That(new MgVector3(1, 2, 3), Is.EqualTo(new MgVector2(1, 2).ToVec3(3)));
    }
    
    [Test]
    public void GetSlope_Vector2_ReturnsYOverX()
    {
        Assert.That(2.5f, Is.EqualTo(new MgVector2(2, 5).GetSlope()));
    }
    
    [Test]
    public void GetQuadrant_PositivePositive_QuadrantI()
    {
        Assert.That(1, Is.EqualTo(new MgVector2(2, 5).GetQuadrant()));
    }
    
    [Test]
    public void GetQuadrant_NegativePositive_QuadrantII()
    {
        Assert.That(2, Is.EqualTo(new MgVector2(-2, 5).GetQuadrant()));
    }
    
    [Test]
    public void GetQuadrant_NegativeNegative_QuadrantIII()
    {
        Assert.That(3, Is.EqualTo(new MgVector2(-2, -5).GetQuadrant()));
    }
    
    [Test]
    public void GetQuadrant_PositiveNegative_QuadrantIV()
    {
        Assert.That(4, Is.EqualTo(new MgVector2(2, -5).GetQuadrant()));
    }
    
    [Test]
    public void GetQuadrant_NonZeroAndZero_QuadrantNone()
    {
        Assert.That(0, Is.EqualTo(new MgVector2(2, 0).GetQuadrant()));
    }
    
    [Test]
    public void GetQuadrant_ZeroAndNonZero_QuadrantNone()
    {
        Assert.That(0, Is.EqualTo(new MgVector2(0, 5).GetQuadrant()));
    }
    
    [Test]
    public void GetQuadrant_ZeroZero_QuadrantNone()
    {
        Assert.That(0, Is.EqualTo(new MgVector2(0, 0).GetQuadrant()));
    }

    [Test]
    public void GetClosestPoint_ReturnsClosestPoint()
    {
        MgVector2 testPoint = new(1, 7);
        MgVector2[] pointCollection =
        {
            new(0, 0),
            new(2, 5),
            new(-3, 8),
            new(-45, 301),
            new(45, -3),
        };
        
        Assert.That(new MgVector2(2, 5), Is.EqualTo(testPoint.GetClosestPoint(pointCollection)));
    }

    [Test]
    public void GetCcwAngle_FromQuadrantI_ReturnsBetween0AndPiOver2()
    {
        const float expected = MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(expected, Is.EqualTo(new MgVector2(2, 2).GetCcwAngle()).Within(0.01)!);
    }

    [Test]
    public void GetCcwAngle_FromQuadrantII_ReturnsBetweenPiOver2AndPi()
    {
        const float expected = 3 * MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(expected, Is.EqualTo(new MgVector2(-2, 2).GetCcwAngle()).Within(0.01)!);
    }

    [Test]
    public void GetCcwAngle_FromQuadrantIII_ReturnsBetweenPiAnd3PiOver2()
    {
        const float expected = 5 * MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(expected, Is.EqualTo(new MgVector2(-2, -2).GetCcwAngle()).Within(0.01)!);
    }

    [Test]
    public void GetCcwAngle_FromQuadrantIV_ReturnsBetween3PiOver2AndPi()
    {
        const float expected = 7 * MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(expected, Is.EqualTo(new MgVector2(2, -2).GetCcwAngle()).Within(0.01)!);
    }

    [Test]
    public void GetClosestAngle_FromQuadrantI_ReturnsBetween0AndPiOver2()
    {
        const float expected = MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(expected, Is.EqualTo(new MgVector2(2, 2).GetClosestAngle()).Within(0.01)!);
    }

    [Test]
    public void GetClosestAngle_FromQuadrantII_ReturnsBetweenPiOver2AndPi()
    {
        const float expected = 3 * MathConstants.RotateCcw * MathConstants.EighthTurn;
        Assert.That(expected, Is.EqualTo(new MgVector2(-2, 2).GetClosestAngle()).Within(0.01)!);
    }

    [Test]
    public void GetClosestAngle_FromQuadrantIII_ReturnsBetweenNegativePiAndNegativePiOver2()
    {
        const float expected = 3 * MathConstants.RotateCw * MathConstants.EighthTurn;
        Assert.That(expected, Is.EqualTo(new MgVector2(-2, -2).GetClosestAngle()).Within(0.01)!);
    }

    [Test]
    public void GetClosestAngle_FromQuadrantIV_ReturnsBetweenNegativePiOver2And0()
    {
        const float expected = MathConstants.RotateCw * MathConstants.EighthTurn;
        Assert.That(expected, Is.EqualTo(new MgVector2(2, -2).GetClosestAngle()).Within(0.01)!);
    }

    [Test]
    public void Extend_Vector2WithPositiveInput_ReturnsLongerVector()
    {
        var inputVector = new MgVector2(3f, 4f);
        var inputMagnitudeSq = inputVector.Length();
        var inputLength = 5f;
        
        Assert.That(new MgVector2(6f, 8f), Is.EqualTo(inputVector.Extend(inputLength)));
        Assert.That(inputMagnitudeSq + inputLength, Is.EqualTo(inputVector.Extend(inputLength).Length()).Within(0.01)!);
    }

    [Test]
    public void Transform_Vector2_ReturnsTransformedVector()
    {
        var inputVector = new MgVector2(4.8f, -4.3f);
        var transform = MgMatrix.CreateRotationZ(MathConstants.EighthTurn);
        
        Assert.That(MgVector2.Transform(inputVector, transform), Is.EqualTo(inputVector.Transform(transform)));
    }

    [Test]
    public void Translate_Vector2_ReturnsTranslatedVector()
    {
        Assert.That(new MgVector2(6, 6), Is.EqualTo(new MgVector2(5, 4).Translate(1, 2)));
    }

    [Test]
    public void Translate_Vector2WithoutParameters_ReturnsSameVector()
    {
        Assert.That(new MgVector2(5, 4), Is.EqualTo(new MgVector2(5, 4).Translate()));
    }
}
