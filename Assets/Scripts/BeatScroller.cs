using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    public float beatTempo;

    public bool hasStarted;

    public NoteObject[] noteList;

    public GameObject leftNote, upNote, downNote, rightNote;

    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60f;

        generateNotes();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            /*
            if(Input.anyKeyDown)
            {
                hasStarted = true;
            }
            */
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }

    }

    //public NoteObject[] generateNotes()
    public void generateNotes()
    {
        float r = 0;

        for(int i = 0; i < 20; i++)
        {
            r = Random.Range(1,5);

            //Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);

            switch(r)
            {
                case 1:
                    Instantiate(leftNote, new Vector3(-1.5f, (float)i, 0), leftNote.transform.rotation, transform);
                    break;
                case 2:
                    Instantiate(upNote, new Vector3(-0.5f, (float)i, 0), upNote.transform.rotation, transform);
                    break;
                case 3:
                    Instantiate(downNote, new Vector3(0.5f, (float)i, 0), downNote.transform.rotation, transform);
                    break;
                case 4:
                    Instantiate(rightNote, new Vector3(1.5f, (float)i, 0), rightNote.transform.rotation, transform);
                    break;
                default:
                    Debug.Log("Oops, shidded and came");
                    break;
            }        
        }

        return;
    }
}
