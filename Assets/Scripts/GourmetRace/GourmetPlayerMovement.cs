using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GourmetPlayerMovement : MonoBehaviour
{
    // Need to test minimum stats needed to beat Brutus
    private int statsSpeed = 20;
    private int jumpForce = 500;
    private int statsStrength = 10;

    bool jumpTriggered = false;
    bool isGrounded = false;

    Rigidbody2D RB;

    //for the time
    float time;
    public Text timeTxt;

    // Progress Bar
    float xPos;
    float startPos = -7.5f;
    float finishPos = 135.5f;

    //for hitting the blocks
    public int hit;
    public int brutusHits;
    public int bobHits;
    GameObject bob;
    GameObject brutus;
    GameObject finishLine;
    public Slider bobProgress;
    public GameObject blocks;

    private void Awake()
    {
        time = 0;
        //statsSpeed = PlayerPrefs.GetInt("speed");
        //jumpForce = PlayerPrefs.GetInt("jump") * 5;
        //statsStrength = PlayerPrefs.GetInt("strength");
    }

    // Start is called before the first frame update
    void Start()
    {
        hit = 13;
        RB = GetComponent<Rigidbody2D>();
        bob = GameObject.FindGameObjectWithTag("bob");
        brutus = GameObject.FindGameObjectWithTag("brutus");
        blocks = GameObject.FindGameObjectWithTag("bs1");
        Physics2D.IgnoreCollision(bob.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        finishLine = GameObject.FindGameObjectWithTag("gFinishLine");
        finishPos = finishLine.transform.position.x;

        Debug.Log("hits:" + hit);
        //brutusHits = hit - statsStrength;
        bobHits = hit - statsStrength;
        Debug.Log("bobHits" + bobHits);
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        timeTxt.text = "Speed Score: " + time.ToString("F");

        xPos = bob.transform.position.x;
        bobProgress.value = xPos - startPos;

        if (isGrounded && !jumpTriggered) {
            RB.AddForce(transform.right * statsSpeed);
        }

        if (jumpTriggered)
        {
            RB.velocity = new Vector3(0, 0, 0);
            RB.AddForce(transform.up * jumpForce);
            Debug.Log("jumped");
            jumpTriggered = false;
        }

        if (bobHits == 0) Destroy(blocks);
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(RB.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void winGame()
    {
        PlayerPrefs.SetFloat("bestTime", time);
        SceneManager.LoadScene("EndScene");
    }

    IEnumerator WaitFunction()
    {
        yield return new WaitForSeconds(3);
        winGame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("jumpTag"))
        {
            jumpTriggered = true;
        }

        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("on the ground");
            if (isGrounded == false)
            {
                isGrounded = true;
            }
        }

        if(collision.gameObject.CompareTag("bs1")) {
            Debug.Log("bobHits remaining: " + bobHits);
            bobHits -= 1;
            Destroy(blocks);
        }

        if (collision.gameObject.CompareTag("gFinishLine"))
        {
            StartCoroutine(WaitFunction());
        }
    }
}
