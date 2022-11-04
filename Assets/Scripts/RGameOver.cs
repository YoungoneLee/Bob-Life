using UnityEngine;
using UnityEngine.SceneManagement;


public class RGameOver : MonoBehaviour
{

    public void NextScene()
    {
        SceneManager.LoadScene("HubScene");
    }
}