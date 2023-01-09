using FlatRedBall;
using FlatRedBall.Content.Math.Geometry;
using FlatRedBall.Content.Polygon;
using FlatRedBall.Math.Geometry;
using Color = Microsoft.Xna.Framework.Color;
// ReSharper disable UnusedMember.Global

namespace ANLG.Utilities.FlatRedBall.Extensions;

///
public static class FrbShapeExtensions
{
    /// <summary>
    /// Maps a shape save into an shape, interpreting the save values as relative positions and overwriting the existing shape values.
    /// </summary>
    public static void MapShapeRelative(this AxisAlignedRectangleSave from, AxisAlignedRectangle to)
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
    
    /// <summary>
    /// Maps a shape save into an shape, interpreting the save values as relative positions and overwriting the existing shape values.
    /// </summary>
    public static void MapShapeRelative(this AxisAlignedCubeSave from, AxisAlignedCube to)
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
    
    /// <summary>
    /// Maps a shape save into an shape, interpreting the save values as relative positions and overwriting the existing shape values.
    /// </summary>
    public static void MapShapeRelative(this PolygonSave from, Polygon to)
    {
        to.RelativeX = from.X;
        to.RelativeY = from.Y;
        to.RelativeZ = from.Z;
        to.RotationZ = from.RotationZ;
        to.Points = (Point[])from.Points!.Clone();
        to.Color = new Color((byte) (from.Red * (double) byte.MaxValue),
            (byte) (from.Green * (double) byte.MaxValue),
            (byte) (from.Blue * (double) byte.MaxValue),
            (byte) (from.Alpha * (double) byte.MaxValue));
        to.Name = from.Name;
    }
    
    /// <summary>
    /// Maps a shape save into an shape, interpreting the save values as relative positions and overwriting the existing shape values.
    /// </summary>
    public static void MapShapeRelative(this CircleSave from, Circle to)
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
    
    /// <summary>
    /// Maps a shape save into an shape, interpreting the save values as relative positions and overwriting the existing shape values.
    /// </summary>
    public static void MapShapeRelative(this SphereSave from, Sphere to)
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

    /// <summary>
    /// Maps a shape save to a new instance of a shape, interpreting the save values as relative positions.
    /// </summary>
    public static AxisAlignedRectangle MapToNewShapeRelative(this AxisAlignedRectangleSave from, PositionedObject? newParent = null)
    {
        AxisAlignedRectangle to = new();
        MapShapeRelative(from, to);
        
        if (newParent is not null)
        {
            to.AttachTo(newParent);
        }
        
        return to;
    }

    /// <summary>
    /// Maps a shape save to a new instance of a shape, interpreting the save values as relative positions.
    /// </summary>
    public static AxisAlignedCube MapToNewShapeRelative(this AxisAlignedCubeSave from, PositionedObject? newParent = null)
    {
        AxisAlignedCube to = new();
        MapShapeRelative(from, to);
        
        if (newParent is not null)
        {
            to.AttachTo(newParent);
        }
        
        return to;
    }

    /// <summary>
    /// Maps a shape save to a new instance of a shape, interpreting the save values as relative positions.
    /// </summary>
    public static Polygon MapToNewShapeRelative(this PolygonSave from, PositionedObject? newParent = null)
    {
        Polygon to = new();
        MapShapeRelative(from, to);
        
        if (newParent is not null)
        {
            to.AttachTo(newParent);
        }
        
        return to;
    }

    /// <summary>
    /// Maps a shape save to a new instance of a shape, interpreting the save values as relative positions.
    /// </summary>
    public static Circle MapToNewShapeRelative(this CircleSave from, PositionedObject? newParent = null)
    {
        Circle to = new();
        MapShapeRelative(from, to);
        
        if (newParent is not null)
        {
            to.AttachTo(newParent);
        }
        
        return to;
    }

    /// <summary>
    /// Maps a shape save to a new instance of a shape, interpreting the save values as relative positions.
    /// </summary>
    public static Sphere MapToNewShapeRelative(this SphereSave from, PositionedObject? newParent = null)
    {
        Sphere to = new();
        MapShapeRelative(from, to);
        
        if (newParent is not null) { to.AttachTo(newParent); }
        
        return to;
    }

    /// <summary>
    /// Reads all shapes in '<paramref name="from"/>' with '<paramref name="name"/>' as their name and
    ///   overwrites the lists in "<paramref name="to"/>' with the found shapes. All shapes' parents are set to
    ///   '<paramref name="newParent"/>'. You may provide a null parent to not parent the shapes.
    /// </summary>
    public static void SetShapesByName(this ShapeCollectionSave from, ShapeCollection to, string name, PositionedObject? newParent)
    {
        int toShapeIndex = 0;
        foreach (var shapeSave in from.AxisAlignedRectangleSaves!)
        {
            if (shapeSave.Name != name) { continue; }

            if (toShapeIndex >= to.AxisAlignedRectangles!.Count)
            {
                var newShape = MapToNewShapeRelative(shapeSave, newParent);
                to.AxisAlignedRectangles.Add(newShape);
                continue;
            }
            
            var copyToShape = to.AxisAlignedRectangles[toShapeIndex++];
            if (copyToShape is null)
            {
                var newShape = MapToNewShapeRelative(shapeSave, newParent);
                to.AxisAlignedRectangles[toShapeIndex] = newShape;
            }
            else
            {
                shapeSave.MapShapeRelative(copyToShape);
            }
        }
        
        foreach (var shapeSave in from.AxisAlignedCubeSaves!)
        {
            if (shapeSave.Name != name) { continue; }

            if (toShapeIndex >= to.AxisAlignedCubes!.Count)
            {
                var newShape = MapToNewShapeRelative(shapeSave, newParent);
                to.AxisAlignedCubes.Add(newShape);
                continue;
            }
            
            var copyToShape = to.AxisAlignedCubes[toShapeIndex++];
            if (copyToShape is null)
            {
                var newShape = MapToNewShapeRelative(shapeSave, newParent);
                to.AxisAlignedCubes[toShapeIndex] = newShape;
            }
            else
            {
                shapeSave.MapShapeRelative(copyToShape);
            }
        }
        
        foreach (var shapeSave in from.PolygonSaves!)
        {
            if (shapeSave.Name != name) { continue; }

            if (toShapeIndex >= to.Polygons!.Count)
            {
                var newShape = MapToNewShapeRelative(shapeSave, newParent);
                to.Polygons.Add(newShape);
                continue;
            }
            
            var copyToShape = to.Polygons[toShapeIndex++];
            if (copyToShape is null)
            {
                var newShape = MapToNewShapeRelative(shapeSave, newParent);
                to.Polygons[toShapeIndex] = newShape;
            }
            else
            {
                shapeSave.MapShapeRelative(copyToShape);
            }
        }
        
        foreach (var shapeSave in from.CircleSaves!)
        {
            if (shapeSave.Name != name) { continue; }

            if (toShapeIndex >= to.Circles!.Count)
            {
                var newShape = MapToNewShapeRelative(shapeSave, newParent);
                to.Circles.Add(newShape);
                continue;
            }
            
            var copyToShape = to.Circles[toShapeIndex++];
            if (copyToShape is null)
            {
                var newShape = MapToNewShapeRelative(shapeSave, newParent);
                to.Circles[toShapeIndex] = newShape;
            }
            else
            {
                shapeSave.MapShapeRelative(copyToShape);
            }
        }
        
        foreach (var shapeSave in from.SphereSaves!)
        {
            if (shapeSave.Name != name) { continue; }

            if (toShapeIndex >= to.Spheres!.Count)
            {
                var newShape = MapToNewShapeRelative(shapeSave, newParent);
                to.Spheres.Add(newShape);
                continue;
            }
            
            var copyToShape = to.Spheres[toShapeIndex++];
            if (copyToShape is null)
            {
                var newShape = MapToNewShapeRelative(shapeSave, newParent);
                to.Spheres[toShapeIndex] = newShape;
            }
            else
            {
                shapeSave.MapShapeRelative(copyToShape);
            }
        }
    }

    // /// <summary>
    // /// Checks this shape collection to see that all its shape lists are filled with only non-null values.
    // /// <br/>Returns true if validated.
    // /// </summary>
    // public static bool ValidateShapeCollectionNonNull(this ShapeCollection collection)
    // {
    //     foreach (var shape in collection.AxisAlignedRectangles) { if (shape is null) { return false; } }
    //     foreach (var shape in collection.AxisAlignedCubes) { if (shape is null) { return false; } }
    //     foreach (var shape in collection.Polygons) { if (shape is null) { return false; } }
    //     foreach (var shape in collection.Circles) { if (shape is null) { return false; } }
    //     foreach (var shape in collection.Spheres) { if (shape is null) { return false; } }
    //     
    //     return true;
    // }
    //
    // /// <summary>
    // /// Checks if this shape collection's shape lists are initialized and that all shapes are non-null. Null lists will
    // ///   be initialized and null shapes will be removed.
    // /// <br/>Returns true if collection was updated.
    // /// </summary>
    // public static bool EnsureShapeCollectionNonNull(this ShapeCollection collection)
    // {
    //     foreach (var shape in collection.AxisAlignedRectangles) { if (shape is null) { return false; } }
    //     foreach (var shape in collection.AxisAlignedCubes) { if (shape is null) { return false; } }
    //     foreach (var shape in collection.Polygons) { if (shape is null) { return false; } }
    //     foreach (var shape in collection.Circles) { if (shape is null) { return false; } }
    //     foreach (var shape in collection.Spheres) { if (shape is null) { return false; } }
    //     
    //     return true;
    // }
}
