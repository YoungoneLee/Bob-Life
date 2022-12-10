using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGAnimations : MonoBehaviour
{
    public GameObject bob;
    public Animator bobAnim;


    // Start is called before the first frame update
    void Start()
    {
        bobAnim = bob.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
