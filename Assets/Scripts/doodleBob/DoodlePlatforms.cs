using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoodlePlatforms : MonoBehaviour
{
    public Text Score;
    public DoodleBobMovement Bob;
    public AudioSource boing;
    public float jumpForce = 10f;
    private bool hasHit;

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.relativeVelocity.y <= 0f) {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null) {
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
                boing.Play();
                if( !hasHit) {
                    hasHit = true;
                    Bob.score += 100;
                    Score.text = "Score: " + Bob.score.ToString("0");
                    PlayerPrefs.SetFloat("score", Bob.score);
                }
                if(Bob.score > 2000) {
                    Destroy(gameObject);
                } 
            }
            
        }

        // if (collision.gameObject.CompareTag("Player") && !hasHit) {
        //     hasHit = true;
        //     Bob.score += 100;
        //     Score.text = "Score: " + Bob.score.ToString("F");
        //     // Debug.Log(score);
        // }
    }
}
