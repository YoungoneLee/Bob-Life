using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb2d;
    public float runSpeed;
    public int jumpForce;
    private int jumpCount = 0;
    private bool spacePressed = false;
    private bool canJump = true;
    public bool isGameOver = false;
    public GameObject GameOverPanel, scoreText;
    public Text FinalScoreText, HighScoreText;

    //alt stuff
    bool mashKey = false;
    bool keyAlternate = false;
    private int speed = 100;

    //for incrasing stats
    private int speeds;
    private float increase;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine("IncreaseGameSpeed");
        PlayerPrefs.GetInt("speed");
    }

    private void FixedUpdate()
    {

        if (rb2d.velocity.y < 0)
        {
            anim.SetBool("falling", true);
            anim.SetBool("jumping", false);
        }

        if (spacePressed && !isGameOver)
        {
            anim.SetBool("jumping", true);
            anim.SetBool("grounded", false);
            anim.SetBool("running", false);
            rb2d.AddForce(Vector2.up * jumpForce);
            jumpCount += 1;
            spacePressed = false;
        }

        if (mashKey)
        {
            anim.SetBool("running", true);
            rb2d.AddForce(transform.right * speed);
            mashKey = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            anim.SetBool("grounded", true);
            anim.SetBool("falling", false);
            jumpCount = 0;
            canJump = true;
        }

        if ((collision.gameObject.CompareTag("BottomDetector") || collision.gameObject.CompareTag("Obstacle")) && !isGameOver)
        {
            GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canJump) spacePressed = true;
        if (jumpCount == 2) canJump = false;

        //alt buttom mashing
        if (Input.GetKeyDown(KeyCode.RightArrow) && keyAlternate == false) keyAlternate = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && keyAlternate == true)
        {
            keyAlternate = false;
            mashKey = true;
        }

    }

    public void GameOver()
    {
        isGameOver = true;
        StopCoroutine("IncreaseGameSpeed");
        ShowGameOverPanel();
        //increase = GameObject.Find("ScoreDetector").GetComponent<ScoreSystem>().score;
        speeds = GameObject.Find("ScoreDetector").GetComponent<ScoreSystem>().score;
        //speeds = (int) Mathf.Ceil((increase / 2));
        Debug.Log("speeds score: " + speeds);
        int newspeed = PlayerPrefs.GetInt("speed") + speeds;
        PlayerPrefs.SetInt("speed", newspeed);
    }

    IEnumerator IncreaseGameSpeed()
    {
        while(true)
        {
            yield return new WaitForSeconds(10);
            if(runSpeed < 8)
            {
                runSpeed += 0.2f;
            }
            if (GameObject.Find("GroundSpawner").GetComponent<ObstacleSpawner>().obstacleSpawnInterval > 1)
            {
                GameObject.Find("GroundSpawner").GetComponent<ObstacleSpawner>().obstacleSpawnInterval -= 0.1f;
            }
        }

        
    }

    void ShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        scoreText.SetActive(false);

        FinalScoreText.text = "Score : " + GameObject.Find("ScoreDetector").GetComponent<ScoreSystem>().score;
        HighScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore");

    }

}
