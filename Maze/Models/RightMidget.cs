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
        public RightMidget(char symbol, (int, int) position, List<(int, int)> endPositions) : base(symbol, position, endPositions)
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
