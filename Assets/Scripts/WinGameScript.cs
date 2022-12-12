using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameScript : MonoBehaviour
{
    public Sprite bobbette;
    public Sprite brutus;
    public GameObject trueLove;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        int str = PlayerPrefs.GetInt("strength");
        int spd = PlayerPrefs.GetInt("speed");
        int jmp = PlayerPrefs.GetInt("jump");

        if (str + spd + jmp >= 60)
        {
            Debug.Log("Secret!");
            panel.SetActive(true);
        }
        Debug.Log("Win!");
    }

    public void chooseBobbette()
    {
        trueLove.GetComponent<SpriteRenderer>().sprite = bobbette;
        panel.SetActive(false);
    }

    public void chooseBrutus()
    {
        trueLove.GetComponent<SpriteRenderer>().sprite = brutus;
        trueLove.transform.position += new Vector3(-0.35f, 0.63f, 0);
        panel.SetActive(false);
    }
}
