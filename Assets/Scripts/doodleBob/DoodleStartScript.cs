using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoodleStartScript : MonoBehaviour
{
    // Start is called before the first frame update

    public void loadDoodleGame() {
        SceneManager.LoadScene("JumpingScene");
    }
}