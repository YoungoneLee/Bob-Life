using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StrengthGameManager : MonoBehaviour
{

    //For when music is added
    //public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;
    public StrengthButtons StrengthButton;
    public AudioSource rhythmMusic;
    public AudioSource brutalMusic;
    public AudioSource missSound, hitSound, perfectSound, glassSound;
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

    //animation stuff
    public GameObject bob;
    Animator bobAnim;

    public GameObject dumbbell;
    Animator dumbAnim;

    public GameObject barbell;
    Animator barAnim;

    public GameObject boberade;
    Animator boberadeAnim;

    public GameObject glove;
    Animator gloveAnim;

    public GameObject bag;
    Animator bagAnim;

    public GameObject deadBob;

    public GameObject sandbag, wallHole;
    Animator sandbagAnim;

    public AudioSource grunt1, grunt2, grunt3, deathSound, drinkSound, explosionSound, punchSound, fallSound, oofSound;

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

        bobAnim = bob.GetComponent<Animator>();
        dumbAnim = dumbbell.GetComponent<Animator>();
        barAnim = barbell.GetComponent<Animator>();
        boberadeAnim = boberade.GetComponent<Animator>();
        gloveAnim = glove.GetComponent<Animator>();
        bagAnim = bag.GetComponent<Animator>();
        sandbagAnim = sandbag.GetComponent<Animator>();

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
                    bobAnim.SetBool("rightPressed", false);
                    bobAnim.SetBool("downPressed", false);
                    gloveAnim.SetBool("punching", false);
                    glove.gameObject.SetActive(false);
                    boberade.gameObject.SetActive(false);
                    dumbbell.gameObject.SetActive(false);
                    barbell.gameObject.SetActive(false);
                    bag.gameObject.SetActive(false);

                    if(!bob.gameObject.activeInHierarchy)
                    {
                        bob.gameObject.SetActive(true);
                        deadBob.gameObject.SetActive(false);
                    }

                    sandbag.gameObject.SetActive(true);
                    glove.gameObject.SetActive(true);
                    bobAnim.SetBool("windup", true);
                    gloveAnim.SetBool("windupPunch", true);

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

    public void playNoteAnim(KeyCode key) {
        bobAnim.SetBool("rightPressed", false);
        bobAnim.SetBool("downPressed", false);
        gloveAnim.SetBool("punching", false);
        glove.gameObject.SetActive(false);
        boberade.gameObject.SetActive(false);
        dumbbell.gameObject.SetActive(false);
        barbell.gameObject.SetActive(false);
        bag.gameObject.SetActive(false);
        //punchSound.Stop();
        drinkSound.Stop();
        grunt1.Stop();
        grunt2.Stop();
        grunt3.Stop();

        if(!bob.gameObject.activeInHierarchy)
        {
            bob.gameObject.SetActive(true);
            deadBob.gameObject.SetActive(false);
        }

        if(key == KeyCode.RightArrow) {
            bobAnim.SetBool("rightPressed", true);
            glove.gameObject.SetActive(true);
            gloveAnim.SetBool("punching", true);
            bag.gameObject.SetActive(true);
            punchSound.Play();
        }
        if(key == KeyCode.DownArrow) {
            bobAnim.SetBool("downPressed", true);
            boberade.gameObject.SetActive(true);
            drinkSound.Play();
        }
        if(key == KeyCode.UpArrow) {
            barbell.gameObject.SetActive(true);
            grunt();
        }
        if(key == KeyCode.LeftArrow) {
            dumbbell.gameObject.SetActive(true);
            grunt();
        }
    }

    public void grunt()
    {
        float r = 0;
        r = Random.Range(1,4);
        if(r == 1)
        {
            grunt1.Play();
        }
        else if (r == 2)
        {
            grunt2.Play();
        }
        else
        {
            grunt3.Play();
        }
    }

    public void playMusic()
    {
        rhythmMusic.Play();
    }

    public void playBrutalMusic()
    {
        brutalMusic.Play();
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
        hitSound.Play();

        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
    }

    public void GoodHit()
    {
        hitSound.Play();

        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;
    }

    public void PerfectHit()
    {
        perfectSound.Play();

        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        
        perfectHits++;
    }

    public void NoteMissed()
    {
        missSound.Play();
        deathSound.Play();

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;

        missedHits++;

        //Dead Part
        bobAnim.SetBool("rightPressed", false);
        bobAnim.SetBool("downPressed", false);
        gloveAnim.SetBool("punching", false);
        glove.gameObject.SetActive(false);
        boberade.gameObject.SetActive(false);
        dumbbell.gameObject.SetActive(false);
        barbell.gameObject.SetActive(false);
        bag.gameObject.SetActive(false);

        bob.gameObject.SetActive(false);
        deadBob.gameObject.SetActive(true);
    }

    public void showResults()
    {
        readyText.gameObject.SetActive(false);
        pressText.gameObject.SetActive(false);

        normalsText.text = "" + normalHits;
        goodsText.text = goodHits.ToString();
        perfectsText.text = "" + perfectHits;
        missesText.text = "" + missedHits;

        float extraMisses = missedHits - (totalNotes - normalHits - goodHits - perfectHits);
        
        float percentHit = (100f * (normalHits + goodHits + perfectHits) / (totalNotes + extraMisses));
        percentHitText.text = "" + percentHit.ToString("F1") + "%";
        
        string rankVal = "F";

        float scoreNoteRatio = (normalHits + goodHits * 2 + perfectHits * 3 - missedHits)/totalNotes;
        
        /*
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
        */

        //Debug.Log("isBrutal = " + theBS.isBrutal);
        //Debug.Log("currentScore = " + currentScore);

        //RANKINGS UPDATE
        /*
        if((!theBS.isBrutal && currentScore >= 9000) || (theBS.isBrutal && currentScore >= 16000))
        {
            rankVal = "EPICPOG";
        }
        else if((!theBS.isBrutal && currentScore >= 8000) || (theBS.isBrutal && currentScore >= 14000))
        {
            rankVal = "S";
        }
        else if((!theBS.isBrutal && currentScore >= 6000) || (theBS.isBrutal && currentScore >= 11000))
        {
            rankVal = "A";
        }
        else if((!theBS.isBrutal && currentScore >= 4500) || (theBS.isBrutal && currentScore >= 8500))
        {
            rankVal = "B";
        }
        else if((!theBS.isBrutal && currentScore >= 3000) || (theBS.isBrutal && currentScore >= 6500))
        {
            rankVal = "C";
        }
        else if((!theBS.isBrutal && currentScore >= 2000) || (theBS.isBrutal && currentScore >= 5000))
        {
            rankVal = "D";
        }
        */

        gloveAnim.SetBool("punching", true);
        bobAnim.SetBool("punch", true);
        bobAnim.SetBool("windup", false);
        punchSound.Play();

        StartCoroutine(waiter(.25f));

        //RANKINGS UPDATE 2
        if(scoreNoteRatio >= 2.5)
        {
            rankVal = "EPICPOG";
            sandbagAnim.SetBool("pogRank", true);
        }
        else if(scoreNoteRatio >= 2.2)
        {
            rankVal = "S";
            sandbagAnim.SetBool("greatRank", true);
            explosionSound.Play();
        }
        else if(scoreNoteRatio >= 2)
        {
            rankVal = "A";
            sandbagAnim.SetBool("greatRank", true);
            explosionSound.Play();
        }
        else if(scoreNoteRatio >= 1.75)
        {
            rankVal = "B";
            sandbagAnim.SetBool("goodRank", true);
        }
        else if(scoreNoteRatio >= 1.25)
        {
            rankVal = "C";
            sandbagAnim.SetBool("goodRank", true);
        }
        else if(scoreNoteRatio >= 0.6)
        {
            rankVal = "D";
            sandbagAnim.SetBool("badRank", true);
            oofSound.Play();
        }
        else
        {
            sandbagAnim.SetBool("badRank", true);
            oofSound.Play();
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

        StartCoroutine(resultsWaiter(rankVal));
    }

    IEnumerator waiter(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator resultsWaiter(string rank)
    {
        yield return new WaitForSeconds(.75f);

        sandbag.gameObject.SetActive(false);
        glove.gameObject.SetActive(false);
        bobAnim.SetBool("punch", false);

        if(rank == "EPICPOG")
        {
            glassSound.Play();
            wallHole.gameObject.SetActive(true);
        }
        else if(rank == "C" || rank == "B")
        {
            fallSound.Play();
        }

        yield return new WaitForSeconds(1f);

        resultsScreen.SetActive(true);
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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
