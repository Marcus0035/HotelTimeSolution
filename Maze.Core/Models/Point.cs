using System;

namespace Maze.Core.Models
{
    public struct Point : IEquatable<Point>
    {
        public readonly int Column;
        public readonly int Row;

        public Point(int column, int row)
        {
            Column = column;
            Row = row;
        }

        // IEquatable ReSharper implementation
        public bool Equals(Point other)
        {
            return Column == other.Column && Row == other.Row;
        }
        public override bool Equals(object obj)
        {
            return obj is Point other && Equals(other);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (Column * 397) ^ Row;
            }
        }
    }
}
