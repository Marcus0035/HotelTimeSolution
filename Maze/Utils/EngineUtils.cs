using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Interfaces;

namespace Maze.Utils
{
    public static class EngineUtils
    {
        #region Const
        private static char startLetter = 'S';
        private static char endletter = 'F';
        #endregion

        #region Public
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
        public static (int, int) GetStartPosition(List<List<char>> map)
        {
            for (var i = 0; i < map.Count; i++)
            {
                for (var j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == startLetter)
                    {
                        return (i, j);
                    }
                }
            }

            throw new Exception("Start position not found in the map");
        }
        public static List<(int, int)> GetAllEndPositions(List<List<char>> map)
        {
            var endSquare = GetEndPosition(map);

            return new List<(int, int)>()
            {
                (endSquare.Item1, endSquare.Item2 - 1),
                (endSquare.Item1, endSquare.Item2 + 1),
                (endSquare.Item1 - 1, endSquare.Item2),
                (endSquare.Item1 + 1, endSquare.Item2)
            };
        }
        public static List<List<char>> PlaceMidget(MidgetBase midget, List<List<char>> map)
        {
            var tempMap = map
                .Select(row => row.ToList())
                .ToList();

            var (x, y) = midget.Position;
            tempMap[x][y] = midget.Symbol;

            return tempMap;
        }

        #endregion

        #region Private
        private static (int, int) GetEndPosition(List<List<char>> map)
        {
            for (var i = 0; i < map.Count; i++)
            {
                for (var j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == endletter)
                    {
                        return (i, j);
                    }
                }
            }

            throw new Exception("Start position not found in the map");
        }
        #endregion






    }
}
