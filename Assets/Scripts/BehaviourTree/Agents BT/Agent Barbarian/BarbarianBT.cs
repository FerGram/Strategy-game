using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

[RequireComponent(typeof(Agent))]
public class BarbarianBT : AgentBT
{
    protected override TreeNode SetUpTree()
    {
        TreeNode root = new Selector(new List<TreeNode>
        {
            //Primero voy a por los agentes, al más cercano. No quedan agentes? Entonces voy a por edificios
            
            new Sequence(new List<TreeNode>
            {
                new CheckIsTargetLayerEmpty(agent, "Agents"),
                new CheckEntityInRange(agent, attackRange, LayerMask.GetMask("Agents")),
                new TaskAttackEntity(agent, attackDamage, timeBetweenAttacks, animator),
            }),
            new TaskMoveToClosestEntity(agent, LayerMask.GetMask("Agents")),

            new Sequence(new List<TreeNode>
            {
                new CheckIsTargetLayerEmpty(agent, "Buildings"),
                new CheckEntityInRange(agent, attackRange, LayerMask.GetMask("Buildings")),
                new TaskAttackEntity(agent, attackDamage, timeBetweenAttacks, animator),
            }),
            new TaskMoveToClosestEntity(agent, LayerMask.GetMask("Buildings"))
            
        });

        return root;
    }
}
