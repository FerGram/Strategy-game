﻿//#define DEBUG_INFO

using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

static class Algorithm
{
    static public List<Node> BFSAlgorithm(Grid grid, Node start, Node end)
    {
        List<Node> visited = new List<Node>();
        Queue<Node> nodeQueue = new Queue<Node>();

#if DEBUG_INFO
        var watch = new Stopwatch();
        UnityEngine.Debug.Log("Start node position at: " + start.GetPosition());
        UnityEngine.Debug.Log("End node position at: " + end.GetPosition());
        watch.Start();
#endif

        Node currentNode = null;
        nodeQueue.Enqueue(start);
        grid.CleanAllPreviousNodes();

        while (currentNode != end && nodeQueue.Count > 0)
        {
            currentNode = nodeQueue.Dequeue();

            if (currentNode != null)
            {
                List<Node> neighbours = currentNode.GetNeighbours();
                foreach (var neighbour in neighbours)
                {
                    if (neighbour != null && !visited.Contains(neighbour) && !nodeQueue.Contains(neighbour))
                    {
                        neighbour.SetCameFromNode(currentNode);
                        nodeQueue.Enqueue(neighbour);
                    }
                }

                visited.Add(currentNode);
            }
        }
#if DEBUG_INFO
        watch.Stop();
        UnityEngine.Debug.Log("Main loop took: " + watch.ElapsedMilliseconds + " ms");
#endif
        if (currentNode == end) return GetPath(start, currentNode);
        else
        {
            UnityEngine.Debug.LogWarning("Queue length reached 0");
            return null;
        }
    }

    static public List<Node> AStarAlgorithm(Grid grid, Node start, Node end)
    {
        bool pathSuccess = false;
        Node currentNode = start;

        if (!start.IsWallNode() && !end.IsWallNode())
        {
            Heap<Node> openSet = new Heap<Node>(grid.NodeCount());
            HashSet<Node> closedSet = new HashSet<Node>();
            
            openSet.Add(currentNode);

            while (openSet.Count > 0)
            {
                currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == end)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbour in currentNode.GetNeighbours())
                {
                    if (neighbour.IsWallNode() || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, end);
                        neighbour.SetCameFromNode(currentNode);

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }
        for (int i = 0; i < grid.Width; i++)
        {
            for (int j = 0; j < grid.Height; j++)
            {
                grid.GetNodeAt(i, j).ResetCosts();
            }
        }
        if (pathSuccess) return GetPath(start, currentNode);
        else
        {
            UnityEngine.Debug.LogWarning("Queue length reached 0");
            return null;
        }
    }

    static private List<Node> GetPath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode.GetCameFromNode() != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.GetCameFromNode();
        }
        path.Reverse();
        return path;
    }

    static int GetDistance(Node nodeA, Node nodeB)
    {
        Vector2 nodeAPos = nodeA.GetPosition();
        Vector2 nodeBPos = nodeB.GetPosition();
        int dstX = (int)Mathf.Round(Mathf.Abs(nodeAPos.x - nodeBPos.x));
        int dstY = (int)Mathf.Round(Mathf.Abs(nodeAPos.y - nodeBPos.y));

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}