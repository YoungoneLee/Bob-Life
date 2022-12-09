using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RGameOver : MonoBehaviour
{
    public float score;
    public Text ScoreTxt;

    private void Start()
    {
        if (ScoreTxt == null)
        {
            ScoreTxt = GameObject.Find("Score").GetComponent<Text>();
        }
        ScoreTxt.text = ("Score: " + score.ToString("F"));
    }

    private void Awake()
    {
        score = PlayerPrefs.GetFloat("score");
    }

    public void NextScene()
    {
        SceneManager.LoadScene("HubScene");
    }

    public void RunningScene()
    {
        SceneManager.LoadScene("newRunningGame");
    }
}