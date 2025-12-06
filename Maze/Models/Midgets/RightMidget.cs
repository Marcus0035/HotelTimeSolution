using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.BaseClasses;
using Maze.Interfaces;
using Maze.Utils;

namespace Maze.Models
{
    public class RightMidget : OneDirectionMidget
    {
        public RightMidget(char symbol, Point position, List<Point> endPositions, List<List<char>> map, Dictionary<MapTile, char> tileSymbols) : base(symbol, position, endPositions, map, tileSymbols)
        {
        }

        protected override IEnumerable<Direction> GetPriorityOrder(Direction current)
        {
            return new List<Direction>()
            {
                RightOf(current),
                current,
                LeftOf(current),
                BackOf(current),
            };
        }
    }
}
