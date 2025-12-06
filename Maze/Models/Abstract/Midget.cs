using System;
using System.Collections.Generic;
using Maze.Utils;

namespace Maze.Models.Abstract
{
    public abstract class Midget
    {
        #region Properties
        public char Symbol { get; }
        public bool HasReachedEnd { get; private set; }
        public ConsoleColor Color { get; }
        public Point Position { get; protected set; }
        #endregion

        #region Static
        // Properties
        public static List<Point> EndPositions { get; set; }
        public static List<List<char>> Map { get; set; }
        public static Dictionary<MapTile, char> TileSymbols { get; set; }

        // Methods
        protected static Point PointAfterMove(Point point, Direction direction)
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
        private static bool IsInsideMap(Point point)
        {
            return point.X >= 0 &&
                   point.X < Map.Count &&
                   point.Y >= 0 &&
                   point.Y < Map[point.X].Count;
        }
        #endregion

        #region Constructor
        protected Midget(char symbol, Point startPosition, ConsoleColor color)
        {
            Symbol = symbol;
            Position = startPosition;
            Color = color;
        }
        #endregion

        #region Abstract
        protected abstract void PerformMove();
        #endregion

        #region Public
        public void Move()
        {
            if (HasReachedEnd) return;

            PerformMove();

            HasReachedEnd = HasReachedFinish();
        }
        #endregion

        #region Protected
        protected List<Direction> PossibleNextDirections(Point point)
        {
            var possibleMoves = new List<Direction>();

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (CanMoveToDirection(point, direction))
                    possibleMoves.Add(direction);
            }

            return possibleMoves;
        }
        #endregion

        #region Private
        private bool HasReachedFinish() => EndPositions.Contains(Position);
        private bool CanMoveToDirection(Point point, Direction direction)
        {
            var row = point.X;
            var col = point.Y;
            var pathSymbol = TileSymbols[MapTile.Path];

            switch (direction)
            {
                case Direction.Up:
                    if (!IsInsideMap(new Point(point.X - 1, point.Y))) return false;
                    return Map[row - 1][col] == pathSymbol;

                case Direction.Down:
                    if (!IsInsideMap(new Point(point.X + 1, point.Y))) return false;
                    return Map[row + 1][col] == pathSymbol;

                case Direction.Left:
                    if (!IsInsideMap(new Point(point.X, point.Y - 1))) return false;
                    return Map[row][col - 1] == pathSymbol;

                case Direction.Right:
                    if (!IsInsideMap(new Point(point.X, point.Y + 1))) return false;
                    return Map[row][col + 1] == pathSymbol;

                default:
                    return false;
            }
        }
        #endregion
    }
}
