using System.Collections;
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
    public GameObject Touch;
    public Image imageColldown;
    public int cooldown;
    public Text Moves;
    public int Moveleft;
    public static ComboSystem WoboCombo;
    public GameObject mArea;
    private float countdown;
    private float MuliteCountDown;
    public Sprite[] WhiteNumberPool;
    public Image mSingle;
    public Image mDouble1;
    public Image mDouble2;
    string MultiString;
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
         Score = 5000;

        GameObject.Find("Number").GetComponent<Text>().text = Score.ToString();



    }
    private void Start()
    {
        WoboCombo = GameObject.Find("TWoBoCombo").GetComponent<ComboSystem>();
        MusicCheck = true;
        SoundCheck = true;

    }
    public void settingMenu()
    {
        GameObject.Find("Points Area").transform.position = new Vector3(0, 500, 0);
        GameObject.Find("Selected Points Area").transform.position = new Vector3(0, 500, 0);
        Set.SetActive(true);
        Time.timeScale = 0f;
        Touch.SetActive(false);
    }
    public void SettingMenuBack()
    {
        GameObject.Find("Points Area").transform.position = new Vector3(-10, -40, -50);
        GameObject.Find("Selected Points Area").transform.position = new Vector3(-10, -40, -50);
        Set.SetActive(false);
        Time.timeScale = 1f;
        Touch.SetActive(true);
    }

    public void MusicSwitch()
    {
        if (MusicCheck == true)
        {
            button.image.overrideSprite = turnoff;
            MusicCheck = false;
            AudioController.sInstance.MuteTutorialBGM();
        }
        else
        {
            button.image.overrideSprite = turnon;
            AudioController.sInstance.MuteTutorialBGM();
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
        Time.timeScale = 1f;
        StartCoroutine(LoadingManager.instance.LoadingScreen(0));

    }
    public void Retry()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadingManager.instance.LoadingScreen(2));
    }
    public void AddScore(int pluse)
    {
       Score=Score + pluse;
        GameObject.Find("Number").GetComponent<Text>().text = Score.ToString();
    }
    public void AddTime(int T)
    {
        timeLeft = timeLeft + T;
        GameObject.Find("Number").GetComponent<Text>().text = Score.ToString();
    }

    public ParticleSystem p1;
    public void OpenGameOverScreen()
    {

        mGameOverScreen.SetActive(true);
        p1.gameObject.SetActive(true);
        // GameManager.mGameManager.SetHighScore(Score);

        GameObject.Find("SettingButton").GetComponent<Button>().enabled = false;      
    }

    public void SetHighscore()
    {
        GameObject.Find("HNumber").GetComponent<Text>().text = "     " + GameManager.mGameManager.GetHighScore().ToString();
    }

    public void ResetTimeLimit()
    {
        imageColldown.fillAmount = 1.0f;
    }
    public void UpdateShapesTimeLimit()
    {
        imageColldown.fillAmount -= 1.0f / cooldown * Time.deltaTime;
        if(imageColldown.fillAmount<=0)
        {
            TAnimationMagager.mTAnimation.ShapeMoveOut(TTouchManager.mTTouchManager.GetShapesIniatialized());
            TTouchManager.mTTouchManager.DeleteCurrentShape();
            imageColldown.fillAmount = 1.0f;

        }
    }

    public void UpdateShapeTry()
    {
        Moves.text = Moveleft.ToString();
        Moveleft -= 1;
        if(Moveleft<=0)
        {
            Moveleft = 0;
        }
    }
    int Multi;
    int Multi2;

    public void MultiplierDisplay()
    {
        // MultplierDisplay.text = WoboCombo.GetMultipliecr().ToString();
        Multi = WoboCombo.GetMultiplier();
        if (Multi2 < 0)
        {
            Multi2 = WoboCombo.GetMultiplier();
        }

        if (mArea.activeInHierarchy)
        {
            if (Multi != Multi2)
            {
                MuliteCountDown = 5.0f;
                Multi2 = Multi;
            }
            else
            {
                MuliteCountDown -= Time.deltaTime;
            }
            if (MuliteCountDown <= 0.0f)
            {
                WoboCombo.ResetCount();
                MuliteCountDown = 5.0f;
            }

        }


        if (Multi < 10)
        {
            if (Multi < 1)
            {
                mArea.SetActive(false);
            }
            else
            {
                mArea.SetActive(true);
                mSingle.gameObject.SetActive(true);
                mDouble1.gameObject.SetActive(false);
                mDouble2.gameObject.SetActive(false);
                mSingle.sprite = WhiteNumberPool[Multi];
            }

        }
        if (Multi > 9)
        {
            mSingle.gameObject.SetActive(false);
            mDouble1.gameObject.SetActive(true);
            mDouble2.gameObject.SetActive(true);
            MultiString = Multi.ToString();
            switch (MultiString[0])
            {
                case '0':
                    mDouble1.sprite = WhiteNumberPool[0];
                    break;
                case '1':
                    mDouble1.sprite = WhiteNumberPool[1];
                    break;
                case '2':
                    mDouble1.sprite = WhiteNumberPool[2];
                    break;
                case '3':
                    mDouble1.sprite = WhiteNumberPool[3];
                    break;
                case '4':
                    mDouble1.sprite = WhiteNumberPool[4];
                    break;
                case '5':
                    mDouble1.sprite = WhiteNumberPool[5];
                    break;
                case '6':
                    mDouble1.sprite = WhiteNumberPool[6];
                    break;
                case '7':
                    mDouble1.sprite = WhiteNumberPool[7];
                    break;
                case '8':
                    mDouble1.sprite = WhiteNumberPool[8];
                    break;
                case '9':
                    mDouble1.sprite = WhiteNumberPool[9];
                    break;
            }
            switch (MultiString[1])
            {
                case '0':
                    mDouble2.sprite = WhiteNumberPool[0];
                    break;
                case '1':
                    mDouble2.sprite = WhiteNumberPool[1];
                    break;
                case '2':
                    mDouble2.sprite = WhiteNumberPool[2];
                    break;
                case '3':
                    mDouble2.sprite = WhiteNumberPool[3];
                    break;
                case '4':
                    mDouble2.sprite = WhiteNumberPool[4];
                    break;
                case '5':
                    mDouble2.sprite = WhiteNumberPool[5];
                    break;
                case '6':
                    mDouble2.sprite = WhiteNumberPool[6];
                    break;
                case '7':
                    mDouble2.sprite = WhiteNumberPool[7];
                    break;
                case '8':
                    mDouble2.sprite = WhiteNumberPool[8];
                    break;
                case '9':
                    mDouble2.sprite = WhiteNumberPool[9];
                    break;
            }

        }

    }
   public void BuyTime()
    {
        if (Score >= 500)
        { 
            AddTime(5);
            Score -= 500;
        }
    }

    void Update()
    {
        // UpdateShapesTimeLimit();
        MultiplierDisplay();

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

   //     else
   //     {        
			//OpenGameOverScreen();
   //     }
   

    }
}
