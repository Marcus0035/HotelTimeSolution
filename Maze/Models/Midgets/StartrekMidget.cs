using System;
using System.Collections.Generic;
using System.Linq;
using Maze.Interfaces;
using Maze.Utils;

namespace Maze.Models
{
    public class StartrekMidget : MidgetBase
    {
        #region Const
        // Teleportation delay in milliseconds
        private const int TeleportDelayMin = 0;
        private const int TeleportDelayMax = 10;
        #endregion

        #region Constructor
        
        public StartrekMidget(char symbol, Point position, List<Point> endPositions, Dictionary<MapTile, char> tileSymbols) 
            : base(symbol, position, endPositions, tileSymbols)
        {
        }

        private DateTime _executeTime { get; set; }
        #endregion

        #region Override
        public override void PerformMove(List<List<char>> map)
        {
            if (_executeTime == default)
            {
                var time = DateTime.Now;
                _executeTime = time.AddSeconds(GenerateRandomDelay());
                return;
            }

            if (_executeTime > DateTime.Now) return;

            Position = EndPositions.First();
        }
        #endregion

        #region Private

        private int GenerateRandomDelay()
        {
            var random = new Random();
            return random.Next(TeleportDelayMin, TeleportDelayMax);
        }
        #endregion
    }
}
