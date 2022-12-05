using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrategyBT : BehaviourTree.Tree
{
    public static GameManager gameManager;
    //AgenteIAenemy
    //Inputs que necesita
    //public static GameManager turn;
    //Gameobjects de spawns con pequeña variación random
    //Mazo de cartas
    //Turnos guardados  
    //Unidades jugadas enemigas y aliadas

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    protected override TreeNode SetUpTree()
    {
        TreeNode root = new Selector(new List<TreeNode>
        {
            new Sequence(new List<TreeNode>
            {
                new CheckBeingSieged(gameManager),
                new TaskCounterAtack(gameManager),
            }),
            
            new Sequence(new List<TreeNode>{
                new CheckAtacking(gameManager),
                new TaskJoinSiege(gameManager),
            }),
            new Sequence(new List<TreeNode>{
                new CheckCanWaitTurn(gameManager),
                new TaskWaitTurn(gameManager),
            }),
            new TaskAtack(gameManager),
            
        });
        ;
        return root;
    }

    
}
