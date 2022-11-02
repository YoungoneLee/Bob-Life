using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float JumpForce;
    float score;

    [SerializeField]
    bool isGrounded = false;
    bool isAlive = true;

    Rigidbody2D RB;

    public Text ScoreTxt;

    //for alternating keys
    bool keyAlternate = false;
    public int speed = 100;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        score = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        var movement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded == true)
            {
                RB.AddForce(Vector2.up * JumpForce);
                isGrounded = false;
            }
        }

        if (isAlive) {
            score += Time.deltaTime * 4;
            ScoreTxt.text = "Speed Score: " + score.ToString("F");
            transform.position += new Vector3(-0.001f, 0, 0) * Time.deltaTime * speed;
        }

        //alt buttom mashing
        if (Input.GetKeyDown(KeyCode.RightArrow) && keyAlternate == false)
        {
            Debug.Log("first key touched");
            keyAlternate = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && keyAlternate == true)
        {
            Debug.Log("within second statement");
            //transform.position += transform.forward * speed * Time.deltaTime;
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
            Debug.Log("after 2 Transform");
            keyAlternate = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            if (isGrounded == false) isGrounded = true;
        }

        if (collision.gameObject.CompareTag("spike"))
        {
            isAlive = false;
            Time.timeScale = 0;
        }
    }
}
