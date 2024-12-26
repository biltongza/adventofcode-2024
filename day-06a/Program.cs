
var map = File.ReadAllLines("input.txt")
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

var pos = (0, 0);
bool found = false;
var mapHeight = map.Length;
var mapWidth = map[0].Length;
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

while (true)
{
    var (x, y) = pos;
    var currentCell = map[y][x];
    var offsetX = currentCell.Direction switch { GuardDirection.East => 1, GuardDirection.West => -1, _ => 0 };
    var offsetY = currentCell.Direction switch { GuardDirection.South => 1, GuardDirection.North => -1, _ => 0 };

    var newX = x + offsetX;
    var newY = y + offsetY;

    if (newX >= mapWidth || newX < 0 || newY >= mapHeight || newY < 0)
    {
        break;
    }

    var nextCell = map[newY][newX];
    if (nextCell.Element == MapElement.Obstacle)
    {
        currentCell.RotateGuard();
        continue;
    }

    if (nextCell.Element == MapElement.Empty)
    {
        nextCell.Element = MapElement.Guard;
        nextCell.Direction = currentCell.Direction;
        nextCell.Visited = true;
        currentCell.Element = MapElement.Empty;
        currentCell.Direction = null;
        pos = (newX, newY);
    }
}

int count = 0;
foreach (var line in map)
{
    foreach (var cell in line)
    {
        if (cell.Visited)
        {
            count++;
        }
    }
}

Console.WriteLine(count);

class Cell(MapElement element, GuardDirection? direction, bool visited)
{
    public MapElement Element = element;
    public GuardDirection? Direction = direction;
    public bool Visited = visited;

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

