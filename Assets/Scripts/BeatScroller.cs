using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    public float beatTempo;

    public bool hasStarted;

    public bool isBrutal = false;

    public int numRandomRolls = 64;

    public GameObject leftNote, upNote, downNote, rightNote;

    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60f;
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

        for(float i = 0.2f; i < numRandomRolls; i++)
        {
            if(isBrutal)
                r = Random.Range(1,11);
            else
                r = Random.Range(1, 5);

            //Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);

            switch(r)
            {
                case 1:
                    Instantiate(leftNote, new Vector3(-1.5f + transform.position.x, (float)i, 0), leftNote.transform.rotation, transform);
                    break;
                case 2:
                    Instantiate(upNote, new Vector3(-0.5f + transform.position.x, (float)i, 0), upNote.transform.rotation, transform);
                    break;
                case 3:
                    Instantiate(downNote, new Vector3(0.5f + transform.position.x, (float)i, 0), downNote.transform.rotation, transform);
                    break;
                case 4:
                    Instantiate(rightNote, new Vector3(1.5f + transform.position.x, (float)i, 0), rightNote.transform.rotation, transform);
                    break;
                case 5: //L U
                    Instantiate(leftNote, new Vector3(-1.5f + transform.position.x, (float)i, 0), leftNote.transform.rotation, transform);
                    Instantiate(upNote, new Vector3(-0.5f + transform.position.x, (float)i, 0), upNote.transform.rotation, transform);
                    break;
                case 6: //L D
                    Instantiate(leftNote, new Vector3(-1.5f + transform.position.x, (float)i, 0), leftNote.transform.rotation, transform);
                    Instantiate(downNote, new Vector3(0.5f + transform.position.x, (float)i, 0), downNote.transform.rotation, transform);
                    break;
                case 7: //L R
                    Instantiate(leftNote, new Vector3(-1.5f + transform.position.x, (float)i, 0), leftNote.transform.rotation, transform);
                    Instantiate(rightNote, new Vector3(1.5f + transform.position.x, (float)i, 0), rightNote.transform.rotation, transform);
                    break;
                case 8: //U D
                    Instantiate(upNote, new Vector3(-0.5f + transform.position.x, (float)i, 0), upNote.transform.rotation, transform);
                    Instantiate(downNote, new Vector3(0.5f + transform.position.x, (float)i, 0), downNote.transform.rotation, transform);
                    break;
                case 9: //U R
                    Instantiate(upNote, new Vector3(-0.5f + transform.position.x, (float)i, 0), upNote.transform.rotation, transform);
                    Instantiate(rightNote, new Vector3(1.5f + transform.position.x, (float)i, 0), rightNote.transform.rotation, transform);
                    break;
                case 10://D R
                    Instantiate(downNote, new Vector3(0.5f + transform.position.x, (float)i, 0), downNote.transform.rotation, transform);
                    Instantiate(rightNote, new Vector3(1.5f + transform.position.x, (float)i, 0), rightNote.transform.rotation, transform);
                    break;
                default:
                    Debug.Log("Oops, shidded and came");
                    break;
            }        
        }

        return;
    }
}
