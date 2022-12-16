using ANLG.Utilities.Extensions;
using MgMatrix = Microsoft.Xna.Framework.Matrix;
namespace ANLG.FRB.Utilities.Tests;

[TestFixture]
public class MatrixExtensionsTests
{
    [Test]
    public void Invert_Identity_ReturnsIdentity()
    {
        Assert.That(MgMatrix.Identity, Is.EqualTo(MgMatrix.Identity.Invert()));
    }
    
    [Test]
    public void Invert_Matrix_ReturnsInverse()
    {
        MgMatrix input = new MgMatrix(
            1, 2, 3, 4,
            2, 1, 2, 3,
            3, 2, 1, 2,
            4, 3, 2, 1);
        
        MgMatrix expected = new MgMatrix(
            -4, 5, 0, 1,
            5, -10, 5, 0,
            0, 5, -10, 5,
            1, 0, 5, -4) * 0.1f;
        
        Assert.That(expected, Is.EqualTo(input.Invert()));
    }
}
