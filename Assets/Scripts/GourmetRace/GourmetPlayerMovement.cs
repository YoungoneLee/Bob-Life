using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.VersionControl;
using Unity.VisualScripting;
using TMPro;

public class GourmetPlayerMovement : MonoBehaviour
{
    private int statsSpeed = 15;
    private int jumpForce = 400;
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

    Animator anim;

    public GameObject bob;
    public GameObject brutus;
    public GameObject punchingGlove;
    GameObject finishLine;
    public Slider bobProgress;
    public TextMeshProUGUI powerUpText;
    public Image powerUpBG;

    private void Awake()
    {
        time = 0;
        statsSpeed = PlayerPrefs.GetInt("speed");
        jumpForce = PlayerPrefs.GetInt("jump") * 25;
        statsStrength = PlayerPrefs.GetInt("strength");
        Debug.Log("Speed");
        Debug.Log(statsSpeed);
        Debug.Log("Jump");
        Debug.Log(jumpForce);
        Debug.Log("Strength");
        Debug.Log(statsStrength);
    }

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        //bob = GameObject.FindGameObjectWithTag("bob");
        //brutus = GameObject.FindGameObjectWithTag("brutus");
        //Physics2D.IgnoreCollision(bob.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        finishLine = GameObject.FindGameObjectWithTag("gFinishLine");
        //finishPos = finishLine.transform.position.x;
        finishPos = 139.77f;
        powerUpText.text = "";
        powerUpBG.enabled = false;
        punchingGlove.SetActive(false);

        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        timeTxt.text = "Speed Score: " + time.ToString("F");

        xPos = bob.transform.position.x;
        bobProgress.value = xPos - startPos;

        if (isGrounded && !jumpTriggered && RB.velocity.x < statsSpeed) {
            RB.AddForce(transform.right * statsSpeed);
            //Debug.Log(RB.velocity);
            anim.SetBool("running", true);
        }

        if (jumpTriggered)
        {
            RB.velocity = new Vector3(0, 0, 0);
            RB.AddForce(transform.up * jumpForce);
            Debug.Log("jumped");
            jumpTriggered = false;
            anim.SetBool("grounded", false);
            anim.SetBool("jumping", true);
        }

        // Fall Animation
        if (anim.GetBool("jumping") && RB.velocity.y < 0)
        {
            anim.SetBool("falling", true);
            anim.SetBool("jumping", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Physics2D.IgnoreCollision(RB.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void drinkBoberade(GameObject boberade)
    {
        string flavor = boberade.GetComponent<BoberadeScript>().flavor;
        if (flavor == "J")
        {
            // Boost Jump Stat
            jumpForce += 200;
            powerUpDisplay("Jump");
        }
        else if (flavor == "Sp")
        {
            // Boost Speed Stat
            statsSpeed += 5;
            powerUpDisplay("Speed");
        }
        else if (flavor == "St")
        {
            // Boost Strength Stat;
            statsStrength += 5;
            powerUpDisplay("Strength");
        }
        Destroy(boberade);
    }

    public void winGame()
    {
        PlayerPrefs.SetFloat("bestTime", time);
        SceneManager.LoadScene("EndScene");
    }

    public void powerUpDisplay(string stat)
    {
        powerUpBG.enabled = true;
        powerUpText.text = "Temp. " + stat + " Boost!";
    }

    IEnumerator WaitFunction()
    {
        yield return new WaitForSeconds(0.1f);
        winGame();
    }

    IEnumerator PunchBlock(GameObject block)
    {
        while (block != null && block.GetComponent<BlockScript>().health > 0)
        {
            // Play Punching Animation here
            Debug.Log("Punching Block");
            block.GetComponent<BlockScript>().getHit(statsStrength);

            yield return new WaitForSeconds(0.5f);
        }
        anim.SetBool("punching", false);
        anim.SetBool("running", true);
        punchingGlove.SetActive(false);

        Debug.Log("Block Destroyed!");
        Destroy(block);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision!");
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("jumpTag"))
        {
            jumpTriggered = true;
            anim.SetBool("falling", false);
            anim.SetBool("jumping", true);
        }
        else if (collision.gameObject.CompareTag("ground"))
        {
            //Debug.Log("on the ground");
            if (isGrounded == false)
            {
                isGrounded = true;
            }
            anim.SetBool("grounded", true);
            anim.SetBool("jumping", false);
            anim.SetBool("falling", false);
        }
        else if (collision.gameObject.CompareTag("blocks"))
        {
            StartCoroutine(PunchBlock(collision.gameObject));
            anim.SetBool("running", false);
            anim.SetBool("punching", true);
            punchingGlove.SetActive(true);
            Debug.Log(bob.transform.position);
            Debug.Log(punchingGlove.transform.position);

        }
        else if (collision.gameObject.CompareTag("gFinishLine"))
        {
            StartCoroutine(WaitFunction());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "boberade")
        {
            Debug.Log("Drink Boberade!");
            drinkBoberade(collider.gameObject);
        }
    }
}
