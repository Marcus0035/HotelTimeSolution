using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Utils;

namespace Maze.Interfaces
{
    public class MidgetBase
    {
        #region Properties
        public char Symbol { get; }
        public (int, int) Position { get; set; }
        protected List<(int, int)> _visitedPositions = new List<(int, int)>();
        #endregion

        #region Virtual
        public virtual void Move(List<List<char>> map) { }
        #endregion

        #region Constructor
        public MidgetBase(char symbol, (int, int) position)
        {
            Symbol = symbol;
            Position = position;
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
        private bool CanMoveToDirection(Direction direction, List<List<char>> map)
        {
            var row = Position.Item1;
            var col = Position.Item2;

            switch (direction)
            {
                case Direction.Up:
                    if (!IsInsideMap(row - 1, col, map)) return false;
                    return map[row - 1][col] == ' ';

                case Direction.Down:
                    if (!IsInsideMap(row + 1, col, map)) return false;
                    return map[row + 1][col] == ' ';

                case Direction.Left:
                    if (!IsInsideMap(row, col - 1, map)) return false;
                    return map[row][col - 1] == ' ';

                case Direction.Right:
                    if (!IsInsideMap(row, col + 1, map)) return false;
                    return map[row][col + 1] == ' ';

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
