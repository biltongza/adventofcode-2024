using System.Text.RegularExpressions;

var (set1, set2) = File.ReadAllLines("input.txt")
    .Select(line =>
    {
        var matches = Parser.Split().Match(line);
        var first = matches.Groups.Values.ElementAt(1).Value;
        var second = matches.Groups.Values.ElementAt(2).Value;
        return (int.Parse(first), int.Parse(second));
    })
    .Aggregate((new List<int>(), new List<int>()), (lists, parts) =>
    {
        var (set1, set2) = lists;
        var (n1, n2) = parts;
        set1.Add(n1);
        set2.Add(n2);
        return lists;
    });

set1.Sort();
set2.Sort();

var distance = set1.Zip(set2).Sum((pair) => Math.Abs(pair.First - pair.Second));
Console.WriteLine(distance);

static partial class Parser
{
    [GeneratedRegex("(\\d+)\\s+(\\d+)")]
    public static partial Regex Split();
}