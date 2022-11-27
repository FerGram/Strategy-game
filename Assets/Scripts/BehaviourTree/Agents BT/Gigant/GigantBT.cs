using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class GigantBT : BehaviourTree.Tree
{
    //Variables globales
    //public static float speed = 2f;
    public static Agent agent;
    public static Animator animator;

    [Header("Targets")]
    public static GameObject[] smallTowers;
    public static GameObject kingTower;
    public static bool smallTowerNotVisited;
    public static bool kingTowerNotVisited;

    [Header("Stats")]
    public static float rangeOfVision = 1f;
    public static float damage = 1f;
    public static float attackTime = 2f;

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
                new CheckIsSmallTowerNotVisited(agent),
                new TaskMoveToClosestTower(agent),
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIsTowerAlive(),
                new TaskAttackTarget(agent),
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIsKingTowerNotVisited(agent),
                new TaskMoveToKingTower(agent),
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIsTowerAlive(),
                new TaskAttackTarget(agent),
            }),

        });

        return root;
    }
}
