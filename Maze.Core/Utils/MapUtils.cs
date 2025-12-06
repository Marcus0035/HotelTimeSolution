using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Maze.Core.Models;

namespace Maze.Core.Utils
{
    public static class MapUtils
    {
        #region Properties

        public static List<List<char>> Map { get; set; }
        public static List<Point> EndPositions { get; set; }

        public static readonly Dictionary<MapTile, char> TileSymbols = new Dictionary<MapTile, char>
        {
            { MapTile.Start, 'S' },
            { MapTile.End, 'F' },
            { MapTile.Path, ' '},
            { MapTile.Wall, '#' }
        };
        #endregion


        public static List<List<char>> LoadMapFromFile(string path)
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
        public static Point GetStartPosition()
        {
            for (var i = 0; i < Map.Count; i++)
            {
                for (var j = 0; j < Map[i].Count; j++)
                {
                    if (Map[i][j] == TileSymbols[MapTile.Start])
                    {
                        return new Point(i, j);
                    }
                }
            }

            throw new Exception("Start position not found in the map");
        }
      
        
        public static List<Point> GetAllEndPositions()
        {
            var endSquare = GetEndPosition(Map, TileSymbols[MapTile.End]);

            var candidates = new List<Point>()
            {
                new Point(endSquare.X, endSquare.Y - 1),
                new Point(endSquare.X, endSquare.Y + 1),
                new Point(endSquare.X - 1, endSquare.Y),
                new Point(endSquare.X + 1, endSquare.Y)
            };

            return candidates
                .Where(pos => pos.X >= 0 && pos.X < Map.Count &&
                              pos.Y >= 0 && pos.Y < Map[0].Count &&
                              Map[pos.X][pos.Y] == TileSymbols[MapTile.Path])
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
    }
}
