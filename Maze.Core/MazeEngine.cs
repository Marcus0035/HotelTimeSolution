using Maze.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Maze.Models.Abstract;
using Maze.Models.Midgets;

namespace Maze.Utils
{
    public static class MazeEngine
    {
        #region Public
        public static List<Midget> SetUpMidgetConfiguration(List<List<char>> map, Dictionary<MapTile, char> tileSymbols)
        {
            var startPosition = GetStartPosition(map, tileSymbols[MapTile.Start]);
            var endPositions = GetAllEndPositions(map, tileSymbols[MapTile.End], tileSymbols[MapTile.Path]);

            Midget.EndPositions = endPositions;
            Midget.Map = map;
            Midget.TileSymbols = tileSymbols;

            return new List<Midget>
            {
                new RightMidget('R', startPosition, ConsoleColor.Yellow),
                new LeftMidget('L', startPosition, ConsoleColor.Green),
                new StartrekMidget('s', startPosition, ConsoleColor.Blue),
                new GuidedMidget('G', startPosition, ConsoleColor.DarkMagenta)
            };
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
        public static readonly Dictionary<MapTile, char> TileSymbols = new Dictionary<MapTile, char>
        {
            { MapTile.Start, 'S' },
            { MapTile.End, 'F' },
            { MapTile.Path, ' '},
            { MapTile.Wall, '#' }
        };
        #endregion

        #region Private
        private static Point GetStartPosition(List<List<char>> map, char startLetter)
        {
            for (var i = 0; i < map.Count; i++)
            {
                for (var j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == startLetter)
                    {
                        return new Point(i, j);
                    }
                }
            }

            throw new Exception("Start position not found in the map");
        }
        private static List<Point> GetAllEndPositions(List<List<char>> map, char endLetter, char pathLetter)
        {
            var endSquare = GetEndPosition(map, endLetter);

            var candidates = new List<Point>()
            {
                new Point(endSquare.X, endSquare.Y - 1),
                new Point(endSquare.X, endSquare.Y + 1),
                new Point(endSquare.X - 1, endSquare.Y),
                new Point(endSquare.X + 1, endSquare.Y)
            };

            return candidates
                .Where(pos => pos.X >= 0 && pos.X < map.Count &&
                              pos.Y >= 0 && pos.Y < map[0].Count &&
                              map[pos.X][pos.Y] == pathLetter)
                .ToList();
        }
        private static Point GetEndPosition(List<List<char>> map, char endLetter)
        {
            for (var i = 0; i < map.Count; i++)
            {
                for (var j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == endLetter)
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