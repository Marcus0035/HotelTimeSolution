using UnitTests.Maze.Tests;

namespace UnitTests.Maze.Services
{
    public class PathFindServiceTests: MazeBaseTests
    {
        private readonly string _validFileName = "ValidSingleEnd.dat";
        private readonly string _noPossibleFileName = "NoPossible.dat";

        [Fact]
        public void FindPathBFS_WorksCorrectly()
        {
            var pathFindService = CreatePathFindService(_validFileName);
            var context = CreateContext(_validFileName);
            var path = pathFindService.FindPathBfs(context.Start, context.EndPositions, false);
            Assert.Contains(context.EndPositions[0], path);
        }
        [Fact]
        public void HasPathSolution_ReturnsTrue()
        {
            var pathFindService = CreatePathFindService(_validFileName);
            var context = CreateContext(_validFileName);
            var hasPath = pathFindService.HasPathSolution(context.Start, context.EndPositions);
            Assert.True(hasPath);
        }

        [Fact]
        public void HasPathSolution_ReturnsFalse_WhenNoPath()
        {
            var pathFindService = CreatePathFindService(_noPossibleFileName);
            var context = CreateContext(_noPossibleFileName);
            var hasPath = pathFindService.HasPathSolution(context.Start, context.EndPositions);
            Assert.False(hasPath);
        }
    }
}
