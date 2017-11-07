using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Firstscreen : MonoBehaviour {

    public GameObject LogininMenu;
    public GameObject Mainmenu;
    public GameObject MenuButton;
    public GameObject EndlessWindow;
    public GameObject TimedWindow;
    public GameObject ChallengeWindow;
    public GameObject Setting;
    public GameObject setbutton;
    public GameObject Mode;
    public GameObject Question;
    private bool MusicCheck;
    public Sprite turnoff;
    public Sprite turnon;
    public Sprite turnoffSound;
    public Sprite turnonSound;
    public Button button;
    public Button Soundbutton;
    private bool SoundCheck;

    //Peter's Code cliksound
    //private AudioController mAudio;

        void Start()
	{
        //Peter's Code cliksound
        //mAudio = GameObject.Find ("SFX").GetComponent<AudioController> ();
        MusicCheck = true;
        SoundCheck = true;
    }

    public void ModBack()
    {
        PlaySFX();
        Mode.SetActive(false);
        MenuButton.SetActive(true);

    }
    public void Tutorial()
    {
        PlaySFX();
        Question.SetActive(true);
        Setting.SetActive(false);
        GameObject.Find("SettingButton").SetActive(false);
    }

    public void TutorialBack()
    {
        PlaySFX();
        Question.SetActive(false);
        setbutton.SetActive(true);
        Setting.SetActive(true);

    }
    public void ModMenu()
    {
        PlaySFX();
        MenuButton.SetActive(false);
        Mode.SetActive(true);
    }
    public void Endless()
    {
        PlaySFX();
        Mode.SetActive(false);
        EndlessWindow.SetActive(true);

    }//Those are the button in main menu
    public void Timed()
    {
        PlaySFX();
        Mode.SetActive(false);
        TimedWindow.SetActive(true);
    }
    public void Challenge()
    {
        PlaySFX();
        Mode.SetActive(false);
        ChallengeWindow.SetActive(true);

    }
    public void SoundSwitch()
    {
        if (SoundCheck == true)
        {
            Soundbutton.image.overrideSprite = turnonSound;
            SoundCheck = false;
            AudioController.sInstance.MuteSFX();
        }
        else
        {
            Soundbutton.image.overrideSprite = turnoffSound;
            AudioController.sInstance.MuteSFX();
            SoundCheck = true;
        }
    }

    public void CloseButton()
    {
           PlaySFX();
           EndlessWindow.SetActive(false);
           TimedWindow.SetActive(false);
           ChallengeWindow.SetActive(false);
           Mode.SetActive(true);
    }
    public void PlayEndless()
    {

        //StartCoroutine(LoadingManager.instance.LoadingScreen(3));
    }
    public void PlayTimed()
    {
        StartCoroutine(LoadingManager.instance.LoadingScreen(3));
    }
    public void GotoTutorial()
    {
       StartCoroutine(LoadingManager.instance.LoadingScreen(2));
    }
    public void PlayChallenge()
    {
        SceneManager.LoadScene(1);
    }
    public void MusicSwitch()
    {
        if(MusicCheck==true)
        {
            button.image.overrideSprite = turnoff;
            MusicCheck = false;
            AudioController.sInstance.MuteMainBGM();
            

        }
        else
        {
            button.image.overrideSprite = turnon;
            AudioController.sInstance.MuteMainBGM();
            MusicCheck = true;
        }
      
    }
    ////Peter's code play SFX
    public void PlaySFX()
    {

        AudioController.sInstance.ClickSFX();
    }

    public void SettingMenu()
    {
        if (!Setting.activeInHierarchy)
        {
            MenuButton.SetActive(false);
            Mode.SetActive(false);
            Setting.SetActive(true);
        }
        else if(Setting.activeInHierarchy)
        {
            Setting.SetActive(false);
            MenuButton.SetActive(true);
        }
       
    }
    void Update () {
        if (LogininMenu.activeInHierarchy)
        { 
            if (Input.GetMouseButtonDown(0))
            {
                LogininMenu.SetActive(false);
                Mainmenu.SetActive(true);
            }
         }


    }

}
// Update is called once per frame
