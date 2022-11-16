using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DoodleBobMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Camera cam;
    private BoxCollider2D camBox;
    public float sizeX, sizeY, ratio;
    float moveX;
    float speed = 12f;
    public Vector3 screenPos;
    public bool isDead;
    float height;
    float width;
    

    // public Text Score;
     public float score;
    // public bool hasHit;
    


    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        Camera cam = GetComponent<Camera>();
        // camBox = GetComponent<BoxCollider2D>();
        height = cam.orthographicSize;
        width = height * cam.aspect;

    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * speed;
        sizeY = cam.orthographicSize * 2f;
        // ratio = (float)Screen.width/(float)Screen.height;
        // sizeX = sizeY * ratio;
        // camBox.size = new Vector2(sizeX, sizeY);
        // Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        
        
    }
    void FixedUpdate() {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;

        //  if(transform.position.y > camBox.size.y)  {
        //     Debug.Log("penis??!!!");
        //     // Destroy(gameObject);
        //     // PlayerPrefs.SetFloat("score", score);
        //     // SceneManager.LoadScene("DoodleEndScene");
        // }

        // if (transform.position.x > cam.transform.position.x  + width + 15f) {
        //     transform.position = new Vector3(transform.position.x - width*2, transform.position.y, transform.position.z);
        //  } else if(transform.position.x < cam.transform.position.x  - width - 15f) {
        //     transform.position = new Vector3(transform.position.x - width*2, transform.position.y, transform.position.z);
        //  }

        
        //     transform.position = new Vector2(-width, transform.position.y);
        // if (transform.position.x < -width)
        //     transform.position = new Vector2(width, transform.position.y);
        if (isDead == true) {
            SceneManager.LoadScene("DoodleEndScene");
        }

    }
    // private void OnCollisionEnter2D (Collision2D collision) {
    //     if (collision.gameObject.CompareTag("DoodlePlatform") && !hasHit) {
    //         hasHit = true;
    //         score += 100;
    //         Score.text = "Score: " + score.ToString("F");
    //         Debug.Log(score);
    //     }
    // }
    // public void EndGame() {
    //     if(transform.position.y > screenPos.y)  {
    //         Destroy(gameObject);
    //         PlayerPrefs.SetFloat("score", score);
    //         SceneManager.LoadScene("DoodleEndScene");
    //     }
    // }


    void OnBecameInvisible() {
            isDead = true;
        }
    

}
