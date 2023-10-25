using System;
using System.Collections.Generic;

namespace Traveler
{
    public static class TravelParser
    {
        public static (int x, int y, char direction)[] Run(string input)
        {
            var result = new List<(int, int, char)>();
            var lines = input.Split("\n");

            var i = 0;
            while (true)
            {
                var mainLine = lines[i];
                if (string.IsNullOrWhiteSpace(mainLine) || mainLine.Contains("//"))
                {
                    i++;
                    continue;
                }

                if (!mainLine.StartsWith("POS="))
                {
                    throw new InvalidOperationException("Missing position!");
                }

                var temp = lines[i].Split('=');
                var p = (int.Parse(temp[1].Split(',')[0]), int.Parse(temp[1].Split(',')[1]), temp[1].Split(',')[2][0]);
                i += 1;

                while (i < lines.Length)
                {
                    var subLine = lines[i];
                    if (subLine.StartsWith("POS="))
                    {
                        break;
                    }

                    if (string.IsNullOrWhiteSpace(subLine) || subLine.Contains("//"))
                    {
                        i++;
                        continue;
                    }

                    foreach (var op in subLine)
                    {
                        if (op == 'F')
                        {
                            switch (p.Item3)
                            {
                                case 'N': p.Item2 -= 1; break;
                                case 'S': p.Item2 += 1; break;
                                case 'E': p.Item1 += 1; break;
                                case 'W': p.Item1 -= 1; break;
                            }
                        }
                        else if (op == 'B')
                        {
                            switch (p.Item3)
                            {
                                case 'N': p.Item2 += 1; break;
                                case 'S': p.Item2 -= 1; break;
                                case 'E': p.Item1 -= 1; break;
                                case 'W': p.Item1 += 1; break;
                            }
                        }
                        else if (op == 'L' || op == 'R')
                        {
                            var newRotation = Rotate(p.Item3, op);
                            p.Item3 = newRotation;
                        }
                    }

                    i++;
                }

                result.Add(p);

                if (i >= lines.Length)
                {
                    break;
                }
            }

            return result.ToArray();
        }

        private static char Rotate(char f, char r)
        {
            switch (f)
            {
                case 'N':
                    {
                        switch (r)
                        {
                            case 'F': return 'N';
                            case 'B': return 'N';
                            case 'L': return 'W';
                            case 'R': return 'E';
                        }
                        break;
                    }
                case 'S':
                    {
                        switch (r)
                        {
                            case 'F': return 'S';
                            case 'B': return 'S';
                            case 'L': return 'E';
                            case 'R': return 'W';
                        }
                        break;
                    }
                case 'E':
                    {
                        switch (r)
                        {
                            case 'F': return 'E';
                            case 'B': return 'E';
                            case 'L': return 'N';
                            case 'R': return 'S';
                        }
                        break;
                    }
                case 'W':
                    {
                        switch (r)
                        {
                            case 'F': return 'W';
                            case 'B': return 'W';
                            case 'L': return 'S';
                            case 'R': return 'N';
                        }
                        break;
                    }
            }

            throw new InvalidOperationException("Could not rotate!");
        }
    }
}
