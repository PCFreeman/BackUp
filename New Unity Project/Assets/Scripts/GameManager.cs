using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager mGameManager = null;

    private void Awake()
    {

        //Check if instance already exist
        if (mGameManager == null)
        {
            //if not, set instance to this
            mGameManager = this;
        }
        else if (mGameManager != this) //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a TouchManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}





    //=====================================================================
    //============================  Functions   ===========================
    //=====================================================================

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("highscore", 0);
    }

    public void SetHighScore(int score)
    {
        if(score > PlayerPrefs.GetInt("highscore", 0))
        { 
        PlayerPrefs.SetInt("highscore", score);
        }
    }


}
