using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{

    public Image mFirstDigit0;
    public Image mSecondDigit0;
    public Image mFirstDigit1;
    public Image mSecondDigit1;
    public Sprite[] mSpritePool;
    public GameObject[] mLevelImagePool;
    uint passing = 19;

    // Use this for initialization
    void Start()
    {
        mFirstDigit0 = mFirstDigit0.GetComponent<Image>();
        mSecondDigit0 = mSecondDigit0.GetComponent<Image>();
        mFirstDigit1 = mFirstDigit1.GetComponent<Image>();
        mSecondDigit1 = mSecondDigit1.GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.B))
        //    Debug.Log(((passing / 10) % 10) + 1);
        LevelMovement(ref passing);
    }

    void LevelMovement(ref uint lvl)
    {
        string mLvlNumber = lvl.ToString();
        if(mLvlNumber.Length == 1)
        {
            mFirstDigit0.sprite = mSpritePool[0];
            mSecondDigit0.sprite = mSpritePool[lvl];
            if(lvl == 9)
            {
                mFirstDigit1.sprite = mSpritePool[1];
                mSecondDigit1.sprite = mSpritePool[0];
            }
        }
        if(mLvlNumber.Length == 2)
        {
            switch (mLvlNumber[0])
            {
                case '1':
                    mFirstDigit0.sprite = mSpritePool[1];
                    mFirstDigit1.sprite = mSpritePool[1];
                    break;
                case '2':
                    mFirstDigit0.sprite = mSpritePool[2];
                    mFirstDigit1.sprite = mSpritePool[1];
                    break;
                case '3':
                    mFirstDigit0.sprite = mSpritePool[3];
                    mFirstDigit1.sprite = mSpritePool[1];
                    break;
                case '4':
                    mFirstDigit0.sprite = mSpritePool[4];
                    mFirstDigit1.sprite = mSpritePool[1];
                    break;
                case '5':
                    mFirstDigit0.sprite = mSpritePool[5];
                    mFirstDigit1.sprite = mSpritePool[1];
                    break;
                case '6':
                    mFirstDigit0.sprite = mSpritePool[6];
                    mFirstDigit1.sprite = mSpritePool[1];
                    break;
                case '7':
                    mFirstDigit0.sprite = mSpritePool[7];
                    mFirstDigit1.sprite = mSpritePool[1];
                    break;
                case '8':
                    mFirstDigit0.sprite = mSpritePool[8];
                    mFirstDigit1.sprite = mSpritePool[1];
                    break;
                case '9':
                    mFirstDigit0.sprite = mSpritePool[9];
                    break;
            }
            switch (mLvlNumber[1])
            {
                case '0':
                    mSecondDigit0.sprite = mSpritePool[0];
                    mSecondDigit1.sprite = mSpritePool[1];
                    break;
                case '1':
                    mSecondDigit0.sprite = mSpritePool[1];
                    mSecondDigit1.sprite = mSpritePool[2];
                    break;
                case '2':
                    mSecondDigit0.sprite = mSpritePool[2];
                    mSecondDigit1.sprite = mSpritePool[3];
                    break;
                case '3':
                    mSecondDigit0.sprite = mSpritePool[3];
                    mSecondDigit1.sprite = mSpritePool[4];
                    break;
                case '4':
                    mSecondDigit0.sprite = mSpritePool[4];
                    mSecondDigit1.sprite = mSpritePool[5];
                    break;
                case '5':
                    mSecondDigit0.sprite = mSpritePool[5];
                    mSecondDigit1.sprite = mSpritePool[6];
                    break;
                case '6':
                    mSecondDigit0.sprite = mSpritePool[6];
                    mSecondDigit1.sprite = mSpritePool[7];
                    break;
                case '7':
                    mSecondDigit0.sprite = mSpritePool[7];
                    mSecondDigit1.sprite = mSpritePool[8];
                    break;
                case '8':
                    mSecondDigit0.sprite = mSpritePool[8];
                    mSecondDigit1.sprite = mSpritePool[9];
                    break;
                case '9':
                    mSecondDigit0.sprite = mSpritePool[9];
                    mFirstDigit1.sprite = mSpritePool[((lvl / 10) % 10) + 1];
                    mSecondDigit1.sprite = mSpritePool[0];
                    break;
            }
        }
    }
}
