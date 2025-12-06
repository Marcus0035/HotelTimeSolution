using System;
using System.Linq;
using Maze.Core.Models.Abstract;
using Maze.Core.Utils;

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
        private DateTime _executeTime { get; set; }
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
            if (_executeTime == default)
            {
                var time = DateTime.Now;
                var delay = GenerateRandomDelay();
                _executeTime = time.AddSeconds(delay);
                return;
            }

            if (_executeTime > DateTime.Now) return;

            Position = MovementService.MazeContext.EndPositions.First();
        }
        #endregion
    }
}
