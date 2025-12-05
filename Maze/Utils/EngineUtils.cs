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

        private const char StartLetter = 'S';
        private const char EndLetter = 'F';

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
                    if (map[i][j] == StartLetter)
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

            var candidates = new List<(int, int)>()
            {
                (endSquare.Item1, endSquare.Item2 - 1),
                (endSquare.Item1, endSquare.Item2 + 1),
                (endSquare.Item1 - 1, endSquare.Item2),
                (endSquare.Item1 + 1, endSquare.Item2)
            };

            return candidates
                .Where(pos => pos.Item1 >= 0 && pos.Item1 < map.Count &&
                              pos.Item2 >= 0 && pos.Item2 < map[0].Count &&
                              map[pos.Item1][pos.Item2] == ' ')
                .ToList();
        }
        public static List<List<char>> PlaceAllMidgets(List<MidgetBase> midgets, List<List<char>> map)
        {
            var tempMap = map
                .Select(row => row.ToList())
                .ToList();
            foreach (var midget in midgets)
            {
                var (x, y) = midget.Position;
                tempMap[x][y] = midget.Symbol;
            }
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
                    if (map[i][j] == EndLetter)
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
