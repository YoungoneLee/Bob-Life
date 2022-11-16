using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthEffectObject : MonoBehaviour
{

    public float lifetime = 1;

    // Start is called before the first frame update
    void Start()
    {
        lifetime = Time.time + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifetime <= Time.time)
        {
            Destroy(gameObject);
        }
        
    }
}
