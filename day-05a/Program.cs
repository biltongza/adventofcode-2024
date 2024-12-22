var file = File.ReadAllLines("input.txt").ToArray();

int stop = 0;
while (file[stop] != string.Empty)
{
    stop++;
}

var pageOrderingRuleLines = file[..stop];
var updateLines = file[(stop + 1)..];

var pageOrderingRules = pageOrderingRuleLines.Select(line =>
{
    var parts = line.Split('|');
    var first = parts[0];
    var second = parts[1];
    return (int.Parse(first), int.Parse(second));
}).ToArray();

var updates = updateLines
    .Select(line => line.Split(',').Select(num => int.Parse(num)).ToArray())
    .ToArray();

Dictionary<int, List<int>> rules = new();

foreach (var (first, second) in pageOrderingRules)
{
    List<int>? predecessors = null;
    if (!rules.TryGetValue(second, out predecessors!))
    {
        predecessors = new List<int>();
        rules[second] = predecessors;
    }

    predecessors.Add(first);
}

var comparer = Comparer<int>.Create((a, b) =>
{
    if (a == b)
    {
        return 0;
    }


    if (rules.TryGetValue(a, out var predecessors) && predecessors.Contains(b))
    {
        return -1;
    }

    return 0;
});

var correctlyOrdered = updates.Where(update =>
{
    for (int i = 0; i < update.Length - 1; i++)
    {
        var a = update[i];
        var b = update[i + 1];
        if (comparer.Compare(a, b) != 0)
        {
            return false;
        }
    }

    return true;
}).ToList();

var middleNumbers = correctlyOrdered.Select(x => x[x.Length / 2]);
var sum = middleNumbers.Sum();
Console.WriteLine(sum);
