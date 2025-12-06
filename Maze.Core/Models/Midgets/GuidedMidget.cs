using System;
using System.Collections.Generic;
using Maze.Core.Models.Abstract;
using Maze.Core.Utils;

namespace Maze.Core.Models.Midgets
{
    public class GuidedMidget : Midget
    {
        #region Properties

        private PathFindService _pathFindService;
        private List<Point> _bestRoute;
        #endregion

        #region Constructor

        public GuidedMidget(char symbol, Point position, ConsoleColor color, MovementService movementService)
            : base(symbol, position, color, movementService)
        {
            _pathFindService = new PathFindService(movementService);
        }
        #endregion

        #region Override
        protected override void PerformMove()
        {
            if (_bestRoute == null || _bestRoute.Count == 0)
            {
                var endPositions = MovementService.MazeContext.EndPositions;
                _bestRoute = _pathFindService.FindPathBFS(Position, endPositions);
            }

            Position = _bestRoute[_bestRoute.IndexOf(Position) + 1];
        }
        #endregion

        #region Private
       
        #endregion
    }
}
