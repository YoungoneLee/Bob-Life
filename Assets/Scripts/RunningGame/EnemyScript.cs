using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public Transform leftSide;
    public Transform rigthSide;
    private Transform target;
    public GameObject bombPrefab;

    public GameObject player;
    public Rigidbody2D rb;
    public GameObject barbellPrefab;

    private float moveSpeed = .001f;

    private void Awake()
    {
        //player = GameObject.Find("player");
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = leftSide;
        properInvoke();
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

        //if (player.GetComponent<PlayerScript>().score >= 10)
        //{
        //    Debug.Log("properInvoke invoked");
        //}
    }

    public void properInvoke()
    {
        Debug.Log("guh:");
        int random = UnityEngine.Random.Range(1, 7);
        Invoke("bombDropper", random);
    }

    public void stopApple()
    {
        CancelInvoke("bombDropper");
    }

    public void bombDropper()
    {
        Debug.Log("before instatiated bomb");
        Instantiate(bombPrefab, transform.position, Quaternion.identity);
        rb.gravityScale += .02f;
        properInvoke();
        moveSpeed += .001f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}