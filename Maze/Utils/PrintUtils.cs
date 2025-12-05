using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Utils
{
    public static class PrintUtils
    {
        public static void PrintMap(List<List<char>> map)
        {
            Console.Clear();
            foreach (var line in map)
            {
                Console.WriteLine(string.Concat(line));
            }
        }

        public static void PrepareConsoleForMaze(List<List<char>> map)
        {
            Console.SetWindowSize(map.Count, map.Max(x => x.Count));
            Console.Clear();
        }
    }
}
