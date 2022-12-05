using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathfindingStar : MonoBehaviour {
	
	PathManager requestManager;
	
	GridStar grid;

	void Awake() {
		requestManager = GetComponent<PathManager>();
		grid = GetComponent<GridStar>();
	}
	
	
	public void StartFindPath(Vector2 startPos, Vector3 targetPos) {
		StartCoroutine(FindPath(startPos, new Vector2(targetPos.x, targetPos.y)));
	}
	
	IEnumerator FindPath(Vector2 startPos, Vector2 targetPos) {

		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
		
		NodeStar startNode = grid.NodeFromWorldPoint(startPos);
		NodeStar targetNode = grid.NodeFromWorldPoint(targetPos);
		
		
		if (startNode.walkable && targetNode.walkable) {
			Heap<NodeStar> openSet = new Heap<NodeStar>(grid.MaxSize);
			HashSet<NodeStar> closedSet = new HashSet<NodeStar>();
			openSet.Add(startNode);
			
			while (openSet.Count > 0) {
				NodeStar currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);
				
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}
				
				foreach (NodeStar neighbour in grid.GetNeighbours(currentNode)) {
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}
					
					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;
						
						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);
					}
				}
			}
		}
		yield return null;
		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
		}
		requestManager.FinishedProcessingPath(waypoints,pathSuccess);
		
	}
	
	Vector3[] RetracePath(NodeStar startNode, NodeStar endNode) {
		List<NodeStar> path = new List<NodeStar>();
		NodeStar currentNode = endNode;
		
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
		
	}
	
	Vector3[] SimplifyPath(List<NodeStar> path) {
		List<Vector3> waypoints = new List<Vector3>();
		Vector3 directionOld = Vector3.zero;
		
		for (int i = 1; i < path.Count; i ++) {
			Vector3 directionNew = new Vector3(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add(path[i].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}
	
	int GetDistance(NodeStar nodeA, NodeStar nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
		
		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
	
	
}