using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GourmetPlayerMovement : MonoBehaviour
{

    public int statsSpeed = 10;
    public int jumpForce = 500;
    public int statsStength = 10;

    bool jumpTriggered = false;
    bool isGrounded = false;

    Rigidbody2D RB;

    //for the time
    float time;
    public Text timeTxt;

    //for hitting the blocks
    public int hit;
    public int brutusHits;
    public int bobHits;
    GameObject bob;
    GameObject brutus;
    public GameObject blocks;


    private void Awake()
    {
        time = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        hit = 13;
        RB = GetComponent<Rigidbody2D>();
        bob = GameObject.FindGameObjectWithTag("bob");
        brutus = GameObject.FindGameObjectWithTag("brutus");
        blocks = GameObject.FindGameObjectWithTag("blocks");
        Physics2D.IgnoreCollision(bob.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Debug.Log("hits:" + hit);
        //brutusHits = hit - statsStength;
        bobHits = hit - statsStength;
        Debug.Log("bobHits" + bobHits);
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        timeTxt.text = "Speed Score: " + time.ToString("F");

        if (isGrounded && !jumpTriggered) {
            RB.AddForce(transform.right * statsSpeed);
        }

        if (jumpTriggered)
        {
            RB.velocity = new Vector3(0, 0, 0);
            RB.AddForce(transform.up * jumpForce);
            Debug.Log("jumped");
            jumpTriggered = false;
        }

        if (bobHits == 0) Destroy(blocks);
    }
    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(RB.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("jumpTag"))
        {
            jumpTriggered = true;
        }

        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("on the ground");
            if (isGrounded == false)
            {
                isGrounded = true;
            }
        }

        if(collision.gameObject.CompareTag("blocks")) {
            Debug.Log("Before Bob Hits: " + bobHits);
            bobHits -= 1;
            Debug.Log("After Bob Hits: " + bobHits);
        }

        if (brutus.CompareTag("blocks"))
        {
            brutusHits -= 1;
            Debug.Log("Brutus Hits: " + brutusHits);
        }
    }
}
