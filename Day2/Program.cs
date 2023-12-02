Solution.Part1();
Solution.Part2();

class Solution
{
    public static void Part1() {
        var redMax = 12;
        var greenMax = 13;
        var blueMax = 14;

        var gameResults = InputParser.Contents();
        var sum = 0;

        foreach (GameResult game in gameResults) 
        {
            bool isPossible = true;
            foreach (Dictionary<string, int> set in game.sets) {
                if (set["red"] > redMax 
                || set["green"] > greenMax
                || set["blue"] > blueMax
                ) {
                    isPossible = false;
                }
            }

            if (isPossible) {
                sum += int.Parse(game.id);
            }
        }

        Console.WriteLine(sum);

    }
    public static void Part2() {
        var gameResults = InputParser.Contents();
        var sum = 0;

        foreach (GameResult game in gameResults) 
        {
            var minRed = 0;
            var minGreen = 0;
            var minBlue = 0;
            foreach (Dictionary<string, int> set in game.sets) {
                minRed = set["red"] > minRed ? set["red"] : minRed;
                minGreen = set["green"] > minGreen ? set["green"] : minGreen;
                minBlue = set["blue"] > minBlue ? set["blue"] : minBlue;
            }

            sum+= minRed * minGreen * minBlue;
        }

        Console.WriteLine(sum);
    }
}

class InputParser {
    public static List<GameResult> Contents()
    {
        List<GameResult> results = new();
        StreamReader reader = new StreamReader("input.txt");
        var line = reader.ReadLine();

        while (line != null)
        {
            Console.WriteLine(line);

            var setList = new List<Dictionary<string, int>>();
            string gameId = "";
            for (int i = 5; i < line.Length; i++) {
                if (line[i].Equals(':')) {
                    break;
                }

                gameId = String.Concat(gameId, line[i]);
            }

            var dict = new Dictionary<string, int>() { 
                {"red", 0},
                {"green", 0},
                {"blue", 0}
            };
            var textBuffer = new List<char>();
            string? key = null;
            string? value = null;
            for (int j = 0; j < line?.Length; j++)
            {
                var c = line[line.Length - j - 1];


                if (c.Equals(';'))
                {
                    setList.Add(dict);
                    dict = new Dictionary<string, int>() { 
                        {"red", 0},
                        {"green", 0},
                        {"blue", 0}
                    };                    continue;
                }

                if (c.Equals(':')) 
                {
                    setList.Add(dict);
                    break;
                }

                if (c.Equals(','))
                {
                    continue;
                }

                if (c.Equals(' ')) {
                    if (key == null)
                    {
                        key = String.Join("", textBuffer);
                    } 
                    else
                    {
                        value = String.Join("", textBuffer);
                        dict[key] = int.Parse(value);
                        key = null;
                        value = null;
                    }

                    textBuffer.Clear();
                    continue;
                }

                textBuffer.Insert(0, c);
            }

            var newResult = new GameResult() { id = gameId, sets = setList };
            results.Add(newResult);
            line = reader.ReadLine();
        }

        return results;
    }
}

class GameResult
{
    public string id {get; set;}
    public List<Dictionary<string, int>> sets {get; set;} = new List<Dictionary<string, int>>();
}

