using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            UIManage.instance.OpenGameOverScreenMoves();
            Debug.Assert(false, "Game Over!");

            //Game Over;
        }        
    }

    public void DecreaseShapesToNext()
    {


        if (mNumShapesToNext > 1)
        {

            mNumShapesToNext--;
            TouchManager.mTouchManager.DeleteCurrentShape(false); //Delete current shape and Instantiate a new one
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

            // ref here is bullshit! no need to use it is just an UINT that is never changed inside function
            TouchManager.mTouchManager.mLevelAnimation.GetComponent<LevelDisplay>().LevelMovement(ref currentLevel.GetComponent<Level>().levelIndex); 

            TouchManager.mTouchManager.DeleteCurrentShape(true); //Delete current shape and Instantiate a new one
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

    
    public void UpdateNextLevel()
    {
        GameObject.Find("NextLevel").GetComponent<Text>().text = mNumShapesToNext.ToString();
    }

    public void UpdateShapesTry()
    {
        GameObject.Find("shapesTry").GetComponent<Text>().text = mNumOfShapesTry.ToString();
    }

    public void UpdateShapesTimeLimit(float timeLimit)
    {
        GameObject.Find("ShapeTLimit").GetComponent<Text>().text = Mathf.FloorToInt(timeLimit % 60f).ToString();
    }
    

}
