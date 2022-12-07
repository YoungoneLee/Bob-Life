using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGameAnimations : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    bool jump;
    bool fall;
    bool onGround;
    bool run;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        jump = GetComponent<PlayerScript>().spacePressed;
        onGround = GetComponent<PlayerScript>().isGrounded;
        run = GetComponent<PlayerScript>().mashKey;

        if(rb.velocity.y < 0)
        {
            fall = true;
        }
    }

    private void FixedUpdate()
    {
        if(onGround)
        {
            anim.SetBool("grounded", true);
            anim.SetBool("jumping", false);
        }

        if (jump)
        {
            if (onGround == true)
            {
                anim.SetBool("jumping", true);
                anim.SetBool("grounded", false);
            }
        }

        if (fall)
        {
            anim.SetBool("falling", true);
            anim.SetBool("jumping", false);
        }

        if (run)
        {
            Debug.Log("running");
            anim.SetBool("running", true);
        }
        else
        {
            //anim.SetBool("running", false);
        }
    }
}
