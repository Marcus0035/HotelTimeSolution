using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Interfaces;
using Maze.Utils;

namespace Maze.Models
{
    public class RightMidget : MidgetBase
    {
        #region Fields
        private Direction currentDirection = Direction.Right;
        #endregion

        #region Protected
        public override void Move(List<List<char>> map)
        {
            var possibleMoves = PossibleNextMoves(map);

            var right = RightOf(currentDirection);
            var forward = currentDirection;
            var left = LeftOf(currentDirection);
            var back = BackOf(currentDirection);

            if (possibleMoves.Contains(right))
            {
                currentDirection = right;
            }
            else if (possibleMoves.Contains(forward))
            {
                currentDirection = forward;
            }
            else if (possibleMoves.Contains(left))
            {
                currentDirection = left;
            }
            else
            {
                currentDirection = back;
            }

            MoveToDirection(currentDirection);
        }
        #endregion

        #region Private
        private void MoveToDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Position = (Position.Item1 - 1, Position.Item2);
                    break;
                case Direction.Down:
                    Position = (Position.Item1 + 1, Position.Item2);
                    break;
                case Direction.Left:
                    Position = (Position.Item1, Position.Item2 - 1);
                    break;
                case Direction.Right:
                    Position = (Position.Item1, Position.Item2 + 1);
                    break;
            }

            _visitedPositions.Add(Position);
        }
        private Direction RightOf(Direction dir)
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
        private Direction LeftOf(Direction dir)
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
        private Direction BackOf(Direction dir)
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
        public RightMidget(char symbol, (int, int) position) : base(symbol, position) { }
        #endregion
    }
}
