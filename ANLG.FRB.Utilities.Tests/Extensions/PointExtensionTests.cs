using ANLG.Utilities.Extensions;
using FrbPoint = FlatRedBall.Math.Geometry.Point;
using MgVector2 = Microsoft.Xna.Framework.Vector2;

namespace ANLG.FRB.Utilities.Tests;

[TestFixture]
public class PointExtensionTests
{
    [Test]
    public void Add_AnotherPoint_ReturnsSum()
    {
        FrbPoint input1 = new(1.5, 2.5);
        FrbPoint input2 = new(-2.5, -1.25);

        Assert.That(new FrbPoint(-1, 1.25), Is.EqualTo(input1.Add(input2)));
    }
    
    [Test]
    public void Add_Vector2_ReturnsSum()
    {
        FrbPoint input1 = new(1.5, 2.5);
        MgVector2 input2 = new(-2.5f, -1.25f);

        Assert.That(new FrbPoint(-1, 1.25), Is.EqualTo(input1.Add(input2)));
    }
    
    [Test]
    public void Multiply_AnotherPoint_ReturnsProduct()
    {
        FrbPoint input1 = new(1.5, 2.5);
        FrbPoint input2 = new(-2.5f, 2.5f);

        Assert.That(new FrbPoint(-3.75, 6.25), Is.EqualTo(input1.Multiply(input2)));
    }
    
    [Test]
    public void Multiply_Vector2_ReturnsProduct()
    {
        FrbPoint input1 = new(1.5, 2.5);
        MgVector2 input2 = new(-2.5f, 2.5f);

        Assert.That(new FrbPoint(-3.75, 6.25), Is.EqualTo(input1.Multiply(input2)));
    }
    
    [Test]
    public void Subtract_AnotherPoint_ReturnsDifference()
    {
        FrbPoint input1 = new(1.5, 2.5);
        FrbPoint input2 = new(-2.5f, 2.5f);

        Assert.That(new FrbPoint(4, 0), Is.EqualTo(input1.Subtract(input2)));
    }
    
    [Test]
    public void Subtract_Vector2_ReturnsDifference()
    {
        FrbPoint input1 = new(1.5, 2.5);
        MgVector2 input2 = new(-2.5f, 2.5f);

        Assert.That(new FrbPoint(4, 0), Is.EqualTo(input1.Subtract(input2)));
    }
    
    [Test]
    public void Divide_AnotherPoint_ReturnsQuotient()
    {
        FrbPoint input1 = new(5, 2.5);
        FrbPoint input2 = new(-2, 2.5f);

        Assert.That(new FrbPoint(-2.5, 1), Is.EqualTo(input1.Divide(input2)));
    }
    
    [Test]
    public void Divide_Vector2_ReturnsQuotient()
    {
        FrbPoint input1 = new(5, 2.5);
        MgVector2 input2 = new(-2f, 2.5f);

        Assert.That(new FrbPoint(-2.5, 1), Is.EqualTo(input1.Divide(input2)));
    }
}
