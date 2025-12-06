using System;
using System.Collections.Generic;
using System.Linq;
using Maze.Core.Models.Abstract;
using Maze.Core.Utils;

namespace Maze
{
    public static class PrintUtils
    {
        public static void PrintMapWithMidgets(List<Midget> midgets)
        {
            Console.SetCursorPosition(0, 0);

            for (var x = 0; x < MapUtils.Map.Count; x++)
            {
                for (var y = 0; y < MapUtils.Map[x].Count; y++)
                {
                    var tile = MapUtils.Map[x][y];

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
        public static void PrepareConsoleBeforeStart()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(MapUtils.Map.Count, MapUtils.Map.Max(x => x.Count));
            Console.Clear();
        }
        public static void PrepareConsoleAfterEnd()
        {
            Console.CursorVisible = true;
        }
    }
}
