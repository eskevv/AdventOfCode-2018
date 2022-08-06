namespace AoC2018.DayThree;

/* --No Matter How You Slice It: 
// PART 1:
// How many square inches of fabric are within two or more claims?
// 
// PART 2:
// What is the ID of the only claim that doesn't overlap?
*/

class PuzzleSolver : ISolver
{
    private int _fabricSquareInches;
    private int? _separatedFabricId;

    public void Solve()
    {
        // parse
        string[] fabricDimensions = File.ReadAllLines("Day3/puzzle.txt");
        Fabric[] fabrics = GetAllFabrics(fabricDimensions);

        // solve
        _fabricSquareInches = FindFabricSquareInches(fabrics);
        _separatedFabricId = FindSeparatedFabric(fabrics);

        Console.WriteLine(_separatedFabricId);
    }

    #region PartOne

    int FindFabricSquareInches(Fabric[] fabrics)
    {
        var fabricAreas = new HashSet<Location>();

        for (int x = 0; x < fabrics.Length; x++)
        {
            for (int y = x + 1; y < fabrics.Length; y++)
            {
                if (!OverLapping(fabrics[x], fabrics[y]))
                    continue;

                Location[] overlaps = GetOverlaps(fabrics[x], fabrics[y]);
                Array.ForEach(overlaps, x => { fabricAreas.Add(x); });
            }
        }

        return fabricAreas.Count;
    }

    Location[] GetOverlaps(Fabric a, Fabric b)
    {
        var output = new List<Location>();

        int right1 = a.X + a.Width;
        int right2 = b.X + b.Width;
        int bottom1 = a.Y + a.Height;
        int bottom2 = b.Y + b.Height;

        int xIntersect = a.X > b.X ? a.X : b.X;
        int yIntersect = a.Y > b.Y ? a.Y : b.Y;
        int xEnd = right1 < right2 ? right1 : right2;
        int yEnd = bottom1 < bottom2 ? bottom1 : bottom2;

        int xOverlaps = xEnd - xIntersect;
        int yOverlaps = yEnd - yIntersect;

        for (int xx = 0; xx < xOverlaps; xx++)
        {
            for (int yy = 0; yy < yOverlaps; yy++)
            {
                var location = new Location(xIntersect + xx, yIntersect + yy);
                output.Add(location);
            }
        }

        return output.ToArray();
    }

    #endregion

    #region PartTwo

    int? FindSeparatedFabric(Fabric[] fabrics)
    {
        for (int x = 0; x < fabrics.Length; x++)
        {
            for (int y = 0; y < fabrics.Length; y++)
            {
                if (fabrics[x].Equals(fabrics[y]))
                    continue;

                if (OverLapping(fabrics[x], fabrics[y]))
                    break;

                if (y == fabrics.Length - 1)
                    return fabrics[x].Id;
            }
        }

        return null;
    }

    #endregion

    bool OverLapping(Fabric a, Fabric b)
    {
        int aRight = a.X + a.Width;
        int bRight = b.X + b.Width;
        int aBottom = a.Y + a.Height;
        int bBottom = b.Y + b.Height;

        return (a.X <= bRight && aRight >= b.X && a.Y <= bBottom && aBottom >= b.Y);
    }

    Fabric[] GetAllFabrics(string[] data)
    {
        var output = new List<Fabric>();

        foreach (var item in data)
        {
            var rectangle = ParseFabric(item);
            output.Add(rectangle);
        }

        return output.ToArray();
    }

    Fabric ParseFabric(string data)
    {
        int at = data.IndexOf('@');
        int comma = data.IndexOf(',');
        int colon = data.IndexOf(':');
        int by = data.IndexOf('x');

        string id = data.Substring(1, at - 1);
        string xPos = data.Substring(at + 2, comma - (at + 2));
        string yPos = data.Substring(comma + 1, colon - (comma + 1));
        string width = data.Substring(colon + 2, by - (colon + 2));
        string height = data.Substring(by + 1, data.Length - (by + 1));

        int i = int.Parse(id);
        int x = int.Parse(xPos);
        int y = int.Parse(yPos);
        int w = int.Parse(width);
        int h = int.Parse(height);

        return new Fabric(i, x, y, w, h);
    }
}