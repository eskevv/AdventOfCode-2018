namespace AoC2018.DayThree;

struct Fabric
{
    public int Id { get; }
    public int X { get; }
    public int Y { get; }
    public int Width { get; }
    public int Height { get; }

    public Fabric(int id, int x, int y, int width, int height)
    {
        Id = id;
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public override string ToString()
    {
        return $"X:{X}, Y:{Y}, W:{Width}, H:{Height}";
    }
}