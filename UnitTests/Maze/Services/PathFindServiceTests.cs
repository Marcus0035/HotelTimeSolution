using UnitTests.Maze.BaseTest;

namespace UnitTests.Maze.Services
{
    public class PathFindServiceTests: MazeBaseTests
    {
        private string ValidFileName = "ValidSingleEnd.dat";
        private string NoPossibleFileName = "NoPossible.dat";

        [Fact]
        public void FindPathBFS_WorksCorrectly()
        {
            var pathFindService = CreatePathFindService(ValidFileName);
            var context = CreateContext(ValidFileName);
            var path = pathFindService.FindPathBFS(context.Start, context.EndPositions, false);
            Assert.Contains(context.EndPositions[0], path);
        }
        [Fact]
        public void HasPathSolution_ReturnsTrue()
        {
            var pathFindService = CreatePathFindService(ValidFileName);
            var context = CreateContext(ValidFileName);
            var hasPath = pathFindService.HasPathSolution(context.Start, context.EndPositions);
            Assert.True(hasPath);
        }

        [Fact]
        public void HasPathSolution_ReturnsFalse_WhenNoPath()
        {
            var pathFindService = CreatePathFindService(NoPossibleFileName);
            var context = CreateContext(NoPossibleFileName);
            var hasPath = pathFindService.HasPathSolution(context.Start, context.EndPositions);
            Assert.False(hasPath);
        }
    }
}
