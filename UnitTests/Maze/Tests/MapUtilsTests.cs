using Maze.Core.Utils;
using Maze.Core.Models;
using UnitTests.Maze.BaseTest;

namespace UnitTests.Maze.Tests
{
    public class MapUtilsTests : MazeBaseTests
    {
        [Fact]
        public void LoadMapFromFile_ThrowsFileNotFoundException()
        {
            var path = GetMapPath("NoExist.dat");
            Assert.Throws<IOException>(() => MapUtils.LoadMapFromFile(path));
        }

        [Fact]
        public void LoadMapFromFile_LoadsCorrectly()
        {
            var path = GetMapPath("ValidSingleEnd.dat");

            var map = MapUtils.LoadMapFromFile(path);

            Assert.NotEmpty(map);
        }

        [Fact]
        public void GetStartPosition_ReturnsCorrectPoint()
        {
            var path = GetMapPath("ValidSingleEnd.dat");
            var map = MapUtils.LoadMapFromFile(path);

            var start = MapUtils.GetStartPosition(map, Tiles);

            Assert.Equal(new Point(0, 1), start);
        }

        [Fact]
        public void GetStartPosition_ReturnException()
        {
            var path = GetMapPath("NoStart.dat");
            var map = MapUtils.LoadMapFromFile(path);

            Assert.Throws<Exception>(() => MapUtils.GetStartPosition(map, Tiles));
        }

        [Fact]
        public void GetEndPosition_ReturnException()
        {
            var path = GetMapPath("NoEnd.dat");
            var map = MapUtils.LoadMapFromFile(path);

            Assert.Throws<Exception>(() => MapUtils.GetAllEndPositions(map, Tiles));
        }
    }
}
