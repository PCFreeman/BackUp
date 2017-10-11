using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{

    private bool isPlaying = false;
    public AudioSource mBGM;
    public AudioSource mClickSFX;
    public AudioSource mSuccessMoveSFX;
    public AudioSource mGameOverSFX;
    public AudioClip[] mCurrentClip;
    private Scene mCurrentScene;

    public static AudioController sInstance;

    void Awake()
    {
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
    // Use this for initialization
    // Update is called once per frame
    void Update()
    {
        //mCurrentScene = SceneManager.GetActiveScene();
        //BGM();
    }

    public void ClickSFX()
    {
        mClickSFX.Play();
    }

    public void SuccessMoveSFX()
    {
        mSuccessMoveSFX.Play();
    }
    public void GameOverSFX()
    {
        mGameOverSFX.Play();
    }

    public void BGM()
    {
        //string sceneName = mCurrentScene.name;
        //Debug.Log (sceneName);
        //if (sceneName == "Main" && isPlaying == false)
        //{
        //	mBGM.clip = mCurrentClip [0];
        //	mBGM.Play ();
        //	isPlaying = true;
        //}

        /*if (sceneName == "Endless" && isPlaying == false)
		{
			mBGM.clip = mCurrentClip [1];
			mBGM.Play ();
			isPlaying = true;
		}
		*/
    }
}
