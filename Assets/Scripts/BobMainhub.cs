using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BobMainhub : MonoBehaviour
{
    // These values go from 1 to 30, one point represents 10 pixels on screen bar
    public int speed;
    public int strength;
    public int jump;
    public GameObject spdBar;
    public GameObject strBar;
    public GameObject jmpBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSpeed(int increase)
    {
        speed += increase;
        spdBar.GetComponent<Slider>().value = speed;
    }

    public void changeStrength(int increase)
    {
        strength += increase;
        strBar.GetComponent<Slider>().value = strength;
    }

    public void changeJump(int increase)
    {
        jump += increase;
        jmpBar.GetComponent<Slider>().value = jump;
    }

    public void LoadRunGame()
    {
        SceneManager.LoadScene("RunningScene");
    }

    public void LoadWeightGame()
    {
        SceneManager.LoadScene("StrengthScene");
    }

    public void LoadClimbGame()
    {
        SceneManager.LoadScene("ClimbingScene");
    }

    public void LoadGourmetRace()
    {
        SceneManager.LoadScene("GourmetRaceScene");
    }
}
