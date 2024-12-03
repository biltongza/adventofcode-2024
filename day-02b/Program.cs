var reports = File.ReadAllLines("input.txt")
    .Select(line => line.Split(' ').Select(num => int.Parse(num)).ToList());
int count = 0;
foreach (var report in reports)
{
    bool isSafe = true;
    int direction = 0;
    bool dampen = true;
    for (int i = 1; i < report.Count(); i++)
    {
        var current = report[i];
        var last = report[i - 1];
        var diff = current - last;
        if (Math.Abs(diff) > 3)
        {
            if (dampen)
            {
                dampen = false;
                report.RemoveAt(i);
                i--;
                continue;
            }
            isSafe = false;
            break;
        }

        var currentDirection = diff >= 1 ? 1 : diff <= -1 ? -1 : 0;
        if (i != 1 && currentDirection != direction)
        {
            if (dampen)
            {
                dampen = false;
                report.RemoveAt(i);
                i--;
                continue;
            }
            isSafe = false;
            break;
        }
        direction = currentDirection;
    }

    if (isSafe) count++;
}


Console.WriteLine(count);
