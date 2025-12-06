using System;
using System.Collections.Generic;
using System.Linq;
using Maze.Core.Models.Abstract;
using Maze.Core.Utils;

namespace Maze
{
    public static class PrintUtils
    {
        public static void PrintMap(List<Midget> midgets, List<List<char>> map)
        {
            Console.SetCursorPosition(0, 0);

            for (var x = 0; x < map.Count; x++)
            {
                for (var y = 0; y < map[x].Count; y++)
                {
                    var tile = map[x][y];

                    var midget = midgets.FirstOrDefault(m => m.Position.X == x && m.Position.Y == y);

                    if (midget != null)
                    {
                        tile = midget.Symbol;
                        Console.ForegroundColor = midget.Color;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(tile);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White; // reset
        }
        public static void PrepareConsoleBeforeStart(List<List<char>> map)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(map.Count, map.Max(x => x.Count));
            Console.Clear();
        }
        public static void PrepareConsoleAfterEnd()
        {
            Console.CursorVisible = true;
        }
    }
}
