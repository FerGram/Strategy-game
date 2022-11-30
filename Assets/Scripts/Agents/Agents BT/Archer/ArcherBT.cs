using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class ArcherBT : AgentBT
{
    
    protected override TreeNode SetUpTree()
    {
        TreeNode root = new Selector(new List<TreeNode>
        {
            new Sequence(new List<TreeNode>
            {
                new CheckEntityInRange(agent, attackRange, targetMask),
                new TaskAttackEntity(agent, attackDamage, timeBetweenAttacks, animator),
            }),
            new TaskMoveToClosestEntity(agent, targetMask)
        });

        return root;
    }
}
