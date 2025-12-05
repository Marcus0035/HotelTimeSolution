using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Interfaces;

namespace Maze.Models
{
    public class StartrekMidget : MidgetBase
    {
        #region Const
        // Teleportation delay in milliseconds
        private const int TeleportDelayMin = 0;
        private const int TeleportDelayMax = 10000;
        #endregion

        #region Constructor
        public StartrekMidget(char symbol, (int, int) position, List<(int, int)> endPositions) 
            : base(symbol, position, endPositions) { }
        #endregion

        #region Properties
        private bool WaitingForTeleportation { get; set; }
        #endregion

        #region Override
        public override void PerformMove(List<List<char>> map)
        {
            // If already waiting for teleportation, do nothing
            if (WaitingForTeleportation) return;

            WaitingForTeleportation = true;

            WaitBeforeTeleportation();

            Position = EndPositions.First();

            WaitingForTeleportation = false;
        }
        #endregion

        #region Private
        private void WaitBeforeTeleportation()
        {
            var random = new Random();
            var delay = random.Next(TeleportDelayMin, TeleportDelayMax);
            Task.Delay(delay).Wait();
        }
        #endregion
    }
}
