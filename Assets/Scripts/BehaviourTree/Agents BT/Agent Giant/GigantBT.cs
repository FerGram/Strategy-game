using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

[RequireComponent(typeof(Agent))]
public class GigantBT : AgentBT
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
