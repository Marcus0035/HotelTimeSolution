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
            List<string> lines;

            try
            {
                lines = File.ReadAllLines(path).ToList();
            }
            catch (Exception ex)
            {
                throw new IOException($"Unable to load map file from path: {path}", ex);
            }

            return lines.Select(line => line.ToList()).ToList();
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
            var end = GetEndPoint(map, tiles[MapTile.End]);

            var candidates = new List<Point>
            {
                new Point(end.Column, end.Row - 1),
                new Point(end.Column, end.Row + 1),
                new Point(end.Column - 1, end.Row),
                new Point(end.Column + 1, end.Row)
            };

            return candidates.Where(p =>
                p.Row >= 0 && p.Row < map.Count &&
                p.Column >= 0 && p.Column < map[p.Row].Count &&
                map[p.Row][p.Column] == tiles[MapTile.Path]
            ).ToList();
        }
        #endregion

        #region Private
        private static Point GetEndPoint(List<List<char>> map, char endSymbol)
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
