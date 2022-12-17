using ANLG.Utilities.FlatRedBall.Extensions;
using FlatRedBall.Input;
using MgVector2 = Microsoft.Xna.Framework.Vector2;
namespace ANLG.Utilities.FlatRedBall.Tests.Extensions;

[TestFixture]
public class I2DInputExtensionsTests
{
    private class Test2DInput : I2DInput
    {
        public Test2DInput(float x, float y, float xVelocity, float yVelocity, float magnitude)
        {
            X = x;
            Y = y;
            XVelocity = xVelocity;
            YVelocity = yVelocity;
            Magnitude = magnitude;
        }

        public float X { get; }
        public float Y { get; }
        public float XVelocity { get; }
        public float YVelocity { get; }
        public float Magnitude { get; }
    }
    
    [Test]
    public void Deconstruct_NonZeroInput_ReturnsNonZero()
    {
        (MgVector2 position, MgVector2 velocity, float magnitude) =
            new Test2DInput(0.5f, 0.5f, 1.5f, 2.5f, 0.707f);
        
        Assert.That(position, Is.EqualTo(new MgVector2(0.5f, 0.5f)));
        Assert.That(velocity, Is.EqualTo(new MgVector2(1.5f, 2.5f)));
        Assert.That(magnitude, Is.EqualTo(0.707f));
    }
    
    [Test]
    public void Deconstruct_NullInput_ReturnsZero()
    {
        I2DInput? input = null;
        (MgVector2 position, MgVector2 velocity, float magnitude) = input;
        
        Assert.That(position, Is.EqualTo(new MgVector2(0f, 0f)));
        Assert.That(velocity, Is.EqualTo(new MgVector2(0f, 0f)));
        Assert.That(magnitude, Is.EqualTo(0f));
    }

    [Test]
    public void GetNormalizedPositionOrZero_NonZeroMagnitude_ReturnsNonZero()
    {
        var input = new Test2DInput(0.5f, 0.5f, 1.5f, 2.5f, 0.707f);
        var normalizedPosition = input.GetNormalizedPositionOrZero();
        
        Assert.That(normalizedPosition.X, Is.EqualTo(0.707f).Within(0.01f));
        Assert.That(normalizedPosition.Y, Is.EqualTo(0.707f).Within(0.01f));
    }

    [Test]
    public void GetNormalizedPositionOrZero_ZeroMagnitude_ReturnsZero()
    {
        var input = new Test2DInput(0f, 0f, 1.5f, 2.5f, 0f);
        var normalizedPosition = input.GetNormalizedPositionOrZero();
        
        Assert.That(normalizedPosition, Is.EqualTo(new MgVector2(0f, 0f)));
    }

    [Test]
    public void GetNormalizedPositionOrZero_NullInput_ReturnsZero()
    {
        I2DInput? input = null;
        var normalizedPosition = input.GetNormalizedPositionOrZero();
        
        Assert.That(normalizedPosition, Is.EqualTo(new MgVector2(0f, 0f)));
    }
}
