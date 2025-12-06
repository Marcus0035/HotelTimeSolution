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
        public override void PerformMove()
        {
            var possible = PossibleNextDirections(Position);

            foreach (var dir in priorityDirections)
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

        #region Private
      
        
        #endregion

        #region Constructor
        protected OneDirectionMidget(char symbol, Point position, List<Point> endPositions, List<List<char>> map, Dictionary<MapTile, char> tileSymbols)
            : base(symbol, position, endPositions, map, tileSymbols)
        {
        }
        #endregion
    }
}
