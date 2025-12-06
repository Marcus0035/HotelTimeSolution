using System;
using System.Collections.Generic;
using Maze.Core.Models.Abstract;
using Maze.Core.Models.Midgets;
using Maze.Core.Utils;

namespace Maze.Core
{
    public static class MazeConfiguration
    {
        #region Public
        public static void SetUpConfiguration(string path)
        {
            MapUtils.Map = MapUtils.LoadMapFromFile(path);
            var endPositions = MapUtils.GetAllEndPositions();
            MapUtils.EndPositions = endPositions;
        }

        public static List<Midget> GetMidgets()
        {
            var startPosition = MapUtils.GetStartPosition();

            return new List<Midget>
            {
                new RightMidget('R', startPosition, ConsoleColor.Yellow),
                new LeftMidget('L', startPosition, ConsoleColor.Green),
                new StartrekMidget('s', startPosition, ConsoleColor.Blue),
                new GuidedMidget('G', startPosition, ConsoleColor.DarkMagenta)
            };
        }
        #endregion
    }
}