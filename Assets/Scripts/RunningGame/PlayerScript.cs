using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{
    private float JumpForce = 500;
    private float backSpeed = 15;
    public float score;

    [SerializeField]
    bool isGrounded = false;
    bool isAlive = true;
    bool spacePressed = false;
    bool mashKey = false;

    Rigidbody2D RB;

    public Text ScoreTxt;
    public PlayerScript Bob;

    //for alternating keys
    bool keyAlternate = false;
    private int speed = 200;

    private void Awake()
    {
        isAlive = true;
        RB = GetComponent<Rigidbody2D>();
        score = 0;
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            score += Time.deltaTime * 4;
            //Debug.Log("Running Score: " + score);
            ScoreTxt.text = "Speed Score: " + Bob.score.ToString("F");
            PlayerPrefs.SetFloat("score", Bob.score);
            RB.AddForce(-transform.right * backSpeed);
        }

        if (spacePressed)
        {
            if (isGrounded == true)
            {
                RB.AddForce(Vector2.up * JumpForce);
                isGrounded = false;
            }
            spacePressed = false;
        }

        if(mashKey)
        {
            RB.AddForce(transform.right * speed);
            mashKey = false;
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }

        //alt buttom mashing
        if (Input.GetKeyDown(KeyCode.RightArrow) && keyAlternate == false) keyAlternate = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && keyAlternate == true)
        {
            keyAlternate = false;
            mashKey = true;
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
            //Time.timeScale = 0;
            gameOver();
        }
    }

    private void OnBecameInvisible()
    {
        isAlive = false;
        gameOver();
    }

    public void gameOver()
    {
        SceneManager.LoadScene("RunningGameOver");
        //GameObject.Find("Player").GetComponent<PlayerScript>().properInvoke();

    }
}
