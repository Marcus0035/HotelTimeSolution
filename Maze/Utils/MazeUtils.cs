using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Utils
{
    internal static class MazeUtils
    {
        public static void PrintMaze(List<List<char>> maze)
        {
            foreach (var line in maze)
            {
                Console.WriteLine(string.Concat(line));
            }
        }

        public static void PrepareConsoleForMaze(List<List<char>> map)
        {
            Console.WindowHeight = map.Count;
            Console.WindowWidth = map.Max(x => x.Count);
            Console.Clear();
        }

        public static List<List<char>> LoadMazeFromFile(string path)
        {
            var maze = new List<List<char>>();

            try
            {
                var lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    maze.Add(line.ToList());
                }
                return maze;
            }
            catch
            {
                throw new FileLoadException("Failed while loading map");
            }
        }
    }
}
