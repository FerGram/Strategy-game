using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public abstract class AgentBT : BehaviourTree.Tree
{
    [Header("Common agent stats")]
    [SerializeField] protected float attackRange = 1f;
    [SerializeField] protected float attackDamage = 1f;
    [SerializeField] protected float timeBetweenAttacks = 2f;
    [Space]
    [SerializeField] bool _targetBuildings = true;
    [SerializeField] bool _targetAgents = true;

    protected Agent agent;
    protected Animator animator;
    protected LayerMask targetMask;


    private void Awake()
    {
        agent = GetComponent<Agent>();
        animator = GetComponent<Animator>();

        List<string> layersToTarget = new List<string>();

        if (_targetBuildings) layersToTarget.Add("Buildings");
        if (_targetAgents) layersToTarget.Add("Agents");

        if (layersToTarget.Count > 0) targetMask = LayerMask.GetMask(layersToTarget.ToArray());
        else targetMask = 0;
    }

    override protected abstract TreeNode SetUpTree();
}
