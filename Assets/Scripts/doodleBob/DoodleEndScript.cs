using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoodleEndScript : MonoBehaviour
{
    public float score;
    public Text Score;

    public void loadDoodleGame() {
        SceneManager.LoadScene("JumpingScene");
        PlayerPrefs.SetFloat("score", 0);
    }
    public void loadHubScene() {
        SceneManager.LoadScene("HubScene");
        GameObject.FindGameObjectWithTag("DoodleMusic").GetComponent<DoodleMusic>().StopMusic();
        PlayerPrefs.SetFloat("score",0);
    }

    public void Start() {
        score = PlayerPrefs.GetFloat("score");
        Score.text = ("Score: " + score); 
    }
}
