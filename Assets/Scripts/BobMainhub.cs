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


    //idle movement script
    Vector3 mousePos;
    public float maxMoveSpeed = 10;
    public float smoothTime = 0.3f;
    public float minDistance = 2;
    Vector2 currentVelocity;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition += ((Vector2)transform.position - mousePosition).normalized * minDistance;
        float newPosition = Mathf.SmoothDamp(transform.position.x, mousePosition.x, ref currentVelocity.x, smoothTime, maxMoveSpeed);
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
        if (mousePosition.x > transform.position.x)
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        else if (mousePosition.x < transform.position.x)
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
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
        SceneManager.LoadScene("StrengthIntroScene");
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
