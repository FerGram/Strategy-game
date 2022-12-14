//#define DEBUG_INFO

using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] int _gridWidthResolution = 5;
    [SerializeField] int _gridHeightResolution = 5;
    [SerializeField] float _gridNodeSize = 1;
    [Space]
    [SerializeField] bool _showGrid = true;

    private Grid _grid;
    private Node _startNode;
    private Node _endNode;

    void Awake()
    {
        _grid = new Grid(_gridWidthResolution, _gridHeightResolution, _gridNodeSize, _showGrid);
    }

    public List<Node> GetPath(Rigidbody2D rb, Transform endPos)
    {
        return GetPath(rb, new Vector2(endPos.position.x, endPos.position.y));
    }
    public List<Node> GetPath(Rigidbody2D rb, Vector2 endPos)
    {
        _startNode = GetClosestNodeToPosition(rb.position);
        _endNode = GetClosestNodeToPosition(endPos);

        List<Node> path = Algorithm.AStarAlgorithm(_grid, _startNode, _endNode);

        if (path != null)
        {
            //Print path
            for (int i = 1; i < path.Count; i++)
            {
                Debug.DrawLine(path[i - 1].GetPosition(), path[i].GetPosition(), Color.green, 3);
            }
            return path;
        }
        else Debug.LogWarning("Algorithm is not valid");
        return null;
    }

    private Node GetClosestNodeToPosition(Vector2 pos)
    {
        Vector2 indexPos = pos / _gridNodeSize;
        indexPos.x = Mathf.Round(indexPos.x);
        indexPos.y = Mathf.Round(indexPos.y);

        Node closest = _grid.GetNodeAt((int)indexPos.x, (int)indexPos.y);

        Queue<Node> pending = new Queue<Node>();
        HashSet<Node> visited = new HashSet<Node>();

        pending.Enqueue(closest);

        while (closest.IsWallNode())
        {
            closest = pending.Dequeue();
            visited.Add(closest);

            List<Node> neighbors = closest.GetNeighbours();
            foreach (var neighbor in neighbors)
            {
                if (visited.Contains(neighbor)) continue;
                pending.Enqueue(neighbor);
            }
            Debug.Log("Reee");
        }
        Debug.Log(String.Format("Position of closest node to ({0}, {1}): ({2}, {3})", pos.x, pos.y, closest.GetPosition().x, closest.GetPosition().y));
        return closest;
    }
}