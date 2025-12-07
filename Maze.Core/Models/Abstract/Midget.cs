using System;
using Maze.Core.Services;
using Maze.Core.Utils;

namespace Maze.Core.Models.Abstract
{
    public abstract class Midget
    {
        #region Properties
        public char Symbol { get; }
        public bool HasReachedEnd { get; private set; }
        public ConsoleColor Color { get; }
        public Point Position { get; protected set; }
        protected MovementService MovementService { get; }
        #endregion

        #region Constructor
        protected Midget(char symbol, Point startPosition, ConsoleColor color, MovementService movementService)
        {
            Symbol = symbol;
            Position = startPosition;
            Color = color;
            MovementService = movementService;
        }
        #endregion

        #region Abstract
        protected abstract void PerformMove();
        #endregion

        #region Public
        public void Move()
        {
            if (HasReachedEnd) return;

            PerformMove();

            HasReachedEnd = MovementService.HasReachedFinish(Position);
        }
        #endregion
    }
}
