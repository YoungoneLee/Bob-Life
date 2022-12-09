using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MovingPlatform : MonoBehaviour
{

    public float speed;
    public int startingPoint;
    public Transform[] points;
    public Text Score;
    public DoodleBobMovement Bob;
    public float jumpForce = 10f;
    private bool hasHit;

    private int i;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f) {
            i++;
            if (i == points.Length) {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.relativeVelocity.y <= 0f) {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null) {
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
                if( !hasHit) {
                    hasHit = true;
                    Bob.score += 100;
                    Score.text = "Score: " + Bob.score.ToString("0");
                    PlayerPrefs.SetFloat("score", Bob.score);
                }
            }
            
        }
    }

}
