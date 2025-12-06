using System;
using System.Collections.Generic;
using Maze.Utils;

namespace Maze.Models.Abstract
{
    public abstract class TurnRuleMidget : Midget
    {
        #region Properties
        private Direction CurrentDirection { get; set; }
        private IEnumerable<Direction> PriorityDirections => GetPriorityOrder(CurrentDirection);
        #endregion

        #region Override
        protected override void PerformMove()
        {
            var possible = PossibleNextDirections(Position);

            foreach (var dir in PriorityDirections)
            {
                if (!possible.Contains(dir)) continue;

                CurrentDirection = dir;
                Position = PointAfterMove(Position, dir);
                return;
            }
        }
        #endregion

        #region Abstract
        protected abstract IEnumerable<Direction> GetPriorityOrder(Direction current);
        #endregion

        #region Protected
        protected Direction RightOf(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Up;
                default:
                    return dir;
            }
        }
        protected Direction LeftOf(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Up;
                default:
                    return dir;
            }
        }
        protected Direction BackOf(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    return dir;
            }
        }
        #endregion

        #region Constructor
        protected TurnRuleMidget(char symbol, Point position, List<Point> endPositions, List<List<char>> map, Dictionary<MapTile, char> tileSymbols, ConsoleColor color)
            : base(symbol, position, endPositions, map, tileSymbols, color)
        {
        }
        #endregion
    }
}
