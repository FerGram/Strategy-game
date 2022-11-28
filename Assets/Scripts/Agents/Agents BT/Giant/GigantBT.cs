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
    [SerializeField] LayerMask objectsLayerMaskSmallTowersPlayer;
    [SerializeField] LayerMask objectsLayerMaskKingTowerPlayer;
    [SerializeField] LayerMask objectsLayerMaskSmallTowersCPU;
    [SerializeField] LayerMask objectsLayerMaskKingTowerCPU;
    [SerializeField] Rol rol = Rol.Player;

    [Header("Targets")]
    private GameObject[] smallTowers;
    private GameObject kingTower;
    private bool smallTowerNotVisited;
    private bool kingTowerNotVisited;
    private LayerMask layerMaskSmallTowers;
    private LayerMask layerMaskKingTower;

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
            layerMaskKingTower = objectsLayerMaskKingTowerCPU;
            layerMaskSmallTowers = objectsLayerMaskSmallTowersCPU;
        }
        else
        {
            //Torres contra las que compite la IA/CPU
            smallTowers = GameObject.FindGameObjectsWithTag("SmallTowerPlayerSide");
            kingTower = GameObject.FindGameObjectWithTag("KingTowerPlayerSide");
            layerMaskKingTower = objectsLayerMaskKingTowerPlayer;
            layerMaskSmallTowers = objectsLayerMaskSmallTowersPlayer;
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
                new CheckIsSmallTowerNotVisited(agent, rangeOfVision, smallTowerNotVisited, layerMaskSmallTowers),
                new TaskMoveToClosestTower(agent, smallTowers, animator),
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIsTowerAlive(),
                new TaskAttackTarget(damage, attackTime, animator),
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIsKingTowerNotVisited(agent, rangeOfVision, kingTowerNotVisited, layerMaskKingTower),
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, rangeOfVision); //gizmo rango de vision
    }
}



public enum Rol
{
    Player,
    CPU
}
