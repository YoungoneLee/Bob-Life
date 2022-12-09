using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public int health;

    // Update is called once per frame
    void Update()
    {
    }

    public void getHit(int damage)
    {
        health -= damage;
        //Debug.Log(health);
    }
}
