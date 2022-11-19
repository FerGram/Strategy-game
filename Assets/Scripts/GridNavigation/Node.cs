using UnityEngine;
using System.Collections.Generic;


public class Node
{
    private Vector2 _nodePosition;
    private List<Node> _neighbours;
    private Node _cameFromNode;

    private bool _isWallNode = false;

    public Node(Vector2 position, float size)
    {
        _nodePosition = position;
        _neighbours = new List<Node>();

        DetermineWallNode(size);
    }

    private void DetermineWallNode(float size)
    {
        if (Physics2D.OverlapBox(_nodePosition, new Vector2(size / 2, size /  2), 0))
        {
            _isWallNode = true;
        }
    }

    public Vector2 GetPosition() => _nodePosition;

    public List<Node> GetNeighbours() => _neighbours;

    public void SetNeighbour(Node neighbour)
    {
        _neighbours.Add(neighbour);
    }

    public Node GetCameFromNode() => _cameFromNode;
    public void SetCameFromNode(Node previous)
    {
        _cameFromNode = previous;
    }

    public bool IsWallNode() => _isWallNode;
}

