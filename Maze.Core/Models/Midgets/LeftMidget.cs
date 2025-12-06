using System;
using System.Collections.Generic;
using Maze.Core.Models.Abstract;
using Maze.Core.Utils;

namespace Maze.Core.Models.Midgets
{
    public class LeftMidget : TurnRuleMidget
    {
        public LeftMidget(char symbol, Point startPosition, ConsoleColor color) 
            : base(symbol, startPosition, color) { }

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
