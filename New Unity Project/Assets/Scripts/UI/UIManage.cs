using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManage : MonoBehaviour
{

    public static UIManage instance;
    public static PurchaseSystem sinstance;
    public static ComboSystem WoboCombo;
    public float timeLeft;
    public int TimeGain = 5;
    public int ChanceGain = 2;
    public int Score;
    //-------------------------------------------------
    public Text ScoreShowedInGameOver;
    public Text ScoreWhenleft;
    public Text HighScoreWhenleft;
    public Text Timer;
    public Text FhScore;
    public Text FScore;
    public GameObject Left;
    public GameObject Set;
    public GameObject mGameOverScreen;
    public GameObject mG1;
    public GameObject mG2;

    public Sprite turnoff;  //working
    public Sprite turnon;   //working
    public Button button;
    private bool MusicCheck;
    float Mins;
    float Secs;
    public ParticleSystem particle;
    public Sprite turnoffSound; //working
    public Sprite turnonSound;  //working
    public Button Soundbutton;
    private bool SoundCheck;
    public GameObject Touch;    //working
    public Text TryLimit;
    public Text nextLevel;
    public Text ShapeTimeLimit;
    int current;
    string ShapeTryString;
    public GameObject GrounpOfShapeTryDisplay;
    public Image Single;
    public Image Double1;
    public Image Double2;
    public Image imageColldown;
    float cooldown = -1.0f;
    GameObject PointsArea;
    GameObject SelectPointArea;
    private int Multi;
    private int Multi2;
    string MultiString;
    public Image mSingle;
    public Image mDouble1;
    public Image mDouble2;
    public GameObject mArea;
    public GameObject ScoreImage;
    public Image MoveImage;
    public GameObject ImageforTime;
    public Text TimeCostDisplay;
    public Text MovesCostDisplay;
    //--------------------------------------------------------

    private float countdown;
    private float MuliteCountDown;
    private Sprite CurrentShapeImage;
    public Sprite[] WhiteNumberPool;
    public Sprite[] NumberPool;
    public Sprite[] UpTriangle;
    public Sprite[] DownTriangle;
    public Sprite[] LeftTriangle;
    public Sprite[] RightTriangle;
    public Sprite[] TriangleRectangleDownRight;
    public Sprite[] TriangleRectangleDownLeft;
    public Sprite[] TriangleRectangleUpRight;
    public Sprite[] TriangleRectangleUpLeft;
    public Sprite[] LUR;
    public Sprite[] LUL;
    public Sprite[] LDL;
    public Sprite[] LDR;
    public Sprite[] TrapUp;
    public Sprite[] TrapDown;
    public Sprite[] Dimon;
    public Sprite[] Square;
    public Sprite[] Rec1;
    public Sprite[] Rec2;
    public Sprite[] Ocgaton;
    public Sprite[] CDBackGroundPool;
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
        Score = 5000;

        GameObject.Find("Number").GetComponent<Text>().text = Score.ToString();
    }

    private void Start()
    {
        MusicCheck = true;
        SoundCheck = true;
        TryLimit.gameObject.SetActive(false);
        current = -1;
        WoboCombo = GameObject.Find("WoboComboManager").GetComponent<ComboSystem>();
        sinstance = GameObject.Find("PurchaseManager").GetComponent<PurchaseSystem>();
        PointsArea = GameObject.Find("Points Area");
        SelectPointArea = GameObject.Find("Selected Points Area");
        MuliteCountDown = 5.0f;
        TimeCostDisplay.gameObject.SetActive(false);
        MovesCostDisplay.gameObject.SetActive(false);

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


    public void ChangeCDimage()
    {
        imageColldown.sprite = SearchForCDBackGround();
    }
    public Sprite SearchForCDBackGround()
    {
        CurrentShapeImage = TouchManager.mTouchManager.GetCurrentShape().GetComponent<Image>().sprite;

        for (int tu = 0; tu < UpTriangle.Length; ++tu)
        {
            if (CurrentShapeImage == UpTriangle[tu])
            {
                return CDBackGroundPool[11];
            }
        }
        for (int td = 0; td < DownTriangle.Length; ++td)
        {
            if (CurrentShapeImage == DownTriangle[td])
            {
                return CDBackGroundPool[12];
            }
        }
        for (int tl = 0; tl < LeftTriangle.Length; ++tl)
        {
            if (CurrentShapeImage == LeftTriangle[tl])
            {
                return CDBackGroundPool[13];
            }
        }
        for (int tr = 0; tr < RightTriangle.Length; ++tr)
        {
            if (CurrentShapeImage == RightTriangle[tr])
            {
                return CDBackGroundPool[14];
            }
        }
        for (int trdr = 0; trdr < TriangleRectangleDownRight.Length; ++trdr)
        {
            if (CurrentShapeImage == TriangleRectangleDownRight[trdr])
            {
                return CDBackGroundPool[8];
            }
        }
        for (int trdl = 0; trdl < TriangleRectangleDownLeft.Length; ++trdl)
        {
            if (CurrentShapeImage == TriangleRectangleDownLeft[trdl])
            {
                return CDBackGroundPool[7];
            }
        }
        for (int trur = 0; trur < TriangleRectangleUpRight.Length; ++trur)
        {
            if (CurrentShapeImage == TriangleRectangleUpRight[trur])
            {
                return CDBackGroundPool[6];
            }
        }
        for (int trul = 0; trul < TriangleRectangleUpLeft.Length; ++trul)
        {
            if (CurrentShapeImage == TriangleRectangleUpLeft[trul])
            {
                return CDBackGroundPool[9];
            }
        }
        for (int trapup = 0; trapup < TrapUp.Length; ++trapup)
        {
            if (CurrentShapeImage == TrapUp[trapup])
            {
                return CDBackGroundPool[16];
            }
        }
        for (int trapdown = 0; trapdown < TrapDown.Length; ++trapdown)
        {
            if (CurrentShapeImage == TrapDown[trapdown])
            {
                return CDBackGroundPool[15];
            }
        }
        for (int lur = 0; lur < LUR.Length; ++lur)
        {
            if (CurrentShapeImage == LUR[lur])
            {
                return CDBackGroundPool[4];
            }
        }
        for (int lul = 0; lul < LUL.Length; ++lul)
        {
            if (CurrentShapeImage == LUL[lul])
            {
                return CDBackGroundPool[5];
            }
        }
        for (int ldl = 0; ldl < LDL.Length; ++ldl)
        {
            if (CurrentShapeImage == LDL[ldl])
            {
                return CDBackGroundPool[3];
            }
        }
        for (int d = 0; d < Dimon.Length; ++d)
        {
            if (CurrentShapeImage == Dimon[d])
            {
                return CDBackGroundPool[0];
            }
        }
        for (int s = 0; s < Square.Length; ++s)
        {
            if (CurrentShapeImage == Square[s])
            {
                return CDBackGroundPool[10];
            }
        }
        for (int r1 = 0; r1 < Rec1.Length; ++r1)
        {
            if (CurrentShapeImage == Rec1[r1])
            {
                return CDBackGroundPool[18];
            }
        }
        for (int r2 = 0; r2 < Rec2.Length; ++r2)
        {
            if (CurrentShapeImage == Rec2[r2])
            {
                return CDBackGroundPool[17];
            }
        }

        for(int oc=0;oc<Ocgaton.Length;++oc)
        {
            if(CurrentShapeImage==Ocgaton[oc])
            {
                return CDBackGroundPool[19];
            }
        }

        return null;
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
        PointsArea.SetActive(false);
        SelectPointArea.SetActive(false);
    }
    public void SettingMenuBack()
    {
        Set.SetActive(false);
        Time.timeScale = 1f;
        Touch.SetActive(true);
        PointsArea.SetActive(true);
        SelectPointArea.SetActive(true);
    }
    public void LeftGameThroughSettingMenu()
    {
        mGameOverScreen.SetActive(true);
        mG1.SetActive(false);
        Left.SetActive(true);
        ScoreWhenleft.text = Score.ToString();
        HighScoreWhenleft.text= GameManager.mGameManager.GetHighScore().ToString();
    }
    public void BacktoMainMenuButton()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadingManager.instance.LoadingScreen(0));

    }
    public void Retry()
    {
        StartCoroutine(LoadingManager.instance.LoadingScreen(3));
    }
    public void AddScore(int pluse)
    {
        Score = Score + pluse;
        GameObject.Find("Number").GetComponent<Text>().text = Score.ToString();
    }
    public void AddTime(int T)
    {
        timeLeft = timeLeft + T;
    }
    public void MultiplierDisplay()
    {
        // MultplierDisplay.text = WoboCombo.GetMultipliecr().ToString();
        Multi = WoboCombo.GetMultiplier();
        if(Multi2<0)
        {
        Multi2 = WoboCombo.GetMultiplier(); 
        }

        if (mArea.activeInHierarchy)
        {
            if (Multi!= Multi2)
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

    float BlinkTime = 0.2f;
    public void PurchaseTime()
    {
        sinstance.PurchaseTime();
        AudioController.sInstance.SuccessMoveSFX();
       TimeCostDisplay.gameObject.SetActive(true);
        InvokeRepeating("BlinkForTime", 0f, BlinkTime);
    }

    public void PurchasChance()
    { 
        sinstance.PurchaseChance();
        AudioController.sInstance.SuccessMoveSFX();
        MovesCostDisplay.gameObject.SetActive(true);
        InvokeRepeating("BlinkforMove", 0f, BlinkTime);

    }
    public void OpenGameOverScreen()
    {
        Time.timeScale = 0f;
        mGameOverScreen.SetActive(true);
        ScoreShowedInGameOver.text = Score.ToString();
        PointsArea.SetActive(false);
        SelectPointArea.SetActive(false);
    }

    void ShowScoreInGameOver(int s)
    {
        GameManager.mGameManager.SetHighScore(s);
        FScore.text = s.ToString();
        FhScore.text = GameManager.mGameManager.GetHighScore().ToString();
        Time.timeScale = 0f;
        GameObject.Find("SettingButton").GetComponent<Button>().enabled = false;
    }
    public void OpenGameOverScreenMoves()//----------------------------------------------------Rafel
    {
        mGameOverScreen.SetActive(true);
        mG1.SetActive(false);
        mG2.SetActive(true);
        ShowScoreInGameOver(Score);
        PointsArea.SetActive(false);
        SelectPointArea.SetActive(false);

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
            PicForShapesTry(n);
            current = n;
        }

    }

    public void PicForShapesTry(int m)
    {
        if (m < 10)
        {
            Double1.gameObject.SetActive(false);
            Double2.gameObject.SetActive(false);
            Single.gameObject.SetActive(true);
            Single.sprite = NumberPool[m];
        }
        else if (m > 9)
        {
            ShapeTryString = m.ToString();
            Single.gameObject.SetActive(false);
            Double1.gameObject.SetActive(true);
            Double2.gameObject.SetActive(true);
            switch (ShapeTryString[0])
            {
                case '1':
                    Double1.sprite = NumberPool[1];
                    break;
                case '2':
                    Double1.sprite = NumberPool[2];
                    break;
                case '3':
                    Double1.sprite = NumberPool[3];
                    break;
                case '4':
                    Double1.sprite = NumberPool[4];
                    break;
                case '5':
                    Double1.sprite = NumberPool[5];
                    break;
                case '6':
                    Double1.sprite = NumberPool[6];
                    break;
                case '7':
                    Double1.sprite = NumberPool[7];
                    break;
                case '8':
                    Double1.sprite = NumberPool[8];
                    break;
                case '9':
                    Double1.sprite = NumberPool[9];
                    break;
            }
            switch (ShapeTryString[1])
            {
                case '0':
                    Double2.sprite = NumberPool[0];
                    break;
                case '1':
                    Double2.sprite = NumberPool[1];
                    break;
                case '2':
                    Double2.sprite = NumberPool[2];
                    break;
                case '3':
                    Double2.sprite = NumberPool[3];
                    break;
                case '4':
                    Double2.sprite = NumberPool[4];
                    break;
                case '5':
                    Double2.sprite = NumberPool[5];
                    break;
                case '6':
                    Double2.sprite = NumberPool[6];
                    break;
                case '7':
                    Double2.sprite = NumberPool[7];
                    break;
                case '8':
                    Double2.sprite = NumberPool[8];
                    break;
                case '9':
                    Double2.sprite = NumberPool[9];
                    break;
            }
        }

    }
    //public void DeactiveShapesTry()
    //{
    //    GrounpOfShapeTryDisplay.gameObject.SetActive(false);
    //}

    public void UpdateShapesTimeLimit(float timeLimit)//----------------------------------------------------Rafel
    {
        ShapeTimeLimit.text = Mathf.FloorToInt(timeLimit % 60f).ToString();
        if (cooldown < 0)
        {
            cooldown = Mathf.FloorToInt(timeLimit % 60f);
        }
        if (timeLimit >= countdown)
        {
            cooldown = Mathf.FloorToInt(timeLimit % 60f);
            imageColldown.fillAmount = 1;
        }

        if (timeLimit < cooldown)
        {
            imageColldown.fillAmount -= 1.0f / (timeLimit + (cooldown - timeLimit)) * Time.deltaTime;
        }

        if (imageColldown.fillAmount <= 0)
        {
            imageColldown.fillAmount = 1;
            cooldown = -1;
        }
        countdown = timeLimit;
    }

    //Use to find Inactive GameObject
    public static GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    public void ReAssignValue(string clonename)
    {
        GameObject newLevel = GameObject.Find(clonename);
        
        ScoreShowedInGameOver = FindObject(newLevel, "ScoreinGameOver").GetComponent<Text>();
        Timer = FindObject(newLevel, "Timer").GetComponent<Text>(); //-------------------
        FhScore = FindObject(newLevel, "HighScore").GetComponent<Text>();
        FScore = FindObject(newLevel, "Score").GetComponent<Text>();//-------------------

        Debug.Log("Break1");

        Set = FindObject(newLevel, "InGameSettingMenu").GetComponent<GameObject>(); //-------------------
        mGameOverScreen = FindObject(newLevel, "GameOverScreen").GetComponent<GameObject>();//-------------------
        mG1 = FindObject(newLevel, "GameOver").GetComponent<GameObject>();//-------------------
        mG2 = FindObject(newLevel, "G2").GetComponent<GameObject>();//------------------
        Debug.Log("Break2");
        button = FindObject(newLevel, "Button").GetComponent<Button>();
        particle = FindObject(newLevel, "Particle System").GetComponent<ParticleSystem>();
        Soundbutton = FindObject(newLevel, "Button (3)").GetComponent<Button>();
        Debug.Log("Break3");
        TryLimit = FindObject(newLevel, "shapesTry").GetComponent<Text>();
        nextLevel = FindObject(newLevel, "NextLevel").GetComponent<Text>();
        Debug.Log("Break4");
        ShapeTimeLimit = FindObject(newLevel, "ShapeTLimit").GetComponent<Text>();
        GrounpOfShapeTryDisplay = FindObject(newLevel, "GroupOfShapeTry").GetComponent<GameObject>();//-------------------
        Debug.Log("Break5");
        Single = FindObject(newLevel, "Single").GetComponent<Image>();
        Double1 = FindObject(newLevel, "Double1").GetComponent<Image>();
        Double2 = FindObject(newLevel, "Double2").GetComponent<Image>();
        imageColldown = FindObject(newLevel, "Cooldown").GetComponent<Image>();
        Debug.Log("Break6");
        mSingle = FindObject(newLevel, "mSingle").GetComponent<Image>();
        mDouble1 = FindObject(newLevel, "mDouble1").GetComponent<Image>(); 
        mDouble2 = FindObject(newLevel, "mDouble2").GetComponent<Image>();
        Debug.Log("Break7");
        mArea = FindObject(newLevel, "Multiplier").GetComponent<GameObject>();//-------------------
        Debug.Log("ReAssignFunctionCalled2! FINISHEDFINISHEDFINISHEDFINISHED");
    }

    int BlinkAmount=5;
    int BlinkCounter = 0;
   void BlinkforMove()
    {
        ScoreImage.GetComponent<RawImage>().color = Color.red;
        MoveImage.color = Color.green;
        ScoreImage.gameObject.SetActive(!ScoreImage.gameObject.activeSelf);
        if (BlinkCounter >= BlinkAmount)
        {
            CancelInvoke("BlinkforMove");
            ScoreImage.GetComponent<RawImage>().color = Color.white;
            MoveImage.color = Color.white;
            BlinkCounter = 0;
            ScoreImage.gameObject.SetActive(true);
            MovesCostDisplay.gameObject.SetActive(false);
        }

        ++BlinkCounter;
    }
    void BlinkForTime()
    {
        ScoreImage.GetComponent<RawImage>().color = Color.red;
        ImageforTime.GetComponent<Image>().color = Color.green;
        ScoreImage.gameObject.SetActive(!ScoreImage.gameObject.activeSelf);
        if (BlinkCounter >= BlinkAmount)
        {
            CancelInvoke("BlinkForTime");
            ScoreImage.GetComponent<RawImage>().color = Color.white;
            ImageforTime.GetComponent<Image>().color = Color.white;
            BlinkCounter = 0;
            ScoreImage.gameObject.SetActive(true);
            TimeCostDisplay.gameObject.SetActive(false);
        }

        ++BlinkCounter;
    }
    void Update()
    {
        ChangeCDimage();
        MultiplierDisplay();

        TimeCostDisplay.text ="-"+ sinstance.TimeCost.ToString();
        MovesCostDisplay.text = "-" + sinstance.ChanceCost.ToString();
        GameObject.Find("Number").GetComponent<Text>().text = Score.ToString();
        Mins = Mathf.FloorToInt(timeLeft / 60f);
        Secs = Mathf.FloorToInt(timeLeft % 60f);
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            Timer.text = " " + Mins + ":" + Secs;
            if (timeLeft < 10 && timeLeft > 9)
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
