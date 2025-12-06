using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Models;
using Maze.Utils;

namespace Maze.Interfaces
{
    public abstract class MidgetBase
    {
        #region Properties
        public char Symbol { get; }
        public Point Position { get; protected set; }
        public bool IsInFinish { get; private set; }
        protected List<List<char>> Map { get; }
        protected List<Point> EndPositions { get; }
        private Dictionary<MapTile, char> TileSymbols { get; }
        #endregion

        #region Constructor
        protected MidgetBase(char symbol, Point position, List<Point> endPositions, List<List<char>> map, Dictionary<MapTile, char> tileSymbols)
        {
            Symbol = symbol;
            Position = position;
            EndPositions = endPositions;
            Map = map;
            TileSymbols = tileSymbols;
        }
        #endregion

        #region Abstract
        public abstract void PerformMove();
        #endregion

        #region Public
        public void Move()
        {
            if (IsInFinish) return;

            PerformMove();

            if (!HasReachedFinish()) return;

            IsInFinish = true;
        }
        #endregion

        #region Protected
        protected List<Direction> PossibleNextMoves()
        {
            var possibleMoves = new List<Direction>();

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (CanMoveToDirection(direction))
                    possibleMoves.Add(direction);
            }

            return possibleMoves;
        }
        #endregion

        #region Private
        private bool HasReachedFinish() => EndPositions.Contains(Position);
        private bool CanMoveToDirection(Direction direction)
        {
            var row = Position.X;
            var col = Position.Y;
            var pathSymbol = TileSymbols[MapTile.Path];

            switch (direction)
            {
                case Direction.Up:
                    if (!IsInsideMap(row - 1, col)) return false;
                    return Map[row - 1][col] == pathSymbol;

                case Direction.Down:
                    if (!IsInsideMap(row + 1, col)) return false;
                    return Map[row + 1][col] == pathSymbol;

                case Direction.Left:
                    if (!IsInsideMap(row, col - 1)) return false;
                    return Map[row][col - 1] == pathSymbol;

                case Direction.Right:
                    if (!IsInsideMap(row, col + 1)) return false;
                    return Map[row][col + 1] == pathSymbol;

                default:
                    return false;
            }
        }
        private bool IsInsideMap(int row, int col)
        {
            return row >= 0 &&
                   row < Map.Count &&
                   col >= 0 &&
                   col < Map[row].Count;
        }
        #endregion
    }
}
