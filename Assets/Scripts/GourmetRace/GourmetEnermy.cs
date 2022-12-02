using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GourmetEnermy : MonoBehaviour
{
    public int enemySpeed = 10;
    public int enemyJump = 500;
    public int enemyStrength = 10;

    bool jumpTriggered = false;
    bool isGrounded = false;

    Rigidbody2D RB;

    //for the time
    float bestTime;
    float enemyTime;
    public Text enemyTimeTxt;

    // Progress Bar
    float xPos;
    float startPos = -4.5f;
    float finishPos = 135.5f;

    //for hitting the blocks
    public int hit;
    public int enemyHits;
    public int bobHits;
    GameObject bob;
    GameObject brutus;
    GameObject finishLine;
    public Slider brutusProgress;
    public GameObject blocks;

    private void Awake()
    {
        enemyTime = 0;
        bestTime = PlayerPrefs.GetFloat("bestTime");
    }

    // Start is called before the first frame update
    void Start()
    {
        hit = 13;
        RB = GetComponent<Rigidbody2D>();
        bob = GameObject.FindGameObjectWithTag("bob");
        blocks = GameObject.FindGameObjectWithTag("blocks");
        Physics2D.IgnoreCollision(bob.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        finishLine = GameObject.FindGameObjectWithTag("gFinishLine");
        finishPos = finishLine.transform.position.x;

        enemyHits = hit - enemyStrength;
    }

    private void FixedUpdate()
    {
        enemyTime += Time.deltaTime;
        enemyTimeTxt.text = "High Score: " + bestTime.ToString("F");

        xPos = this.transform.position.x;
        brutusProgress.value = xPos - startPos;

        if (isGrounded && !jumpTriggered)
        {
            RB.AddForce(transform.right * enemySpeed);
        }

        if (jumpTriggered)
        {
            RB.velocity = new Vector3(0, 0, 0);
            RB.AddForce(transform.up * enemyJump);
            Debug.Log("jumped");
            jumpTriggered = false;
        }

        if (enemyHits == 0) Destroy(blocks);
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(RB.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void gameOver()
    {
        Debug.Log("Brutus Wins!");
        Debug.Log(enemyTime);
        PlayerPrefs.SetFloat("bestTime", enemyTime);
        SceneManager.LoadScene("GourmetLose");
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

        if (collision.gameObject.CompareTag("blocks"))
        {
            enemyHits -= 1;
        }

        if (collision.gameObject.CompareTag("gFinishLine"))
        {
            gameOver();
        }
    }

}
