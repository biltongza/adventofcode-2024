using System.Text.RegularExpressions;

var regex = new Regex("\\d+");
var equations = File.ReadAllLines("input.txt").Select(line =>
{
    var matches = regex.Matches(line);
    var target = long.Parse(matches.First().Value);
    var terms = matches.Skip(1).Select(o => long.Parse(o.Value)).ToArray();
    return new Equation(target, terms);
});

var sum = equations.Where(x => IsValidResult(x)).Sum(x => x.Target);

Console.WriteLine(sum);


static bool IsValidResult(Equation equation)
{
    List<Func<long, long, long>> operations = [
        (a, b) => a + b,
        (a, b) => a * b,
        (a, b) => long.Parse($"{a}{b}"),
    ];
    Stack<(long target, long[] terms)> stack = new();
    stack.Push((equation.Target, equation.Terms));
    while (stack.Count > 0)
    {
        var (target, terms) = stack.Pop();
        if (terms.Length == 1)
        {
            if (terms[0] == target)
            {
                return true;
            }
            continue;
        }
        var a = terms[0];
        var b = terms[1];
        var rest = terms[2..];
        foreach (var op in operations)
        {
            stack.Push((target, [op(a, b), .. rest]));
        }
    }
    return false;
}

record Equation(long Target, long[] Terms);