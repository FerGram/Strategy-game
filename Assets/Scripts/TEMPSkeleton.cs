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
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) StartNavigation(_rb, goTowardsPosition.transform.position);
    }
}
