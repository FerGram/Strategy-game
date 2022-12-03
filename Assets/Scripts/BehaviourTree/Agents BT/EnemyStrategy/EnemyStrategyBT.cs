using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrategyBT : BehaviourTree.Tree
{
    public static Agent agent;
    //AgenteIAenemy
    //Inputs que necesita
    //public static GameManager turn;
    //Gameobjects de spawns con pequeña variación random
    //Mazo de cartas
    //Turnos guardados  
    //Unidades jugadas enemigas y aliadas

    private void Awake()
    {
        
    }
    protected override TreeNode SetUpTree()
    {
        TreeNode root = new Selector(new List<TreeNode>
        {
            new Sequence(new List<TreeNode>
            {
                new CheckBeingSieged(agent),
                new TaskCounterAtack(agent),
            }),
            new Sequence(new List<TreeNode>{
                new CheckAtacking(agent),
                new TaskJoinSiege(agent),
            }),
            new Sequence(new List<TreeNode>{
                new CheckCanWaitTurn(agent),
                new TaskWaitTurn(agent),
            }),
            new TaskAtack(agent),
        });
        ;
        return root;
    }

    
}
