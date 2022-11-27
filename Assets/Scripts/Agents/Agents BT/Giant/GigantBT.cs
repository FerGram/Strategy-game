using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

[RequireComponent(typeof(Giant))]
public class GigantBT : BehaviourTree.Tree
{
    [Header("Stats")]
    [SerializeField] float rangeOfVision = 1f;
    [SerializeField] float damage = 1f;
    [SerializeField] float attackTime = 2f;

    [Header("Targets")]
    private GameObject[] smallTowers;
    private GameObject kingTower;
    private bool smallTowerNotVisited;
    private bool kingTowerNotVisited;

    private Agent agent;
    private Animator animator;


    private void Awake()
    {
        agent = GetComponent<Agent>();
        animator = GetComponent<Animator>();
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
                new CheckIsSmallTowerNotVisited(agent, rangeOfVision, smallTowerNotVisited),
                new TaskMoveToClosestTower(agent, smallTowers, animator),
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIsTowerAlive(),
                new TaskAttackTarget(damage, attackTime, animator),
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIsKingTowerNotVisited(agent, rangeOfVision, kingTowerNotVisited),
                new TaskMoveToKingTower(agent, kingTower, animator),
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIsTowerAlive(),
                new TaskAttackTarget(damage, attackTime, animator),
            }),

        });

        return root;
    }
}
