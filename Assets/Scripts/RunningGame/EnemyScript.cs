using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform leftSide;
    public Transform rigthSide;
    private Transform target;
    public GameObject droppingPrefab;

    public GameObject player;
    public Rigidbody2D rb;
    public GameObject barbellPrefab;

    private float moveSpeed = .001f;

    private void Awake()
    {
        player = GameObject.Find("player");
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = leftSide;
    }

    void Update()
    {
        if (moveSpeed >= .01f)
        {
            moveSpeed = .01f;
        }
        else
        {
            moveSpeed += .001f;
        }
        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed);

        if (Mathf.Abs(transform.position.x - target.position.x) < 1f)
        {
            if (target == rigthSide) target = leftSide;
            else target = rigthSide;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}