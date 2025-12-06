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
        public Point Position { get; set; }
        protected List<Point> EndPositions { get; }
        public bool IsInFinish { get; set; }
        private Dictionary<MapTile, char> TileSymbols { get; }
        #endregion

        #region Constructor
        protected MidgetBase(char symbol, Point position, List<Point> endPositions, Dictionary<MapTile, char> tileSymbols)
        {
            Symbol = symbol;
            Position = position;
            EndPositions = endPositions;
            TileSymbols = tileSymbols;
        }
        #endregion

        #region Abstract
        public abstract void PerformMove(List<List<char>> map);
        #endregion

        #region Public
        public void Move(List<List<char>> map)
        {
            if (IsInFinish) return;

            PerformMove(map);

            if (!HasReachedFinish()) return;

            IsInFinish = true;
        }
        #endregion

        #region Protected
        protected List<Direction> PossibleNextMoves(List<List<char>> map)
        {
            var possibleMoves = new List<Direction>();

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (CanMoveToDirection(direction, map))
                    possibleMoves.Add(direction);
            }

            return possibleMoves;
        }
        #endregion

        #region Private
        private bool HasReachedFinish() => EndPositions.Contains(Position);
        private bool CanMoveToDirection(Direction direction, List<List<char>> map)
        {
            var row = Position.X;
            var col = Position.Y;
            var pathSymbol = TileSymbols[MapTile.Path];

            switch (direction)
            {
                case Direction.Up:
                    if (!IsInsideMap(row - 1, col, map)) return false;
                    return map[row - 1][col] == pathSymbol;

                case Direction.Down:
                    if (!IsInsideMap(row + 1, col, map)) return false;
                    return map[row + 1][col] == pathSymbol;

                case Direction.Left:
                    if (!IsInsideMap(row, col - 1, map)) return false;
                    return map[row][col - 1] == pathSymbol;

                case Direction.Right:
                    if (!IsInsideMap(row, col + 1, map)) return false;
                    return map[row][col + 1] == pathSymbol;

                default:
                    return false;
            }
        }
        private bool IsInsideMap(int row, int col, List<List<char>> map)
        {
            return row >= 0 &&
                   row < map.Count &&
                   col >= 0 &&
                   col < map[row].Count;
        }
        #endregion
    }
}
