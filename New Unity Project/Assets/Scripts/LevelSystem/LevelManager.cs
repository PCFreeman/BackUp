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
        currentLevel = currentLevel = Instantiate(mLevels[mLevelIndex], new Vector3(0.0f, 0.0f, -20.0f), Quaternion.identity);
        
        mNumOfShapesTry = (int)currentLevel.GetComponent<Level>().MaxShapesTry;
        mNumShapesToNext = (int)currentLevel.GetComponent<Level>().ShapesToNext;
    }
	
	
    public void DecreaseShapesTry()
    {
        if(mNumOfShapesTry > 1)
        {
            mNumOfShapesTry--;
        }
        else
        {
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


            Destroy(currentLevel);
            mLevelIndex++;
            currentLevel = Instantiate(mLevels[mLevelIndex], new Vector3(0.0f, 0.0f, -20.0f), Quaternion.identity);
        }
    }


}
