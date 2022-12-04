using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGameAnimations : MonoBehaviour
{
    Animator anim;
    bool jump;
    bool onGround;
    bool run;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        jump = GetComponent<PlayerScript>().spacePressed;
        onGround = GetComponent<PlayerScript>().isGrounded;
        run = GetComponent<PlayerScript>().mashKey;
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
