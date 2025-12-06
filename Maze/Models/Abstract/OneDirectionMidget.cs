using Maze.Interfaces;
using Maze.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Models;

namespace Maze.BaseClasses
{
    public abstract class OneDirectionMidget : MidgetBase
    {
        #region Properties
        public Direction CurrentDirection { get; set; }
        private IEnumerable<Direction> priorityDirections => GetPriorityOrder(CurrentDirection);
        #endregion

        #region Override
        public override void PerformMove(List<List<char>> map)
        {
            var possible = PossibleNextMoves(map);

            foreach (var dir in priorityDirections)
            {
                if (!possible.Contains(dir)) continue;

                CurrentDirection = dir;
                MoveToDirection(dir);
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

        #region Private
        private void MoveToDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Position = new Point(Position.X - 1, Position.Y);
                    break;
                case Direction.Down:
                    Position = new Point(Position.X + 1, Position.Y);
                    break;
                case Direction.Left:
                    Position = new Point(Position.X, Position.Y - 1);
                    break;
                case Direction.Right:
                    Position = new Point(Position.X, Position.Y + 1);
                    break;
            }
        }
        #endregion

        #region Constructor
        protected OneDirectionMidget(char symbol, Point position, List<Point> endPositions, Dictionary<MapTile, char> tileSymbols)
            : base(symbol, position, endPositions, tileSymbols)
        { }
        #endregion
    }
}
