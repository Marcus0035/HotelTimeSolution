using System;
using System.Collections.Generic;
using System.Linq;
using Maze.Core.Models;

namespace Maze.Core.Utils
{
    public static class MoveUtils
    {
      

        #region Public
        public static Point PointAfterMove(Point point, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Point(point.X - 1, point.Y);
                case Direction.Down:
                    return new Point(point.X + 1, point.Y);
                case Direction.Left:
                    return new Point(point.X, point.Y - 1);
                case Direction.Right:
                    return new Point(point.X, point.Y + 1);
                default:
                    return point;
            }
        }

        public static List<Direction> PossibleNextDirections(Point point)
        {
            var possibleMoves = new List<Direction>();

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (CanMoveToDirection(point, direction))
                    possibleMoves.Add(direction);
            }

            return possibleMoves;
        }

        public static List<Point> GetAllPossibleNextPositions(Point point)
        {
            var nextDirections = PossibleNextDirections(point);
            return nextDirections.Select(x => PointAfterMove(point, x)).ToList();
        }
        #endregion

        #region Private
        private static bool CanMoveToDirection(Point point, Direction direction)
        {
            var row = point.X;
            var col = point.Y;
            var pathSymbol = MapUtils.TileSymbols[MapTile.Path];

            switch (direction)
            {
                case Direction.Up:
                    if (!IsInsideMap(new Point(point.X - 1, point.Y))) return false;
                    return MapUtils.Map[row - 1][col] == pathSymbol;

                case Direction.Down:
                    if (!IsInsideMap(new Point(point.X + 1, point.Y))) return false;
                    return MapUtils.Map[row + 1][col] == pathSymbol;

                case Direction.Left:
                    if (!IsInsideMap(new Point(point.X, point.Y - 1))) return false;
                    return MapUtils.Map[row][col - 1] == pathSymbol;

                case Direction.Right:
                    if (!IsInsideMap(new Point(point.X, point.Y + 1))) return false;
                    return MapUtils.Map[row][col + 1] == pathSymbol;

                default:
                    return false;
            }
        }

        private static bool IsInsideMap(Point point)
        {
            return point.X >= 0 &&
                   point.X < MapUtils.Map.Count &&
                   point.Y >= 0 &&
                   point.Y < MapUtils.Map[point.X].Count;
        }
        #endregion
    }
}
