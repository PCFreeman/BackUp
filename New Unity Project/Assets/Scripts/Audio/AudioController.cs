using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioSource SFX;
    public MainBGM mBGM;
    //public AudioSource mBGM;
    public AudioClip[] mCurrentSFXClip;

    public static AudioController sInstance;

    void Awake()
    {
        SFX = GetComponent<AudioSource>();
        mBGM = GameObject.Find("MainBackgroundMusic").GetComponent<MainBGM>();
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

    public void MuteMainBGM()
    {
        mBGM.mainBGM.mute = !mBGM.mainBGM.mute;
    }
}
