using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunningScene()
    {
        SceneManager.LoadScene("newRunningGame");
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("HubScene");
    }

    public void JumpingScene() {
        SceneManager.LoadScene("JumpingScene");
    }


}
