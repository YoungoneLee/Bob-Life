using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("player").GetComponent<PlayerController>().isGameOver)
        {
            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                Debug.Log("within the fixed update, within highscore loop");
                PlayerPrefs.SetInt("HighScore", score);
                Debug.Log("New High Score is: " + score);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("score" + score);
            score += 1;
            scoreText.text = "Score : " + score;

        }
    }
}
