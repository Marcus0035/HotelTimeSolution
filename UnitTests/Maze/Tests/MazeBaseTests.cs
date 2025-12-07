using Maze.Core;
using Maze.Core.Services;
using Maze.Core.Utils;

namespace UnitTests.Maze.Tests
{
    public class MazeBaseTests
    {
        protected static MazeContext MazeContext;

        protected readonly Dictionary<MapTile, char> Tiles = new()
        {
            { MapTile.Start, 'S' },
            { MapTile.End, 'F' },
            { MapTile.Path, ' ' },
            { MapTile.Wall, '#' }
        };

        protected string GetMapPath(string fileName)
        {
            var baseDir = Directory.GetCurrentDirectory();

            var projectDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\"));

            return Path.Combine(projectDir, "Maze", "TestMaps", fileName);
        }

        protected PathFindService CreatePathFindService(string fileName)
        {
            var context = CreateContext(fileName);
            var movementService = new MovementService(context);
            return new PathFindService(movementService);
        }
        protected MazeContext CreateContext(string fileName)
        {
            var path = GetMapPath(fileName);
            var map = MapUtils.LoadMapFromFile(path);

            var start = MapUtils.GetStartPosition(map, Tiles);
            var ends = MapUtils.GetAllEndPositions(map, Tiles);

            return new MazeContext(map, Tiles, start, ends);
        }



    }
}
