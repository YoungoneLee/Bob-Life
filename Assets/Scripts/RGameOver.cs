using UnityEngine;
using UnityEngine.SceneManagement;


public class RGameOver : MonoBehaviour
{

    public void NextScene()
    {
        SceneManager.LoadScene("HubScene");
    }

    public void RunningScene()
    {
        SceneManager.LoadScene("RunningScene");
    }
}