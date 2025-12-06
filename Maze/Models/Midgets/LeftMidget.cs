using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.BaseClasses;
using Maze.Utils;

namespace Maze.Models
{
    public class LeftMidget : OneDirectionMidget
    {
        public LeftMidget(char symbol, Point position, List<Point> endPositions, List<List<char>> map, Dictionary<MapTile, char> tileSymbols) : base(symbol, position, endPositions, map, tileSymbols)
        {
        }

        protected override IEnumerable<Direction> GetPriorityOrder(Direction current)
        {
            return new List<Direction>()
            {
                LeftOf(current),
                current,
                RightOf(current),
                BackOf(current),
            };
        }
        
    }
}
