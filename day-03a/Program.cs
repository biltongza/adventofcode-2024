
using System.Text.RegularExpressions;
var file = File.ReadAllText("input.txt");
var matches = Helpers.MulExpression().Matches(file);
int sum = 0;
foreach (var match in matches.Cast<Match>())
{
    var a = match.Groups[1].Value;
    var b = match.Groups[2].Value;
    sum += int.Parse(a) * int.Parse(b);
}
Console.WriteLine(sum);

static partial class Helpers
{
    [GeneratedRegex("mul\\((\\d{1,3}),(\\d{1,3})\\)")]
    public static partial Regex MulExpression();
}