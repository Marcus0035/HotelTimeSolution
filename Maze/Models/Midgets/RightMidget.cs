using System;
using System.Collections.Generic;
using Maze.Models.Abstract;
using Maze.Utils;

namespace Maze.Models.Midgets
{
    public class RightMidget : TurnRuleMidget
    {
        public RightMidget(char symbol, Point position, List<Point> endPositions, List<List<char>> map, Dictionary<MapTile, char> tileSymbols, ConsoleColor color) 
            : base(symbol, position, endPositions, map, tileSymbols, color)
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
