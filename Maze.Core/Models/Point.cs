using System;

namespace Maze.Core.Models
{
    public struct Point : IEquatable<Point>
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // IEquatable ReSharper implementation
        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }
        public override bool Equals(object obj)
        {
            return obj is Point other && Equals(other);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }
}
