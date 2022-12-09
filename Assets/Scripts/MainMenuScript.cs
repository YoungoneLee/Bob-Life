using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void NewGame()
    {
        // Should also show Story Introduction here
        PlayerPrefs.SetInt("speed", 1);
        PlayerPrefs.SetInt("strength", 1);
        PlayerPrefs.SetInt("jump", 1);
        PlayerPrefs.SetFloat("bestTime", 20);
        SceneManager.LoadScene("HubScene");
    }

    public void LoadHub()
    {
        SceneManager.LoadScene("HubScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
