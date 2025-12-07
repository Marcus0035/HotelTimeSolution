using Maze.Core.Models;
using Maze.Core.Utils;
using System.Collections.Generic;

namespace Maze.Core
{
    public class MazeContext
    {
        public List<List<char>> Map { get; }
        public Dictionary<MapTile, char> Tiles { get; }
        public Point Start { get; }
        public List<Point> EndPositions { get; }

        public MazeContext(List<List<char>> map, Dictionary<MapTile, char> tiles, Point start, List<Point> endPositions)
        {
            Map = map;
            Tiles = tiles;
            Start = start;
            EndPositions = endPositions;
        }
    }
}
