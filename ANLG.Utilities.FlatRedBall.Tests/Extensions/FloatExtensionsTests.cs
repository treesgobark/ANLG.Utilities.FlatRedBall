using ANLG.Utilities.FlatRedBall.Extensions;

namespace ANLG.Utilities.FlatRedBall.Tests.Extensions;

[TestFixture]
public class FloatExtensionsTests
{
    [Test]
    public void SignDifference_BothPositive_Returns1()
    {
        Assert.That(1, Is.EqualTo(2.3f.SignDifference(3.78f)));
    }

    [Test]
    public void SignDifference_BothNegative_Returns1()
    {
        Assert.That(1, Is.EqualTo((-3.01f).SignDifference(-5.8f)));
    }

    [Test]
    public void SignDifference_FirstPositiveSecondNegative_ReturnsMinus1()
    {
        Assert.That(-1, Is.EqualTo(6.0f.SignDifference(-6.5f)));
    }

    [Test]
    public void SignDifference_FirstNegativeSecondPositive_ReturnsMinus1()
    {
        Assert.That(-1, Is.EqualTo((-567.3f).SignDifference(69f)));
    }
}
