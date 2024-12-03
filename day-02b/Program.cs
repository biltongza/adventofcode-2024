var reports = File.ReadAllLines("input.txt")
    .Select(line => line.Split(' ').Select(num => int.Parse(num)).ToArray());
int count = 0;
foreach (var report in reports)
{
    bool isSafe = DetermineSafety(report, true);

    if (isSafe) count++;
}

bool DetermineSafety(int[] report, bool recurse)
{
    bool isSafe = true;
    int direction = 0;
    int badIndex = 0;
    for (int i = 1; i < report.Length; i++)
    {
        var current = report[i];
        var last = report[i - 1];
        var diff = current - last;

        if (Math.Abs(diff) > 3)
        {
            isSafe = false;
            badIndex = i;
            break;
        }

        var currentDirection = diff >= 1 ? 1 : diff <= -1 ? -1 : 0;
        if (i != 1 && currentDirection != direction)
        {
            isSafe = false;
            badIndex = i;
            break;
        }
        direction = currentDirection;
    }

    if (isSafe)
    {
        return true;
    }

    if (!recurse)
    {
        return false;
    }
    for (int i = 0; i < report.Length; i++)
    {
        var newReport = CopyExceptIndex(report, i);
        if (DetermineSafety(newReport, false))
        {
            return true;
        }
    }

    return false;
}

int[] CopyExceptIndex(int[] source, int index)
{
    var newArray = new int[source.Length - 1];
    Array.Copy(source, 0, newArray, 0, index);
    Array.Copy(source, index + 1, newArray, index, source.Length - index - 1);
    return newArray;
}

Console.WriteLine(count);
