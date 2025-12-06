using System;
using System.Collections.Generic;
using Maze.Models.Abstract;
using Maze.Utils;

namespace Maze.Models.Midgets
{
    public class RightMidget : TurnRuleMidget
    {
        public RightMidget(char symbol, Point startPosition, ConsoleColor color) 
            : base(symbol, startPosition, color) { }

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
