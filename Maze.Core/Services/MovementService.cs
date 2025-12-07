using System;
using System.Collections.Generic;
using System.Linq;
using Maze.Core.Models;

namespace Maze.Core.Utils
{
    public class MovementService
    {
        #region Properties
        public readonly MazeContext MazeContext;
        #endregion

        #region Constructor
        public MovementService(MazeContext mazeContext)
        {
            MazeContext = mazeContext;
        }
        #endregion

        #region Public

        public bool HasReachedFinish(Point position)
        {
            return MazeContext.EndPositions.Contains(position);
        }

        public Point PointAfterMove(Point point, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Point(point.Column - 1, point.Row);
                case Direction.Down:
                    return new Point(point.Column + 1, point.Row);
                case Direction.Left:
                    return new Point(point.Column, point.Row - 1);
                case Direction.Right:
                    return new Point(point.Column, point.Row + 1);
                default:
                    return point;
            }
        }

        public List<Direction> PossibleNextDirections(Point point)
        {
            var possibleMoves = new List<Direction>();

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (CanMoveToDirection(point, direction))
                    possibleMoves.Add(direction);
            }

            return possibleMoves;
        }

        public List<Point> GetAllPossibleNextPositions(Point point)
        {
            var nextDirections = PossibleNextDirections(point);
            return nextDirections.Select(x => PointAfterMove(point, x)).ToList();
        }
        #endregion

        #region Private
        private bool CanMoveToDirection(Point point, Direction direction)
        {
            var row = point.Column;
            var col = point.Row;
            var pathSymbol = MazeContext.Tiles[MapTile.Path];

            switch (direction)
            {
                case Direction.Up:
                    if (!IsInsideMap(new Point(point.Column - 1, point.Row))) return false;
                    return MazeContext.Map[row - 1][col] == pathSymbol;

                case Direction.Down:
                    if (!IsInsideMap(new Point(point.Column + 1, point.Row))) return false;
                    return MazeContext.Map[row + 1][col] == pathSymbol;

                case Direction.Left:
                    if (!IsInsideMap(new Point(point.Column, point.Row - 1))) return false;
                    return MazeContext.Map[row][col - 1] == pathSymbol;

                case Direction.Right:
                    if (!IsInsideMap(new Point(point.Column, point.Row + 1))) return false;
                    return MazeContext.Map[row][col + 1] == pathSymbol;

                default:
                    return false;
            }
        }

        private bool IsInsideMap(Point point)
        {
            return point.Column >= 0 &&
                   point.Column < MazeContext.Map.Count &&
                   point.Row >= 0 &&
                   point.Row < MazeContext.Map[point.Column].Count;
        }
        #endregion
    }
}
