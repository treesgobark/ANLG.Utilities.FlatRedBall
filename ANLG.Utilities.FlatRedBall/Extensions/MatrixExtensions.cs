using MgMatrix = Microsoft.Xna.Framework.Matrix;

namespace ANLG.Utilities.FlatRedBall.Extensions;

///
public static class MatrixExtensions
{
    /// <returns>This matrix's inverse, if it exists.</returns>
    public static MgMatrix Invert(this MgMatrix mat)
    {
        return MgMatrix.Invert(mat);
    }
}