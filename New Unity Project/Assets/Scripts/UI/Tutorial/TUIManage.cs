﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TUIManage : MonoBehaviour {

    public static TUIManage instance;

    public float timeMax;
    public float timeLeft;
    public Text Timer;
    public GameObject Set;
    public GameObject mGameOverScreen;
    float Mins;
    float Secs;
    public Sprite turnoffSound;
    public Sprite turnonSound;
    public Button Soundbutton;
    private bool SoundCheck;
    public Sprite turnoff;
    public Sprite turnon;
    public Button button;
    private bool MusicCheck;

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


       // SetHighscore();
        Time.timeScale = 1f;
        //Start Score
         Score = 103;

        GameObject.Find("Number").GetComponent<Text>().text = Score.ToString();



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

        mGameOverScreen.SetActive(true);
        if (mGameOverScreen.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }
       // GameManager.mGameManager.SetHighScore(Score);
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
        if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;

			Timer.text = " " + Mins + ":" + Secs;
        }
        else if (timeLeft > -1 && timeLeft < 0)
        {
            timeLeft -= Time.deltaTime;
        }
        if(Score>=150)
        {
           OpenGameOverScreen();
        }
   //     else
   //     {        
			//OpenGameOverScreen();
   //     }
   

    }
}
