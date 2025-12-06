using System;
using System.Collections.Generic;
using Maze.Core.Models.Abstract;
using Maze.Core.Models.Midgets;
using Maze.Core.Utils;

namespace Maze.Core
{
    public static class MazeConfiguration
    {
        public static MazeContext MazeContext { get; private set; }

        public static void SetUpConfiguration(string path)
        {
            var tiles = GetTileSymbols();
            var map = MapUtils.LoadMapFromFile(path);
            var start = MapUtils.GetStartPosition(map, tiles);
            var ends = MapUtils.GetAllEndPositions(map, tiles);

            MazeContext = new MazeContext(map, tiles, start, ends);
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
                new GuidedMidget('G', start, ConsoleColor.DarkMagenta, moveUtils)
            };
        }

        private static Dictionary<MapTile, char> GetTileSymbols() => new Dictionary<MapTile, char>
        {
            { MapTile.Start, 'S' },
            { MapTile.End, 'F' },
            { MapTile.Path, ' ' },
            { MapTile.Wall, '#' }
        };
    }

}