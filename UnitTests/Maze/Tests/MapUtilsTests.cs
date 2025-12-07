using Maze.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Core.Models;
using UnitTests.Maze.BaseTest;

namespace UnitTests.Maze.Tests
{
    public class MapUtilsTests : MazeBaseTests
    {
        [Fact]
        public void LoadMapFromFile_LoadsCorrectly()
        {
            var path = GetMapPath("valid.dat");

            var map = MapUtils.LoadMapFromFile(path);

            Assert.NotEmpty(map);
        }

        [Fact]
        public void GetStartPosition_ReturnsCorrectPoint()
        {
            var path = GetMapPath("valid.dat");
            var map = MapUtils.LoadMapFromFile(path);

            var start = MapUtils.GetStartPosition(map, Tiles);

            Assert.Equal(new Point(1, 0), start);
        }
    }
}
