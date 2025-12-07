using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Core;
using Maze.Core.Models;
using Maze.Core.Utils;
using UnitTests.Maze.BaseTest;

namespace UnitTests.Maze.Services
{
    public class MovementServiceTests : MazeBaseTests
    {

        private string FileSystemName = "ValidSingleEnd.dat";

        [Fact]
        public void PointAfterMove_Up_WorksCorrectly()
        {
            var ctx = CreateContext(FileSystemName);
            var service = new MovementService(ctx);

            var result = service.PointAfterMove(ctx.Start, Direction.Down);

            Assert.Equal(new Point(1, 1), result);
        }

        [Fact]
        public void PossibleNextDirections_FromStart_IsCorrect()
        {
            var ctx = CreateContext(FileSystemName);
            var service = new MovementService(ctx);

            var start = ctx.Start;

            var dirs = service.PossibleNextDirections(start);

            Assert.Contains(Direction.Down, dirs);
        }

        [Fact]
        public void HasReachedFinish_True_WhenOnEnd()
        {
            var ctx = CreateContext(FileSystemName);
            var service = new MovementService(ctx);

            var finish = ctx.EndPositions[0];

            Assert.True(service.HasReachedFinish(finish));
        }
    }

}
