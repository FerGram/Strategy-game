using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class ArcherBT : BehaviourTree.Tree
{

    protected override TreeNode SetUpTree()
    {
        TreeNode root = new Selector(new List<TreeNode>
        {
            //new Sequence(new List<TreeNode>
            //{
            //    new CheckIsSmallTowerNotVisited(agent, rangeOfVision, smallTowerNotVisited),
            //    new TaskMoveToClosestTower(agent, smallTowers, animator),
            //}),
            //new Sequence(new List<TreeNode>
            //{
            //    new CheckIsTowerAlive(),
            //    new TaskAttackTarget(damage, attackTime, animator),
            //}),
            //new Sequence(new List<TreeNode>
            //{
            //    new CheckIsKingTowerNotVisited(agent, rangeOfVision, kingTowerNotVisited),
            //    new TaskMoveToKingTower(agent, kingTower, animator),
            //}),
            //new Sequence(new List<TreeNode>
            //{
            //    new CheckIsTowerAlive(),
            //    new TaskAttackTarget(damage, attackTime, animator),
            //})
        });

        return root;
    }
}
