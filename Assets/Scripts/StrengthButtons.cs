using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthButtons : MonoBehaviour
{

    private SpriteRenderer sr;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keyToPress;

    public bool canBePressed = false;

    public GameObject missEffect;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            sr.sprite = pressedImage;

            if(!canBePressed)
            {
                StrengthGameManager.instance.NoteMissed();
                //Instantiate(missedEffect,transform.position, missedEffect.transform.rotation);
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }

        if(Input.GetKeyUp(keyToPress))
        {
            sr.sprite = defaultImage;

            if(canBePressed)
                canBePressed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Untagged")
        {
            canBePressed = true;
            Debug.Log("ButtonCanBePressed");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if(other.tag == "Activator" && other.transform.position.y < -4.45)
        // {
        //     canBePressed = false;
        //     Debug.Log("Button NO Press");
        // }
    }

}
