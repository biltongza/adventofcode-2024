using System.Numerics;

var file = File.ReadAllText("input.txt");

char[,] pattern1 = {
    {'M', '\0', 'S'},
    {'\0', 'A', '\0'},
    {'M', '\0', 'S'}
};

char[,] pattern2 = {
    {'S', '\0', 'S'},
    {'\0', 'A', '\0'},
    {'M', '\0', 'M'}
};

char[,] pattern3 = {
    {'M', '\0', 'M'},
    {'\0', 'A', '\0'},
    {'S', '\0', 'S'}
};

char[,] pattern4 = {
    {'S', '\0', 'M'},
    {'\0', 'A', '\0'},
    {'S', '\0', 'M'}
};


List<char[,]> patterns = [pattern1, pattern2, pattern3, pattern4];
int sum = 0;
foreach (var pattern in patterns)
{
    sum += countMatches(pattern, file);
}

Console.WriteLine(sum);

int countMatches(char[,] pattern, string input)
{
    int count = 0;
    var allLines = input.Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray().AsSpan();
    var patternHeight = pattern.GetLength(0);
    var patternWidth = pattern.GetLength(1);
    char[,] comparison = new char[patternHeight, patternWidth];
    for (int y = 0; y < allLines.Length; y++)
    {
        if (y + patternHeight > allLines.Length)
        {
            break;
        }

        var activeLines = allLines.Slice(y, patternHeight);

        for (int x = 0; x < activeLines[0].Length; x++)
        {
            if (x + patternWidth > activeLines[0].Length)
            {
                break;
            }

            for (int i = 0; i < activeLines.Length; i++)
            {
                var activeLine = activeLines[i];
                for (int j = 0; j < patternWidth; j++)
                {
                    comparison[i, j] = activeLine[x + j];
                }
            }

            if (comparePattern(pattern, comparison))
            {
                count++;
            }
        }
    }

    return count;
}

bool comparePattern(char[,] pattern, char[,] test)
{
    var height = pattern.GetLength(0);
    var width = pattern.GetLength(1);
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            if (pattern[i, j] != '\0' && pattern[i, j] != test[i, j])
            {
                return false;
            }
        }
    }
    return true;
}