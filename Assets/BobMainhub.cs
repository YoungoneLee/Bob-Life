using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobMainhub : MonoBehaviour {
    Vector3 mousePos;
    public float maxMoveSpeed = 10;
    public float smoothTime = 0.3f;
    public float minDistance = 2;
    Vector2 currentVelocity;
    private Rigidbody2D rb;
 
     void Start ()
     {
         rb = gameObject.GetComponent<Rigidbody2D>();
     }
     
     void Update ()
     {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition += ((Vector2)transform.position - mousePosition).normalized * minDistance; 
        float newPosition = Mathf.SmoothDamp(transform.position.x, mousePosition.x, ref currentVelocity.x, smoothTime, maxMoveSpeed); 
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
        if(mousePosition.x > transform.position.x)
            gameObject.transform.localScale = new Vector3(1,1,1);
        else if(mousePosition.x < transform.position.x)
            gameObject.transform.localScale = new Vector3(-1,1,1);
     }
}