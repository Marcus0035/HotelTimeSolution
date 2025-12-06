using Maze.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Maze.Core.Utils;
using Maze.Models.Abstract;
using Maze.Models.Midgets;

namespace Maze.Utils
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