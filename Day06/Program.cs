using System.Diagnostics;
using System.Text.RegularExpressions;

Console.WriteLine("AoC 2015 Day06");

var input = File.ReadAllLines("input.txt");

var sw = Stopwatch.StartNew();
Console.WriteLine($"Result: {Solve1(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

sw.Restart();
Console.WriteLine($"Result: {Solve2(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve1(string[] data)
{
    var lights = new int[1000, 1000];

    var regex = new Regex(@"(\w+) (\d*,\d*) through (\d*,\d*)");
    var instr = data.Select(x => regex.Match(x)
        .Groups
        .Cast<Group>()
        .Skip(1)
        .Select(y => y.Value)
        .ToArray())
        .Select(x => new
        {
            action = x[0],
            from = x[1].Split(',').Select(int.Parse).ToArray(),
            to = x[2].Split(',').Select(int.Parse).ToArray()
        });

    foreach (var ins in instr)
    {
        for (int y = ins.from[0]; y <= ins.to[0]; y++)
        {
            for (int x = ins.from[1]; x <= ins.to[1]; x++)
            {
                if (ins.action == "toggle")
                {
                    lights[y, x] = (lights[y, x] == 0) ? 1 : 0;
                }
                else if (ins.action == "on")
                {
                    lights[y, x] = 1;
                }
                else
                {
                    lights[y, x] = 0;
                }
            }
        }
    }

    var result = 0;

    for (int y = 0; y < lights.GetLength(0); y++)
    {
        for (int x = 0; x < lights.GetLength(1); x++)
        {
            result += (lights[y, x] > 0) ? 1 : 0;
        }
    }

    return result;
}

int Solve2(string[] data)
{
    var lights = new int[1000, 1000];

    var regex = new Regex(@"(\w+) (\d*,\d*) through (\d*,\d*)");
    var instr = data.Select(x => regex.Match(x)
        .Groups
        .Cast<Group>()
        .Skip(1)
        .Select(y => y.Value)
        .ToArray())
        .Select(x => new
        {
            action = x[0],
            from = x[1].Split(',').Select(int.Parse).ToArray(),
            to = x[2].Split(',').Select(int.Parse).ToArray()
        });

    foreach (var ins in instr)
    {
        for (int y = ins.from[0]; y <= ins.to[0]; y++)
        {
            for (int x = ins.from[1]; x <= ins.to[1]; x++)
            {
                if (ins.action == "toggle")
                {
                    lights[y, x] += 2;
                }
                else if (ins.action == "on")
                {
                    lights[y, x] += 1;
                }
                else
                {
                    lights[y, x] += lights[y, x] > 0 ? -1 : 0;
                }
            }
        }
    }

    var result = 0;

    for (int y = 0; y < lights.GetLength(0); y++)
    {
        for (int x = 0; x < lights.GetLength(1); x++)
        {
            result += lights[y, x];
        }
    }

    return result;
}
