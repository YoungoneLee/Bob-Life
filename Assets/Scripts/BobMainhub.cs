using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BobMainhub : MonoBehaviour
{
    // These values go from 1 to 30, one point represents 10 pixels on screen bar
    public GameObject spdBar;
    public GameObject strBar;
    public GameObject jmpBar;
    public GameObject pauseMenu;
    public GameObject boberade;
    public GameObject bob;
    public AudioSource hubTheme;
    public AudioSource bobNoise1, bobNoise2, bobNoise3, bobNoise4, gulp;
    public AudioSource levelStr, levelSpd, levelJmp;

    //idle movement script
    Vector3 mousePos;
    public float maxMoveSpeed = 10;
    public float smoothTime = 0.3f;
    public float minDistance = 2;
    Vector2 currentVelocity;
    private Rigidbody2D rb;
    private float mapEdge = 7.1f;
    private bool isPaused = false;
    private float rand;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spdBar.GetComponent<Slider>().value = PlayerPrefs.GetInt("speed");
        strBar.GetComponent<Slider>().value = PlayerPrefs.GetInt("strength");
        jmpBar.GetComponent<Slider>().value = PlayerPrefs.GetInt("jump");
        Debug.Log(PlayerPrefs.GetInt("speed"));
        Debug.Log(PlayerPrefs.GetInt("strength"));
        Debug.Log(PlayerPrefs.GetInt("jump"));

        anim = bob.GetComponent<Animator>();
        boberade.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("speed") > 30)
        {
            PlayerPrefs.SetInt("speed", 30);
            levelSpd.Play();
        }
        if (PlayerPrefs.GetInt("strength") > 30)
        {
            PlayerPrefs.SetInt("strength", 30);
            levelStr.Play();
        }
        if (PlayerPrefs.GetInt("jump") > 30)
        {
            PlayerPrefs.SetInt("jump", 30);
            levelJmp.Play();
        }
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition += ((Vector2)transform.position - mousePosition).normalized * minDistance;
        float newPosition = Mathf.SmoothDamp(bob.transform.position.x, mousePosition.x, ref currentVelocity.x, smoothTime, maxMoveSpeed);
        // Temporary Solution, need to remember how to get collision working
        if (newPosition > mapEdge)
            newPosition = mapEdge;
        else if (newPosition < mapEdge * -1f)
            newPosition = mapEdge * -1f;

        // Pause Game by pressing 'P'
        if (Input.GetKeyDown(KeyCode.P))
            PauseGame();

        if (!isPaused)
        {
            bob.transform.position = new Vector3(newPosition, bob.transform.position.y, bob.transform.position.z);
            if (mousePosition.x > bob.transform.position.x)
                bob.transform.localScale = new Vector3(1, 1, 1);
            else if (mousePosition.x < bob.transform.position.x)
                bob.transform.localScale = new Vector3(-1, 1, 1);

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit)
                {
                    //Debug.Log(hit.transform.name);
                    if (hit.collider.CompareTag("Player"))
                        ClickBob();
                }
            }
        }

        if (!gulp.isPlaying)
        {
            anim.SetBool("Boberade", false);
            boberade.SetActive(false);
        }
    }

    public void changeSpeed(int increase)
    {
        levelSpd.Play();
        int speed = PlayerPrefs.GetInt("speed");
        speed += increase;
        spdBar.GetComponent<Slider>().value = speed;
        PlayerPrefs.SetInt("speed", speed);
    }

    public void changeStrength(int increase)
    {
        levelStr.Play();
        int strength = PlayerPrefs.GetInt("strength");
        strength += increase;
        strBar.GetComponent<Slider>().value = strength;
        PlayerPrefs.SetInt("strength", strength);
    }

    public void changeJump(int increase)
    {
        levelJmp.Play();
        int jump = PlayerPrefs.GetInt("jump");
        jump += increase;
        jmpBar.GetComponent<Slider>().value = jump;
        PlayerPrefs.SetInt("jump", jump);
    }

    public void ClickBob()
    {
        // Should be some kind of small interaction or sound effect
        Debug.Log("Bob Clicked!");

        rand = UnityEngine.Random.Range(0, 5);
        if(rand == 0)
        {
            bobNoise1.Play();
        }
        else if (rand == 1)
        {
            bobNoise2.Play();
        }
        else if (rand == 2)
        {
            bobNoise3.Play();
        }
        else if (rand == 3)
        {
            bobNoise4.Play();
        }
        else if(rand == 4)
        {
            boberade.SetActive(true);
            anim.SetBool("Boberade", true);
            gulp.Play();
        }
    }

    public void LoadRunGame()
    {
        SceneManager.LoadScene("RunningOpeningScene");
    }

    public void LoadWeightGame()
    {
        SceneManager.LoadScene("StrengthIntroScene");
    }

    public void LoadClimbGame()
    {
        SceneManager.LoadScene("DoodleOpenScene");
    }

    public void LoadGourmetRace()
    {
        SceneManager.LoadScene("GourmetRaceScene");
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Debug.Log("Resume");
            hubTheme.UnPause();
            pauseMenu.SetActive(false);
        }
        else
        {
            Debug.Log("Pause");
            hubTheme.Pause();
            pauseMenu.SetActive(true);
        }
        isPaused = !isPaused;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
