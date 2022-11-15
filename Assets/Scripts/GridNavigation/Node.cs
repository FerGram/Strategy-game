using UnityEngine;
using System.Collections.Generic;


public class Node
{
    private Vector3 _nodePosition;
    private List<Node> _neighbours;
    private Node _cameFromNode;

    private bool _isWallNode = false;

    public Node(Vector3 position, float size)
    {
        _nodePosition = position;
        _neighbours = new List<Node>();

        DetermineWallNode(size);
    }

    private void DetermineWallNode(float size)
    {
        //Check if any collider hitting a PhysicsBox with the size of the node (and 0.1f for the 3rd dimension)
        if (Physics.CheckBox(_nodePosition, new Vector3(size / 2, 0.1f, size /  2), Quaternion.identity, -1, QueryTriggerInteraction.Ignore))
        {
            _isWallNode = true;
        }
    }

    public Vector3 GetPosition() => _nodePosition;

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

