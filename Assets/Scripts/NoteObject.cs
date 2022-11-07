using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;

    public KeyCode keyToPress;

    float distFromCenter;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(canBePressed)
        // {
        //     Debug.Log("Diff: " + Mathf.Abs(StrengthGameManager.instance.StrengthButton.transform.position.y - transform.position.y));
        // }


        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);

                // Debug.Log("Ypos: " + transform.position.y);
                // Debug.Log("ButtonYPos: " + StrengthGameManager.instance.StrengthButton.transform.position.y);

                //StrengthGameManager.instance.NoteHit();

                distFromCenter = Mathf.Abs(StrengthGameManager.instance.StrengthButton.transform.position.y - transform.position.y);

                //Debug.Log("Distance: " + distFromCenter);

                if(distFromCenter < 0.05)
                {
                    Debug.Log("Perfect");
                    StrengthGameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                else if(distFromCenter < 0.20)
                {
                    Debug.Log("Good");
                    StrengthGameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);

                }
                else if(distFromCenter < 0.45)
                {
                    Debug.Log("Normal");
                    StrengthGameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);

                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = false;

            StrengthGameManager.instance.NoteMissed();
        }
    }
    */

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator" && transform.position.y < -4.45)
        {
            //Debug.Log("Exited collider on game object: "+ other.gameObject.name);
            //Debug.Log("MissedYpos: " + transform.position.y);
            canBePressed = false;
            StrengthGameManager.instance.NoteMissed();
            //Instantiate(missedEffect,transform.position, missedEffect.transform.rotation);
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);

        }
    }

}
