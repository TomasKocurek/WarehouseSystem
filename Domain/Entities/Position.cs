namespace Domain.Entities;
public class Position : IComparable<Position>
{
    public int X { get; set; }
    public int Y { get; set; }

    public int CompareTo(Position? other)
    {
        if (other == null) return 1;

        if (Y > other.Y) return 1;

        if (Y < other.Y) return -1;

        return X.CompareTo(other.X);
    }
}