using System.Collections.Generic;
using UnityEngine;
using Unity.Col.Gameplay.GameplayObjects;
using Unity.Col.Gameplay.Manager;

namespace Unity.Col.Utils
{
    public static class AStar
    {
        public static bool AStarSearch(TilePosition start, TilePosition goal, int searchCapacity, out List<TilePosition> pathFound)
        {
            Dictionary<TilePosition, TilePosition> cameFrom;
            Dictionary<TilePosition, double> costSoFar;
            // This static class is for astar search algorithm.
            var frontier = new PriorityQueue<TilePosition, double>(searchCapacity);
            frontier.Enqueue(start, 0);

            // Initialize
            cameFrom = new();
            costSoFar = new();
            cameFrom[start] = start;
            costSoFar[start] = 0;

            while (true)
            {
                var current = frontier.Dequeue();
                if (current == goal)
                {
                    break;
                }
                foreach (var next in TileUtils.Neighbors(current))
                {
                    double newCost = costSoFar[current] + TileUtils.Cost(current, next);
                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        double priority = newCost + TileUtils.Heuristic(next, goal);
                        frontier.Enqueue(next, -1 * priority);
                        cameFrom[next] = current;
                    }
                }
                if (frontier.Count == 0)
                {
                    //Debug.Log("No Path found");
                    pathFound = new();
                    return false;
                }
            }
            // If path is found
            List<TilePosition> path = new();
            path.Add(goal);
            TilePosition index = goal;
            while (true)
            {
                if (cameFrom[index] == start)
                {
                    break;
                }
                index = cameFrom[index];
                path.Add(index);
            }
            path.Reverse();
            pathFound = path;
            return true;
        }

        //public static List<Vector3Int> ReachSearch(Vector3Int start, List<Vector3Int> searchRange, int costCapacity)
        //{
        //    List<Vector3Int> res = new();

        //    foreach (var pos in searchRange)
        //    {

        //        AStarSearch(start, pos, 1000);
        //        if (costSoFar[pos] <= costCapacity)
        //        {
        //            res.Add(pos);
        //        }
        //    }
        //    return res;
        //}
    }
}
