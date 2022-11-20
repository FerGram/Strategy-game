using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPSkeleton : Agent
{
    [SerializeField] GameObject goTowardsPosition;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void OnEnable()
    {
        //StartNavigation(_rb, goTowardsPosition.transform.position);
        StartNavigation(_rb, new Vector3(3.25f, -1.77f, -1.14f));
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) StartNavigation(_rb, goTowardsPosition.transform.position);
    }
}
