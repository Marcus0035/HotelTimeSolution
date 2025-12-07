using System;
using System.Collections.Generic;
using Maze.Core.Models.Abstract;
using Maze.Core.Services;

namespace Maze.Core.Models.Midgets
{
    public class GuidedMidget : Midget
    {
        #region Properties
        private readonly PathFindService _pathFindService;
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
                _bestRoute = _pathFindService.FindPathBfs(Position, endPositions);
            }

            Position = _bestRoute[_bestRoute.IndexOf(Position) + 1];
        }
        #endregion
    }
}
