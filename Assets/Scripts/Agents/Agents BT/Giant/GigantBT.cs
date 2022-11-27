using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class GigantBT : BehaviourTree.Tree
{
    //Variables globales
    //public static float speed = 2f;
    public static Agent agent;
    public static float rangeOfVision = 1f;
    public static float damage = 1f;
    public static Animator animator;

    public static GameObject[] smallTowers;
    private void Awake()
    {
        agent = GetComponent<Agent>();
        animator = GetComponent<Animator>();
        smallTowers = GameObject.FindGameObjectsWithTag("SmallTower");
    }
    protected override TreeNode SetUpTree()
    {
        TreeNode root = new Selector(new List<TreeNode>
        {
            new Sequence(new List<TreeNode>
            {
                new CheckObjectInRange(agent),
                new TaskAttackTarget(agent),
            }),
            new TaskMoveToClosestTower(agent),
        });
            ;
        return root;
    }
}
