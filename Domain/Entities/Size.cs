namespace Domain.Entities;
public class Size
{
    /// <summary>
    /// Výška krabice (mm)
    /// </summary>
    public int Height { get; set; }
    /// <summary>
    /// Šířka krabice (mm)
    /// </summary>
    public int Width { get; set; }
    /// <summary>
    /// Hloubka krabice (mm)
    /// </summary>
    public int Depth { get; set; }

    public Size(int height, int width, int depth)
    {
        Height = height;
        Width = width;
        Depth = depth;
    }

    public int FloorArea => Depth * Width;
    public int Volume => Width * Depth * Depth;
}
