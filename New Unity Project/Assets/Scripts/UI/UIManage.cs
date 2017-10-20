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
    public GameObject Set;
    public GameObject mGameOverScreen;
    public GameObject mG1;
    public GameObject mGC;
    public GameObject mG2;
    float Mins;
    float Secs;


    private int Score;

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

    public void settingMenu()
    {
        TouchManager.mTouchManager.GetComponent<DrawTouch>().DestroyLine();
        Set.SetActive(true);
        Time.timeScale = 0f;
    }
    public void SettingMenuBack()
    {
        Set.SetActive(false);
        Time.timeScale = 1f;
    }
    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(0);
       
    }
    public void Retry()
    {
        SceneManager.LoadScene(2);
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
        mGC.SetActive(false);
        mGameOverScreen.SetActive(true);
        if (mGameOverScreen.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mG1.SetActive(false);
                mGC.SetActive(false);
                mG2.SetActive(true);
            }
        }
        GameManager.mGameManager.SetHighScore(Score);
        FScore.text = "Your Score: " + Score.ToString();
		FhScore.text = "Highest Score: " + GameManager.mGameManager.GetHighScore().ToString();
        Time.timeScale = 0f;
        GameObject.Find("SettingButton").GetComponent<Button>().enabled = false;      
    }

    public void OpenGameOverScreenMoves()//----------------------------------------------------Rafel
    {
        mG1.SetActive(false);
        mGameOverScreen.SetActive(true);
        if (mGameOverScreen.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mGC.SetActive(false);
                mG2.SetActive(true);
            }
        }
        GameManager.mGameManager.SetHighScore(Score);
        FScore.text = "Your Score: " + Score.ToString();
        FhScore.text = "Highest Score: " + GameManager.mGameManager.GetHighScore().ToString();
        Time.timeScale = 0f;
        GameObject.Find("SettingButton").GetComponent<Button>().enabled = false;
    }
    //public void SetHighscore()
    //{
    //    GameObject.Find("HNumber").GetComponent<Text>().text = "     " + GameManager.mGameManager.GetHighScore().ToString();
    //}

    void Update()
    {
        
        Mins = Mathf.FloorToInt(timeLeft / 60f);
        Secs = Mathf.FloorToInt(timeLeft % 60f);
        if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;

			Timer.text = " " + Mins + ":" + Secs;
        }
        else if (timeLeft > -1 && timeLeft < 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            OpenGameOverScreen();
        }
   

    }
}
