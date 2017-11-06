using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioSource SFX;
    public AudioSource mBGM;
    public AudioSource eBGM;
    public AudioSource tBGM;
    //public AudioSource mBGM;
    public AudioClip[] mCurrentSFXClip;
    public static bool isMute = false;

    public static AudioController sInstance;

    void Awake()
    {
        SFX = GetComponent<AudioSource>();
        //mBGM = GameObject.Find("MainBackgroundMusic").GetComponent<MainBGM>();
        //eBGM = GameObject.Find("EndlessBGM").GetComponent<EndlessBGM>();
        //Check if instance already exist
        if (sInstance == null)
        {
            //if not, set instance to this
            sInstance = this;
        }
        else if (sInstance != this) //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a TouchManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public void ClickSFX()
    {
        SFX.clip = mCurrentSFXClip[0];
        SFX.Play();
    }

    public void SuccessMoveSFX()
    {
        SFX.clip = mCurrentSFXClip[1];
        SFX.Play();
    }
    public void GameOverSFX()
    {
        SFX.clip = mCurrentSFXClip[2];
        SFX.Play();
    }
    public void ErrorSFX()
    {
        SFX.clip = mCurrentSFXClip[3];
        SFX.Play();
    }
    public void LevelUpSFX()
    {
        SFX.clip = mCurrentSFXClip[4];
        SFX.Play();
    }

    public void TimeNearEnd()
    {
        SFX.clip = mCurrentSFXClip[5];
        SFX.Play();
    }
    public void MuteMainBGM()
    {

        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        if (scene.name == "Main")
        {
            mBGM = GameObject.Find("MainBackgroundMusic").GetComponent<AudioSource>();
            mBGM.mute = !mBGM.mute;
        }
    }

    public void MuteEndlessBGM()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        if (scene.name == "Endless")
        {
            eBGM = GameObject.Find("EndlessBGM").GetComponent<AudioSource>();
            eBGM.mute = !eBGM.mute;
        }
    }

    public void MuteTutorialBGM()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        if (scene.name == "Tutorial")
        {
            tBGM = GameObject.Find("TutorialSound").GetComponent<AudioSource>();
            tBGM.mute = !tBGM.mute;
        }
    }

    public void MuteSFX()
    {
        SFX.mute = !SFX.mute;
    }

    public bool GetMute()
    {
        return isMute;
    }

}