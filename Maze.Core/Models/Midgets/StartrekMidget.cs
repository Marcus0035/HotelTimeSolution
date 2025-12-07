using System;
using System.Linq;
using Maze.Core.Models.Abstract;
using Maze.Core.Services;

namespace Maze.Core.Models.Midgets
{
    public class StartrekMidget : Midget
    {
        #region Const
        // Teleportation delay in milliseconds
        private const int TeleportDelayMin = 0;
        private const int TeleportDelayMax = 10;
        #endregion

        #region Properties
        private DateTime ExecuteTime { get; set; }
        #endregion

        #region Static
        private static int GenerateRandomDelay()
        {
            var random = new Random();
            return random.Next(TeleportDelayMin, TeleportDelayMax);
        }
        #endregion

        #region Constructor
        public StartrekMidget(char symbol, Point startPosition, ConsoleColor color, MovementService movementService) 
            : base(symbol, startPosition, color, movementService) { }
        #endregion

        #region Override
        protected override void PerformMove()
        {
            if (ExecuteTime == default)
            {
                var time = DateTime.Now;
                var delay = GenerateRandomDelay();
                ExecuteTime = time.AddSeconds(delay);
                return;
            }

            if (ExecuteTime > DateTime.Now) return;

            Position = MovementService.MazeContext.EndPositions.First();
        }
        #endregion
    }
}
