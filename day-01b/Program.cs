using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


var (list, dict) = File.ReadAllLines("input.txt").Select(line =>
{
    var matches = Parser.Split().Matches(line);
    var first = matches[0].Value;
    var second = matches[1].Value;
    return (first, second);
}).Aggregate((new List<string>(), new Dictionary<string, int>()), (collections, parts) =>
{
    var (list, dict) = collections;
    var (part1, part2) = parts;
    list.Add(part1);
    CollectionsMarshal.GetValueRefOrAddDefault(dict, part2, out _)++;
    return collections;
});

var sum = list.Sum(x =>
{
    int n = int.Parse(x);
    dict.TryGetValue(x, out int freq);
    return n * freq;
});

Console.WriteLine(sum);


static partial class Parser
{
    [GeneratedRegex("\\w+")]
    public static partial Regex Split();
}