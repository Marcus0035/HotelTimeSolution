using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Interfaces;
using Maze.Utils;

namespace Maze.Models.Midgets
{
    public class GuidedMidget : MidgetBase
    {
        #region Properties
        private List<Point> _bestRoute;
        #endregion

        #region Constructor
        public GuidedMidget(char symbol, Point position, List<Point> endPositions, List<List<char>> map, Dictionary<MapTile, char> tileSymbols)
            : base(symbol, position, endPositions, map, tileSymbols)
        { }
        #endregion

        #region Override
        public override void PerformMove()
        {
            if (_bestRoute == null || _bestRoute.Count == 0)
                _bestRoute = FindPathBFS(Position, EndPositions[0]);

            Position = _bestRoute[_bestRoute.IndexOf(Position) + 1];
        }
        #endregion

        #region Private
        private List<Point> FindPathBFS(Point start, Point end)
        {
            var visited = new HashSet<Point>();
            var queue = new Queue<Point>();
            var parent = new Dictionary<Point, Point>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (EndPositions.Contains(current))
                {
                    return ReconstructPath(parent, start, end);
                }

                foreach (var neighbour in GetAllPossibleNextPositions(current))
                {
                    if (!visited.Add(neighbour))
                        continue;

                    parent[neighbour] = current;
                    queue.Enqueue(neighbour);
                }
            }

            return new List<Point>();
        }
        private List<Point> ReconstructPath(Dictionary<Point, Point> parent, Point start, Point end)
        {
            var path = new List<Point>();
            var current = end;

            while (!current.Equals(start))
            {
                path.Add(current);
                current = parent[current];
            }

            path.Add(start);
            path.Reverse();
            return path;
        }
        private List<Point> GetAllPossibleNextPositions(Point point)
        {
            var nextDirections = PossibleNextDirections(point);
            return nextDirections.Select(x => PointAfterMove(point, x)).ToList();
        }
        #endregion
    }
}
