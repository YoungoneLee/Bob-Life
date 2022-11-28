using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RGameOver : MonoBehaviour
{
    public float score;
    public Text ScoreTxt;

    private void Awake()
    {
        score = PlayerPrefs.GetFloat("score");
        ScoreTxt.text = ("Score: " + score.ToString("F"));
    }

    public void NextScene()
    {
        SceneManager.LoadScene("HubScene");
    }

    public void RunningScene()
    {
        SceneManager.LoadScene("RunningScene");
    }
}