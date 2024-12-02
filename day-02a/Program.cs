var reports = File.ReadAllLines("input.txt")
    .Select(line => line.Split(' ').Select(num => int.Parse(num)).ToArray());
int count = 0;
foreach (var report in reports)
{
    bool isSafe = true;
    int direction = 0;
    for (int i = 1; i < report.Length; i++)
    {
        var current = report[i];
        var last = report[i - 1];
        var diff = current - last;
        if (Math.Abs(diff) > 3)
        {
            isSafe = false;
            break;
        }

        var currentDirection = diff >= 1 ? 1 : diff <= -1 ? -1 : 0;
        if (i != 1 && currentDirection != direction)
        {
            isSafe = false;
            break;
        }
        direction = currentDirection;
    }

    if (isSafe) count++;
}
Console.WriteLine(count);
