using Maze.Core.Models.Abstract;
using Maze.Core.Models.Midgets;
using Maze.Core.Utils;
using System;
using System.Collections.Generic;
using Maze.Core.Models;
using Maze.Core.Services;

namespace Maze.Core
{
    public static class MazeConfiguration
    {
        #region Properties
        public static MazeContext MazeContext { get; private set; }
        #endregion

        #region Public
        public static void SetUpConfiguration(string path)
        {
            var tiles = GetTileSymbols();
            var map = MapUtils.LoadMapFromFile(path);
            var start = MapUtils.GetStartPosition(map, tiles);
            var ends = MapUtils.GetAllEndPositions(map, tiles);

            MazeContext = new MazeContext(map, tiles, start, ends);

            ValidateMap(start, ends);
        }
        public static List<Midget> GetMidgets()
        {
            var start = MapUtils.GetStartPosition(MazeContext.Map, MazeContext.Tiles);
            var moveUtils = new MovementService(MazeContext);

            return new List<Midget>
            {
                new RightMidget('R', start, ConsoleColor.Yellow, moveUtils),
                new LeftMidget('L', start, ConsoleColor.Green, moveUtils),
                new StartrekMidget('s', start, ConsoleColor.Blue, moveUtils),
                new GuidedMidget('G', start, ConsoleColor.Red, moveUtils)
            };
        }
        #endregion


        #region Private
        private static void ValidateMap(Point start, List<Point> ends)
        {
            var movement = new MovementService(MazeContext);
            var pathfinder = new PathFindService(movement);

            if (!pathfinder.HasPathSolution(start, ends))
                throw new InvalidOperationException("Maze does not have a valid path from S to F.");
        }
        private static Dictionary<MapTile, char> GetTileSymbols() => new Dictionary<MapTile, char>
        {
            { MapTile.Start, 'S' },
            { MapTile.End, 'F' },
            { MapTile.Path, ' ' },
            { MapTile.Wall, '#' }
        };
        #endregion


    }

}