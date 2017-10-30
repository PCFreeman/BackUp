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
    public Button button;

    //Peter's Code cliksound
    //private AudioController mAudio;
    void Start()
	{
        //Peter's Code cliksound
        //mAudio = GameObject.Find ("SFX").GetComponent<AudioController> ();
        MusicCheck = true;



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
        SceneManager.LoadScene(2);
    }
    public void PlayTimed()
    {
        SceneManager.LoadScene(3);
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
    public void GotoTutorial()
    {
        SceneManager.LoadScene(4);
    }
    ////Peter's code play SFX
    public void PlaySFX()
    {

        //AudioController.sInstance.ClickSFX();
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
