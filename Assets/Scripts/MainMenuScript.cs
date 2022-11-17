using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        // Should also show Story Introduction here
        PlayerPrefs.SetInt("speed", 1);
        PlayerPrefs.SetInt("strength", 1);
        PlayerPrefs.SetInt("jump", 1);
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
