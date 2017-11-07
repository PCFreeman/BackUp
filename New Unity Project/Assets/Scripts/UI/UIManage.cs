using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManage : MonoBehaviour {

    public static UIManage instance;

    public float timeMax;
    public float timeLeft;
    public Text Timer;
    public Text FhScore;
    public Text FScore;
    public Text Left;
    public GameObject Set;
    public GameObject mGameOverScreen;
    public GameObject mG1;
    public Text mGC;
    public GameObject mG2;
    public Sprite turnoff;
    public Sprite turnon;
    public Button button;
    private bool MusicCheck;
    float Mins;
    float Secs;
    public ParticleSystem particle;
    public Sprite turnoffSound;
    public Sprite turnonSound;
    public Button Soundbutton;
    private bool SoundCheck;
    public GameObject Touch;
    private int Score;
    public Text TryLimit;
    public Text nextLevel;
    public Text ShapeTimeLimit;
    public int current;

    private void Awake()
    {
        //Check if instance already exist
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this) //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a TouchManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);


        //SetHighscore();
        Time.timeScale = 1f;
        //Start Score
         Score = 103;

        GameObject.Find("Number").GetComponent<Text>().text = Score.ToString();



    }

    private void Start()
    {
        MusicCheck = true;
        SoundCheck = true;
        TryLimit.gameObject.SetActive(false);

    }
    public void MusicSwitch()
    {
        if (MusicCheck == true)
        {
            button.image.overrideSprite = turnoff;
            MusicCheck = false;
            AudioController.sInstance.MuteEndlessBGM();
        }
        else
        {
            button.image.overrideSprite = turnon;
            AudioController.sInstance.MuteEndlessBGM();
            MusicCheck = true;
        }

    }

    public void SoundSwitch()
    {
        if (SoundCheck == true)
        {
            Soundbutton.image.overrideSprite = turnoffSound;
            SoundCheck = false;
            AudioController.sInstance.MuteSFX();
        }
        else
        {
            Soundbutton.image.overrideSprite = turnonSound;
            AudioController.sInstance.MuteSFX();
            SoundCheck = true;
        }
    }
    public void settingMenu()
    {
        TouchManager.mTouchManager.GetComponent<DrawTouch>().DestroyLine();
        Set.SetActive(true);
        Time.timeScale = 0f;
        Touch.SetActive(false);
    }
    public void SettingMenuBack()
    {
        Set.SetActive(false);
        Time.timeScale = 1f;
        Touch.SetActive(true);
    }
    public void BacktoMainMenu()
    {
        mGameOverScreen.SetActive(true);
        mG1.SetActive(false);
        mG2.SetActive(true);
        Left.gameObject.SetActive(true);
        ShowScoreInGameOver(Score);

    }
    public void BacktoMainMenuButton()
    {
        StartCoroutine(LoadingManager.instance.LoadingScreen(0));
    }
    public void Retry()
    {
        StartCoroutine(LoadingManager.instance.LoadingScreen(2));
    }
    public void AddScore(int pluse)
    {
       Score=Score + pluse;
        GameObject.Find("Number").GetComponent<Text>().text = Score.ToString();
    }
    public void AddTime(int T)
    {
        timeMax = timeMax + T;
        timeLeft = timeLeft + T;
    }
   
    public void OpenGameOverScreen()
    {
        
        mGameOverScreen.SetActive(true);
        if (mGameOverScreen.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mG1.SetActive(false);
                mG2.SetActive(true);
            }
        }
        ShowScoreInGameOver(Score);
    }

    void ShowScoreInGameOver(int s)
    {
        GameManager.mGameManager.SetHighScore(s);
        FScore.text = "Your Score: " + s.ToString();
		FhScore.text = "Highest Score: " + GameManager.mGameManager.GetHighScore().ToString();
        Time.timeScale = 0f;
        GameObject.Find("SettingButton").GetComponent<Button>().enabled = false;      
    }
    public void OpenGameOverScreenMoves()//----------------------------------------------------Rafel
    {
       
        mGameOverScreen.SetActive(true);
        mG1.SetActive(false);
        mG2.SetActive(true);
        mGC.gameObject.SetActive(true);

        ShowScoreInGameOver(Score);
    }
    public void SetHighscore()
    {
        GameObject.Find("HNumber").GetComponent<Text>().text = "     " + GameManager.mGameManager.GetHighScore().ToString();
    }

    public void UpdateNextLevel(int l)//----------------------------------------------------Rafel
    {
        nextLevel.text = l.ToString();
    }

    public void UpdateShapesTry(int n)//----------------------------------------------------Rafel
    {
        if (current < 0)
        {
            current = n;
        }

        if (current != n)
        {
            TryLimit.gameObject.SetActive(true);
            TryLimit.text = n.ToString();
            Invoke("DeactiveShapesTry", 1);
            current = n;
        }

    }

    public void DeactiveShapesTry()
    {
        TryLimit.gameObject.SetActive(false);
    }

    public void UpdateShapesTimeLimit(float timeLimit)//----------------------------------------------------Rafel
    {
        ShapeTimeLimit.text = Mathf.FloorToInt(timeLimit % 60f).ToString();
    }
    void Update()
    {

        Mins = Mathf.FloorToInt(timeLeft / 60f);
        Secs = Mathf.FloorToInt(timeLeft % 60f);
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            Timer.text = " " + Mins + ":" + Secs;
            if (timeLeft < 10&&timeLeft>9)
            {
                AudioController.sInstance.TimeNearEnd();
            }
        }
        else
        {
            AudioController.sInstance.GameOverSFX();
            OpenGameOverScreen();
        }
   

    }
}
