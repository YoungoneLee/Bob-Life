using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GourmetLoseScript : MonoBehaviour
{
    public float score;
    public Text ScoreTxt;

    private void Awake()
    {
        score = PlayerPrefs.GetFloat("bestTime");
        ScoreTxt.text = ("Brutus' Score: " + score.ToString("F") + "s");
    }

    public void NextScene()
    {
        SceneManager.LoadScene("HubScene");
    }
}
