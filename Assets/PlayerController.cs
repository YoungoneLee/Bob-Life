using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb2d;
    public int runSpeed;
    public int jumpForce;
    private int jumpCount = 0;
    private bool spacePressed = false;
    private bool canJump = true;


    //alt stuff
    bool mashKey = false;
    bool keyAlternate = false;
    private int speed = 100;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //auto move
        //transform.position = Vector3.right * runSpeed * Time.deltaTime + transform.position;

        if (spacePressed)
        {
            Debug.Log("jumpin");
            rb2d.AddForce(Vector2.up * jumpForce);
            jumpCount += 1;
            spacePressed = false;
        }

        if (mashKey)
        {
            rb2d.AddForce(transform.right * speed);
            mashKey = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            jumpCount = 0;
            canJump = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canJump) spacePressed = true;
        if (jumpCount == 2) canJump = false;

        //alt buttom mashing
        if (Input.GetKeyDown(KeyCode.RightArrow) && keyAlternate == false) keyAlternate = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && keyAlternate == true)
        {
            keyAlternate = false;
            mashKey = true;
        }

    }
}
