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
    .Select(line => line.Split(',').Select(num => int.Parse(num)).ToList())
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
    if (a == b) // short circuit
    {
        return 0;
    }

    if (rules.TryGetValue(b, out var predecessors) && predecessors.Contains(a))
    {
        return -1;
    }

    return 1;
});

var incorrectlyOrdered = updates.Where(update =>
{
    for (int i = 0; i < update.Count - 1; i++)
    {
        var a = update[i];
        var b = update[i + 1];
        if (comparer.Compare(a, b) == 1)
        {
            return true;
        }
    }

    return false;
}).ToList();

foreach (var incorrect in incorrectlyOrdered)
{
    incorrect.Sort(comparer);
}

var middleNumbers = incorrectlyOrdered.Select(x => x[x.Count / 2]);
var sum = middleNumbers.Sum();
Console.WriteLine(sum);
