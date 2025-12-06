using Maze.Interfaces;
using Maze.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        public static Point GetStartPosition(List<List<char>> map)
        {
            for (var i = 0; i < map.Count; i++)
            {
                for (var j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == StartLetter)
                    {
                        return new Point(i, j);
                    }
                }
            }

            throw new Exception("Start position not found in the map");
        }
        public static List<Point> GetAllEndPositions(List<List<char>> map)
        {
            var endSquare = GetEndPosition(map);

            var candidates = new List<Point>()
            {
                new Point(endSquare.x, endSquare.y - 1),
                new Point(endSquare.x, endSquare.y + 1),
                new Point(endSquare.x - 1, endSquare.y),
                new Point(endSquare.x + 1, endSquare.y)
            };

            return candidates
                .Where(pos => pos.x >= 0 && pos.x < map.Count &&
                              pos.y >= 0 && pos.y < map[0].Count &&
                              map[pos.x][pos.y] == ' ')
                .ToList();
        }
        public static List<List<char>> PlaceAllMidgets(List<MidgetBase> midgets, List<List<char>> map)
        {
            var tempMap = map
                .Select(row => row.ToList())
                .ToList();
            foreach (var midget in midgets)
            {
                var point = midget.Position;
                tempMap[point.x][point.y] = midget.Symbol;
            }
            return tempMap;
        }

        #endregion

        #region Private
        private static Point GetEndPosition(List<List<char>> map)
        {
            for (var i = 0; i < map.Count; i++)
            {
                for (var j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == EndLetter)
                    {
                        return new Point(i, j);
                    }
                }
            }

            throw new Exception("Start position not found in the map");
        }
        #endregion






    }
}
