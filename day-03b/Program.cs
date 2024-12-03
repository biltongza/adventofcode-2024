
using System.Text.RegularExpressions;
var file = File.ReadAllText("input.txt");
var matches = Helpers.MulExpression().Matches(file);
int sum = 0;
bool enabled = true;
foreach (var match in matches.Cast<Match>())
{
    var enabledString = match.Groups["enable"]?.Value;
    if (enabledString == "do()")
    {
        enabled = true;
    }
    else if (enabledString == "don't()")
    {
        enabled = false;
    }
    if (enabled)
    {
        var a = match.Groups["a"].Value;
        var b = match.Groups["b"].Value;
        sum += int.Parse(a) * int.Parse(b);
    }
}
Console.WriteLine(sum);

static partial class Helpers
{
    [GeneratedRegex(@"(?<=(?<enable>do\(\)|don't\(\)).*?)?mul\((?<a>\d{1,3}),(?<b>\d{1,3})\)")]
    public static partial Regex MulExpression();
}