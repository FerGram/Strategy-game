using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

[RequireComponent(typeof(Agent))]
public class GigantBT : AgentBT
{
    //Targets
    private GameObject[] smallTowers;
    private GameObject kingTower;
    private bool smallTowerNotVisited;
    private bool kingTowerNotVisited;

    private void OnEnable()
    {
        smallTowers = GameObject.FindGameObjectsWithTag("SmallTower");
        kingTower = GameObject.FindGameObjectWithTag("KingTower");
        smallTowerNotVisited = true;
        kingTowerNotVisited = true;
    }
    protected override TreeNode SetUpTree()
    {
        TreeNode root = new Selector(new List<TreeNode>
        {
            new Sequence(new List<TreeNode>
            {
                new CheckIsSmallTowerNotVisited(agent, attackRange, smallTowerNotVisited),
                new TaskMoveToClosestTower(agent, smallTowers, animator),
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIsTowerAlive(),
                new TaskAttackTarget(attackDamage, timeBetweenAttacks, animator),
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIsKingTowerNotVisited(agent, attackRange, kingTowerNotVisited),
                new TaskMoveToKingTower(agent, kingTower, animator),
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIsTowerAlive(),
                new TaskAttackTarget(attackDamage, timeBetweenAttacks, animator),
            }),

        });

        return root;
    }
}
