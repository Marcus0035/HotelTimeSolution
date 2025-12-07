using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Core.Utils;

namespace UnitTests.Maze.BaseTest
{
    public class MazeBaseTests
    {
        protected readonly Dictionary<MapTile, char> Tiles = new()
        {
            { MapTile.Start, 'S' },
            { MapTile.End, 'E' },
            { MapTile.Path, '.' },
            { MapTile.Wall, '#' }
        };

        protected string GetMapPath(string fileName)
        {
            var baseDir = Directory.GetCurrentDirectory();

            var projectDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\"));

            return Path.Combine(projectDir, "Maze", "TestMaps", fileName);
        }
    }
}
