using System;
using System.Collections.Generic;
using System.Linq;
using Maze.Models.Abstract;
using Maze.Utils;

namespace Maze.Models.Midgets
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

        #region Constructor
        public StartrekMidget(char symbol, Point position, List<Point> endPositions, List<List<char>> map, Dictionary<MapTile, char> tileSymbols, ConsoleColor color) 
            : base(symbol, position, endPositions, map, tileSymbols, color)
        {
        }
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
