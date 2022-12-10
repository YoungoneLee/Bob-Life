using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DoodleBobMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    Animator anime;
    bool grounded;
    bool jumping;
    bool falling = true;
    public float sizeX, sizeY, ratio;
    float moveX;
    float speed = 12f;
    public bool isDead;
    private int jump;
    private int increase;
    public float score;
 

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * speed;
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            gameObject.transform.localScale = new Vector3(-0.6689f, 0.6768f, 1);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            gameObject.transform.localScale = new Vector3(0.6689f, 0.6768f, 1);
        }

        if(rb.velocity.y <= 0) {
            jumping = false;
            falling = true;
        }
        
    }
    void FixedUpdate() {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;

        if(jumping) {
            anime.SetBool("takeFlight", true);
            anime.SetBool("falling", false);
        }
        if(falling) {
            anime.SetBool("takeFlight", false);
            anime.SetBool("falling", true);
        }

        if (isDead == true) {
            SceneManager.LoadScene("DoodleEndScene");
            score = PlayerPrefs.GetFloat("score");
            increase = (int) score/500;
            jump = PlayerPrefs.GetInt("jump");
            if(jump > 0) {
                jump += increase;
                PlayerPrefs.SetInt("jump", jump);
            } else {
                jump += 1;
                PlayerPrefs.SetInt("jump", jump);
            }


        }

    }
    private void OnCollisionEnter2D (Collision2D collision) {
            jumping = true;
            falling = false;
    }

    void OnBecameInvisible() {
            isDead = true;
        }
    

}
