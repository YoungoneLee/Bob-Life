using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthGameManager : MonoBehaviour
{

    //For when music is added
    //public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;
    public StrengthButtons StrengthButton;
    ////////////////////////////////
    // 1. Add music file to scene
    // 2. Turn off play on awake

    public static StrengthGameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    //Reaction Test
    [SerializeField]
    private Text readyText, pressText;

    //[SerializeField]
    //private SpriteRenderer whiteRect;

    private float reactionTime, startTime, randomDelay;

    private bool clockIsTicking, timerCanBeStopped;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, reactionSpeedText, rankText, finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        reactionTime = 0f;
        startTime = 0f;
        clockIsTicking = false;
        timerCanBeStopped = true;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            // if(Input.anyKeyDown)
            // {
            //     startPlaying = true;
            //     theBS.hasStarted = true;
                
            //     totalNotes = FindObjectsOfType<NoteObject>().Length;

            //     //theMusic.Play();
            // }
        }
        else
        {
            if(FindObjectsOfType<NoteObject>().Length == 0 && !resultsScreen.activeInHierarchy)
            {
                //Reaction Test
                if(!clockIsTicking && !resultsScreen.activeInHierarchy)
                {
                    StartCoroutine(startRandomTimer());
                    clockIsTicking = true;
                    timerCanBeStopped = false;
                }
                else if(Input.GetKeyDown(KeyCode.Space) && clockIsTicking && timerCanBeStopped)
                {
                    StopCoroutine(startRandomTimer());
                    reactionTime = Time.time - startTime;
                    if(reactionTime <= 2f)
                    {
                        currentScore = (int)((double)currentScore * (1.5 - reactionTime * 0.25));
                    }
                    showResults();
                }
                else if(Input.GetKeyDown(KeyCode.Space) && clockIsTicking && !timerCanBeStopped)
                {
                    StopCoroutine(startRandomTimer());
                    reactionTime = -1f;
                    showResults();
                }

            }
        }
    }

    public void NoteHit()
    {
        scoreText.text = "Note Score: " + currentScore;

        if(currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if(multiplierThresholds[currentMultiplier - 1] <=  multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        
        perfectHits++;
    }

    public void NoteMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;

        missedHits++;
    }

    public void showResults()
    {
        readyText.gameObject.SetActive(false);
        pressText.gameObject.SetActive(false);


        resultsScreen.SetActive(true);
        normalsText.text = "" + normalHits;
        goodsText.text = goodHits.ToString();
        perfectsText.text = "" + perfectHits;
        missesText.text = "" + missedHits;

        float extraMisses = missedHits - (totalNotes - normalHits - goodHits - perfectHits);
        
        float percentHit = (100f * (normalHits + goodHits + perfectHits) / (totalNotes + extraMisses));
        percentHitText.text = "" + percentHit.ToString("F1") + "%";
        
        string rankVal = "F";
        
        if(percentHit >= 99)
        {
            rankVal = "EPICPOG";
        }
        else if(percentHit >= 95)
        {
            rankVal = "S";
        }
        else if(percentHit >= 90)
        {
            rankVal = "A";
        }
        else if(percentHit >= 80)
        {
            rankVal = "B";
        }
        else if(percentHit >= 60)
        {
            rankVal = "C";
        }
        else if(percentHit >= 40)
        {
            rankVal = "D";
        }

        if(reactionTime == -1f)
        {
            reactionSpeedText.text = "Too Early!";
        }
        else
        {
            reactionSpeedText.text = "" + reactionTime.ToString("F3");
        }

        rankText.text = rankVal;

        finalScoreText.text = "" + currentScore;

    }

    IEnumerator startRandomTimer()
    {
        yield return new WaitForSecondsRealtime(1f);
        readyText.text = "Get Ready";
        readyText.gameObject.SetActive(true);

        clockIsTicking = true;
        timerCanBeStopped = false;


        randomDelay = Random.Range(2f, 5f);
        yield return new WaitForSecondsRealtime(randomDelay);


        if(!resultsScreen.activeInHierarchy)
        {
            pressText.gameObject.SetActive(true);
        }
        startTime = Time.time;
        clockIsTicking = true;
        timerCanBeStopped = true;
    }

    public void startGame(int difficulty)
    {
        startPlaying = true;
        theBS.hasStarted = true;

        if(difficulty == 2)
        {
            theBS.isBrutal = true;
        }
        
        theBS.generateNotes();

        totalNotes = FindObjectsOfType<NoteObject>().Length;

        //theMusic.Play();
    }
}
