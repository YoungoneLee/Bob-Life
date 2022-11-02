using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;

    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);

                StrengthGameManager.instance.NoteHit();
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
        if(other.tag == "Activator" && transform.position.y < -4.34)
        {
            //Debug.Log("Exited collider on game object: "+ other.gameObject.name);
            canBePressed = false;
            StrengthGameManager.instance.NoteMissed();
            //Instantiate(missedEffect,transform.position, missedEffect.transform.rotation);

        }
    }

}
