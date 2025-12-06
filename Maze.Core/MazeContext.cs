using Maze.Core.Models;
using Maze.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Core
{
    public class MazeContext
    {
        public List<List<char>> Map { get; }
        public Dictionary<MapTile, char> Tiles { get; set; }
        public Point Start { get; set; }
        public List<Point> EndPositions { get; set; }

        public MazeContext(List<List<char>> map, Dictionary<MapTile, char> tiles, Point start, List<Point> endPositions)
        {
            Map = map;
            Tiles = tiles;
            Start = start;
            EndPositions = endPositions;
        }
    }
}
