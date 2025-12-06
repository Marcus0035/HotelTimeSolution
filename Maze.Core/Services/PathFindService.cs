using Maze.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Core.Utils
{
    public class PathFindService
    {
        #region Properties
        private readonly MovementService _movement;
        private List<Point> _cachedPath;
        #endregion

        #region Constructor
        public PathFindService(MovementService movement)
        {
            _movement = movement;
        }
        #endregion

        #region Public
        public bool HasPathSolution(Point start, List<Point> endPositions)
        {
            return FindPathBFS(start, endPositions).Count > 0;
        }
        public void ResetCachedPath()
        {
            _cachedPath = null;
        }

        /// <summary>
        /// Breadth-First Search pathfinding algorithm.
        /// For new run use 'useCache' parameter.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="endPositions"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public List<Point> FindPathBFS(Point start, List<Point> endPositions, bool useCache = true)
        {
            if (_cachedPath != null && useCache)
                return _cachedPath;
            
            ResetCachedPath();

            var visited = new HashSet<Point>();
            var queue = new Queue<Point>();
            var parent = new Dictionary<Point, Point>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (endPositions.Contains(current))
                {
                    var bestPath = ReconstructPath(parent, start, current);
                    _cachedPath = bestPath;
                    return bestPath;
                }

                foreach (var neighbour in _movement.GetAllPossibleNextPositions(current))
                {
                    if (!visited.Add(neighbour))
                        continue;

                    parent[neighbour] = current;
                    queue.Enqueue(neighbour);
                }
            }

            return new List<Point>();
        }


        

        #endregion

        #region Private
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
        #endregion

    }
}
