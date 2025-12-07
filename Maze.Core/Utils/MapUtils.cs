using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Maze.Core.Models;

namespace Maze.Core.Utils
{
    public static class MapUtils
    {
        #region Public
        public static List<List<char>> LoadMapFromFile(string path)
        {
            var maze = new List<List<char>>();

            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
                maze.Add(line.ToList());

            return maze;
        }

        public static Point GetStartPosition(List<List<char>> map, Dictionary<MapTile, char> tiles)
        {
            for (var i = 0; i < map.Count; i++)
                for (var j = 0; j < map[i].Count; j++)
                    if (map[i][j] == tiles[MapTile.Start])
                        return new Point(i, j);

            throw new Exception("Start position not found");
        }

        public static List<Point> GetAllEndPositions(List<List<char>> map, Dictionary<MapTile, char> tiles)
        {
            var end = GetEndPosition(map, tiles[MapTile.End]);

            var candidates = new List<Point>
            {
                new Point(end.Column, end.Row - 1),
                new Point(end.Column, end.Row + 1),
                new Point(end.Column - 1, end.Row),
                new Point(end.Column + 1, end.Row)
            };

            return candidates.Where(p =>
                p.Column >= 0 && p.Column < map.Count &&
                p.Row >= 0 && p.Row < map[p.Column].Count &&
                map[p.Column][p.Row] == tiles[MapTile.Path]
            ).ToList();
        }
        #endregion

        #region Private
        private static Point GetEndPosition(List<List<char>> map, char endSymbol)
        {
            for (var i = 0; i < map.Count; i++)
                for (var j = 0; j < map[i].Count; j++)
                    if (map[i][j] == endSymbol)
                        return new Point(i, j);

            throw new Exception("End symbol not found");
        }
        #endregion
    }


}
