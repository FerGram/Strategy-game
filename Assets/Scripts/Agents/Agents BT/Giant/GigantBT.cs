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
    [SerializeField] Rol rol = Rol.Player;

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

        if(rol == Rol.Player)
        {
            //Torres contra las que compite el jugador
            smallTowers = GameObject.FindGameObjectsWithTag("SmallTowerCPUSide");
            kingTower = GameObject.FindGameObjectWithTag("KingTowerCPUSide");
        }
        else
        {
            //Torres contra las que compite la IA/CPU
            smallTowers = GameObject.FindGameObjectsWithTag("SmallTowerPlayerSide");
            kingTower = GameObject.FindGameObjectWithTag("KingTowerPlayerSide");
        }

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
                new CheckIsKingTowerNotVisited(agent, rangeOfVision, kingTowerNotVisited, kingTower),
                new TaskMoveToTarget(agent, kingTower, animator),
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

public enum Rol
{
    Player,
    CPU
}
