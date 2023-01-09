using FlatRedBall;
using FlatRedBall.Content.Math.Geometry;
using FlatRedBall.Content.Polygon;
using FlatRedBall.Math.Geometry;
using Color = Microsoft.Xna.Framework.Color;

namespace ANLG.Utilities.FlatRedBall.Extensions;

///
public static class FrbShapeExtensions
{
    private static void SetRelativeValuesOn(this AxisAlignedRectangleSave from, AxisAlignedRectangle to)
    {
        to.RelativeX = from.X;
        to.RelativeY = from.Y;
        to.RelativeZ = from.Z;
        to.ScaleX = from.ScaleX;
        to.ScaleY = from.ScaleY;
        to.Color = new Color((byte) (from.Red * (double) byte.MaxValue),
            (byte) (from.Green * (double) byte.MaxValue),
            (byte) (from.Blue * (double) byte.MaxValue),
            (byte) (from.Alpha * (double) byte.MaxValue));
        to.Name = from.Name;
    }

    private static AxisAlignedRectangle ToRelativeAxisAlignedRectangle(this AxisAlignedRectangleSave from)
    {
        AxisAlignedRectangle to = new();
        SetRelativeValuesOn(from, to);
        return to;
    }
    
    private static void SetRelativeValuesOn(this AxisAlignedCubeSave from, AxisAlignedCube to)
    {
        to.RelativeX = from.X;
        to.RelativeY = from.Y;
        to.RelativeZ = from.Z;
        to.ScaleX = from.ScaleX;
        to.ScaleY = from.ScaleY;
        to.Color = new Color((byte) (from.Red * (double) byte.MaxValue),
            (byte) (from.Green * (double) byte.MaxValue),
            (byte) (from.Blue * (double) byte.MaxValue),
            (byte) (from.Alpha * (double) byte.MaxValue));
        to.Name = from.Name;
    }

    private static AxisAlignedCube ToRelativeAxisAlignedCube(this AxisAlignedCubeSave from)
    {
        AxisAlignedCube to = new();
        SetRelativeValuesOn(from, to);
        return to;
    }
    
    private static void SetRelativeValuesOn(this PolygonSave from, Polygon to)
    {
        to.RelativeX = from.X;
        to.RelativeY = from.Y;
        to.RelativeZ = from.Z;
        to.RotationZ = from.RotationZ;
        to.Points = (Point[])from.Points.Clone();
        to.Color = new Color((byte) (from.Red * (double) byte.MaxValue),
            (byte) (from.Green * (double) byte.MaxValue),
            (byte) (from.Blue * (double) byte.MaxValue),
            (byte) (from.Alpha * (double) byte.MaxValue));
        to.Name = from.Name;
    }

    private static Polygon ToRelativePolygon(this PolygonSave from)
    {
        Polygon to = new();
        SetRelativeValuesOn(from, to);
        return to;
    }
    
    private static void SetRelativeValuesOn(this CircleSave from, Circle to)
    {
        to.RelativeX = from.X;
        to.RelativeY = from.Y;
        to.RelativeZ = from.Z;
        to.Radius = from.Radius;
        to.Color = new Color((byte) (from.Red * (double) byte.MaxValue),
            (byte) (from.Green * (double) byte.MaxValue),
            (byte) (from.Blue * (double) byte.MaxValue),
            (byte) (from.Alpha * (double) byte.MaxValue));
        to.Name = from.Name;
    }

    private static Circle ToRelativeCircle(this CircleSave from)
    {
        Circle to = new();
        SetRelativeValuesOn(from, to);
        return to;
    }
    
    private static void SetRelativeValuesOn(this SphereSave from, Sphere to)
    {
        to.RelativeX = from.X;
        to.RelativeY = from.Y;
        to.RelativeZ = from.Z;
        to.Radius = from.Radius;
        to.Color = new Color((byte) (from.Red * (double) byte.MaxValue),
            (byte) (from.Green * (double) byte.MaxValue),
            (byte) (from.Blue * (double) byte.MaxValue),
            (byte) (from.Alpha * (double) byte.MaxValue));
        to.Name = from.Name;
    }

    private static Sphere ToRelativeSphere(this SphereSave from)
    {
        Sphere to = new();
        SetRelativeValuesOn(from, to);
        return to;
    }

    /// <summary>
    /// Reads all shapes in '<paramref name="from"/>' with '<paramref name="name"/>' as their name and
    ///   overwrites the lists in "<paramref name="to"/>' with the found shapes. All shapes' parents are set to
    ///   '<paramref name="newParent"/>'. You may provide a null parent to not parent the shapes.
    /// </summary>
    private static void SetShapesByName(this ShapeCollectionSave from, ShapeCollection to, string name, PositionedObject newParent)
    {
        int toRectIndex = 0;
        foreach (var rectangleSave in from.AxisAlignedRectangleSaves)
        {
            if (rectangleSave.Name != name)
            {
                continue;
            }

            AxisAlignedRectangle copyToRectangle;
            if (toRectIndex < to.AxisAlignedRectangles.Count)
            {
                copyToRectangle = to.AxisAlignedRectangles[toRectIndex++];
            }
            else
            {
                copyToRectangle = new AxisAlignedRectangle();
                copyToRectangle.AttachTo(newParent);
            }
            rectangleSave.SetRelativeValuesOn(copyToRectangle);
        }
        foreach (var cubeSave in from.AxisAlignedCubeSaves)
        {
            if (cubeSave.Name != name)
            {
                continue;
            }

            AxisAlignedCube copyToCube;
            if (toRectIndex < to.AxisAlignedCubes.Count)
            {
                copyToCube = to.AxisAlignedCubes[toRectIndex++];
            }
            else
            {
                copyToCube = new AxisAlignedCube();
                copyToCube.AttachTo(newParent);
            }
            cubeSave.SetRelativeValuesOn(copyToCube);
        }
        foreach (var polygonSave in from.PolygonSaves)
        {
            if (polygonSave.Name != name)
            {
                continue;
            }

            Polygon copyToPolygon;
            if (toRectIndex < to.Polygons.Count)
            {
                copyToPolygon = to.Polygons[toRectIndex++];
            }
            else
            {
                copyToPolygon = new Polygon();
                copyToPolygon.AttachTo(newParent);
            }
            polygonSave.SetRelativeValuesOn(copyToPolygon);
        }
        foreach (var circleSave in from.CircleSaves)
        {
            if (circleSave.Name != name)
            {
                continue;
            }

            Circle copyToCircle;
            if (toRectIndex < to.Circles.Count)
            {
                copyToCircle = to.Circles[toRectIndex++];
            }
            else
            {
                copyToCircle = new Circle();
                copyToCircle.AttachTo(newParent);
            }
            circleSave.SetRelativeValuesOn(copyToCircle);
        }
        foreach (var sphereSave in from.SphereSaves)
        {
            if (sphereSave.Name != name)
            {
                continue;
            }

            Sphere copyToSphere;
            if (toRectIndex < to.Spheres.Count)
            {
                copyToSphere = to.Spheres[toRectIndex++];
            }
            else
            {
                copyToSphere = new Sphere();
                copyToSphere.AttachTo(newParent);
            }
            sphereSave.SetRelativeValuesOn(copyToSphere);
        }
    }
}
