using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager mLevelManager = null;

    private GameObject currentLevel;
    private int mLevelIndex;

    //Level Array

    public List<GameObject> mLevels;

    //Level variables
    private int mNumOfShapesTry;
    private int mNumShapesToNext;


    //Functions


    private void Awake()
    {
        //Check if instance already exist
        if (mLevelManager == null)
        {
            //if not, set instance to this
            mLevelManager = this;
        }
        else if (mLevelManager != this) //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a LevelManager.
            Destroy(gameObject);
        }
    }



    // Use this for initialization
    void Start ()
    {
        mLevelIndex = 0;
        //currentLevel = (GameObject)Instantiate(mLevels[mLevelIndex], new Vector3(0.0f, 0.0f, -20.0f), Quaternion.identity);
        
        mNumOfShapesTry = (int)currentLevel.GetComponent<Level>().MaxShapesTry;
        mNumShapesToNext = (int)currentLevel.GetComponent<Level>().ShapesToNext;
    }
	
	
    public void DecreaseShapesTry()
    {
        if(mNumOfShapesTry >= 1)
        {
            mNumOfShapesTry--;
        }
        else
        {
            Debug.Assert(false, "Game Over!");
            //Game Over;
        }        
    }

    public void DecreaseShapesToNext()
    {
        if (mNumShapesToNext > 1)
        {
            mNumShapesToNext--;
        }
        else
        {
            //Call Animation
            Debug.Log("Next Level");

            Destroy(currentLevel);
            mLevelIndex++;
            if(mLevelIndex != mLevels.Count)
            {
                currentLevel = Instantiate(mLevels[mLevelIndex], new Vector3(0.0f, 0.0f, -20.0f), Quaternion.identity);
            }
            else
            {
                Debug.Assert(false);
            }
            mNumShapesToNext = (int)currentLevel.GetComponent<Level>().ShapesToNext;
            mNumOfShapesTry = (int)currentLevel.GetComponent<Level>().MaxShapesTry;
            TouchManager.mTouchManager.GenerateShapesList();
        }
    }


    //Get Functions

    public GameObject GetCurrentLevel()
    {
        if(currentLevel == null)
        {
            currentLevel = (GameObject)Instantiate(mLevels[mLevelIndex], new Vector3(0.0f, 0.0f, -20.0f), Quaternion.identity);
        }
        return currentLevel;
    }

    public int GetCurrentLevelIndex()
    {
        return mLevelIndex + 1;
    }

    public int GetToNext()
    {
        return mNumShapesToNext;
    }

    public int GetNumTry()
    {
        return mNumOfShapesTry;
    }
}
