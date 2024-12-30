Cell[][] map = LoadMap();
var path = DetermineGuardPath(map);
int count = CountPossibleSquares(map, path);

Console.WriteLine(count);


static Cell[][] LoadMap()
{
    return File.ReadAllLines("input.txt")
        .Select(line => line.Select(c => c switch
        {
            '.' => new Cell(MapElement.Empty, null, false),
            '#' => new Cell(MapElement.Obstacle, null, false),
            '^' => new Cell(MapElement.Guard, GuardDirection.North, true),
            '>' => new Cell(MapElement.Guard, GuardDirection.East, true),
            'v' => new Cell(MapElement.Guard, GuardDirection.South, true),
            '<' => new Cell(MapElement.Guard, GuardDirection.West, true),
            _ => throw new Exception($"unknown map element {c}"),
        }).ToArray())
        .ToArray();
}

static (int, int) FindGuardStartPosition(Cell[][] map)
{
    var pos = (0, 0);
    bool found = false;
    for (int y = 0; y < map.Length; y++)
    {
        var line = map[y];
        for (int x = 0; x < line.Length; x++)
        {
            var cell = line[x];
            if (cell.Element == MapElement.Guard)
            {
                pos = (x, y);
                found = true;
                break;
            }
        }
        if (found)
        {
            break;
        }
    }

    return pos;
}

static List<List<(int, int)>> DetermineGuardPath(Cell[][] map)
{
    List<List<(int, int)>> path = new();
    var pos = FindGuardStartPosition(map);
    List<(int, int)> currentEdge = new();
    while (true)
    {
        var (x, y) = pos;
        var currentCell = map[y][x];
        var offsetX = currentCell.Direction switch { GuardDirection.East => 1, GuardDirection.West => -1, _ => 0 };
        var offsetY = currentCell.Direction switch { GuardDirection.South => 1, GuardDirection.North => -1, _ => 0 };

        var newX = x + offsetX;
        var newY = y + offsetY;

        if (newX >= map[0].Length || newX < 0 || newY >= map.Length || newY < 0)
        {
            break;
        }

        var nextCell = map[newY][newX];
        if (nextCell.Element == MapElement.Obstacle)
        {
            currentCell.RotateGuard();
            path.Add(currentEdge);
            currentEdge = new();
            continue;
        }

        if (nextCell.Element == MapElement.Empty)
        {
            nextCell.GuardEnter(currentCell.Direction!.Value);
            currentCell.GuardLeave();
            pos = (newX, newY);
        }
        currentEdge.Add(pos);
    }

    return path;
}

static int CountPossibleSquares(Cell[][] map, List<List<(int, int)>> path)
{
    for (int edgeIndex = 0; edgeIndex < path.Count - 3; edgeIndex++)
    {
        var edge = path[edgeIndex];
        var edge2 = path[edgeIndex + 1];
        var edge3 = path[edgeIndex + 2];

    }
}

static bool IsParallel(List<(int, int)> a, List<(int, int)> b)
{
    var (ax, ay) = GetDirection(a);
    var (bx, by) = GetDirection(b);

    return ax == bx || bx == by;

}

static (int, int) GetDirection(List<(int, int)> edge)
{
    var (x1, y1) = edge.First();
    var (x2, y2) = edge.Last();

    return (Math.Clamp(Math.Abs(x1 - x2), 0, 1), Math.Clamp(Math.Abs(y1 - y2), 0, 1));
}

static List<(int, int)> GetIntersectingEdge(List<(int, int)> edges, int direction)
{

}

class Cell(MapElement element, GuardDirection? direction, bool visited)
{
    public MapElement Element { get; private set; } = element;
    public GuardDirection? Direction { get; private set; } = direction;
    public bool Visited { get; private set; } = visited;

    public void RotateGuard()
    {
        if (Element != MapElement.Guard)
        {
            throw new InvalidOperationException("Not a guard!");
        }

        Direction = Direction switch
        {
            GuardDirection.North => GuardDirection.East,
            GuardDirection.East => GuardDirection.South,
            GuardDirection.South => GuardDirection.West,
            GuardDirection.West => GuardDirection.North,
            _ => throw new InvalidOperationException("Unexpected direction"),
        };
    }

    public void GuardEnter(GuardDirection direction)
    {
        if (Element != MapElement.Empty)
        {
            throw new InvalidOperationException("Cell is not empty");
        }

        Element = MapElement.Guard;
        Direction = direction;
        Visited = true;
    }

    public void GuardLeave()
    {
        if (Element != MapElement.Guard)
        {
            throw new InvalidOperationException("Cell is not a guard");
        }

        Element = MapElement.Empty;
        Direction = null;
    }
}

enum GuardDirection
{
    North,
    East,
    South,
    West,
}


enum MapElement
{
    Empty,
    Guard,
    Obstacle,
}

