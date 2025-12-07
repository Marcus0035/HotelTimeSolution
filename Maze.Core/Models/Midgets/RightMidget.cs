using System;
using System.Collections.Generic;
using Maze.Core.Models.Abstract;
using Maze.Core.Services;
using Maze.Core.Utils;

namespace Maze.Core.Models.Midgets
{
    public class RightMidget : TurnRuleMidget
    {
        public RightMidget(char symbol, Point startPosition, ConsoleColor color, MovementService movementService) 
            : base(symbol, startPosition, color, movementService) { }

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
