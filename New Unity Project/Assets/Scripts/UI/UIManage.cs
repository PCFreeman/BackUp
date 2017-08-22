using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManage : MonoBehaviour {

    FMODUnity.StudioEventEmitter emitter;

    public static UIManage instance;

    public float timeMax;
    public float timeLeft;
    public Text Timer;
    public GameObject Set;
    public GameObject mGameOverScreen;
    float Mins;
    float Secs;


    int Score;

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

       

        Time.timeScale = 1f;
        //Start Score
        Score = 1;

        GameObject.Find("Number").GetComponent<Text>().text = Score.ToString();

        //Elrick's Code
        var target = GameObject.Find("Timer");
        emitter = target.GetComponent<FMODUnity.StudioEventEmitter>();
    }

    public void settingMenu()
    {
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
        //GameObject.Find("Canvas").SetActive(false);
        mGameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        GameObject.Find("SettingButton").GetComponent<Button>().enabled = false;
        
    }
    public void SetHighscore()
    {
        GameObject.Find("HNumber").GetComponent<Text>().text = "     " + GameManager.mGameManager.GetHighScore().ToString();
    }

    void Update()
    {
        
        Mins = Mathf.FloorToInt(timeLeft / 60f);
        Secs = Mathf.FloorToInt(timeLeft % 60f);

        
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            Timer.text = " " + Mins + ":" + Secs;
            
            //Elric's code
            if (timeLeft > 20)
            {
                emitter.SetParameter("Decider", 0);
            }
            else if (Secs < 20 && Secs > 10)
            {
                emitter.SetParameter("Decider", 7.51f);
            }
            else if (Secs < 10 && Secs > 0)
            {
                emitter.SetParameter("Decider", 9.01f);
            }
        }
        else if (timeLeft > -1 && timeLeft < 0)
        {
            timeLeft -= Time.deltaTime;

            emitter.SetParameter("Decider", 9.90f);
        }
        else
        {            
            OpenGameOverScreen();

            GameManager.mGameManager.SetHighScore(Score);

            GameObject.Find("FinalScore").GetComponent<Text>().text = "Final Score:   " + Score.ToString();
            GameObject.Find("HighScore").GetComponent<Text>().text = "High Score:   " + GameManager.mGameManager.GetHighScore().ToString();
        }
        SetHighscore();

    }
}
