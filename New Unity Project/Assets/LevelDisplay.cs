using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{

    public RectTransform mRectTrans;
    public Image mFirstDigit0;
    public Image mSecondDigit0;
    public Sprite[] mSpritePool;
    public uint passing = 1;

    private Vector3 mStartPos;
    private Vector3 mEndPos;
    private float mMovingDistance = 500.0f;
    private float speed;

    // Use this for initialization
    void Start()
    {
        mRectTrans = GetComponent<RectTransform>();
        mFirstDigit0 = mFirstDigit0.GetComponent<Image>();
        mSecondDigit0 = mSecondDigit0.GetComponent<Image>();

        mStartPos = mRectTrans.position;
        mEndPos = new Vector3(565.0f, -214.0f, 0.0f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Start Pos: " + mStartPos);
            Debug.Log("End Pos: " + mEndPos);
            Debug.Log(Vector3.Distance(mStartPos, mEndPos));
            LevelMovement(ref passing);
        }
    }

    public void LevelMovement(ref uint lvl)
    {
        string mLvlNumber = lvl.ToString();

        //TextMovement
        StartCoroutine(MovingClock());

        //change uint to string
        //get check how many digits and get each digit then assign correct sprit
        if (mLvlNumber.Length == 1)
        {
            mFirstDigit0.sprite = mSpritePool[0];
            mSecondDigit0.sprite = mSpritePool[lvl];
        }
        if (mLvlNumber.Length == 2)
        {
            switch (mLvlNumber[0])
            {
                case '1':
                    mFirstDigit0.sprite = mSpritePool[1];
                    break;
                case '2':
                    mFirstDigit0.sprite = mSpritePool[2];
                    break;
                case '3':
                    mFirstDigit0.sprite = mSpritePool[3];
                    break;
                case '4':
                    mFirstDigit0.sprite = mSpritePool[4];
                    break;
                case '5':
                    mFirstDigit0.sprite = mSpritePool[5];
                    break;
                case '6':
                    mFirstDigit0.sprite = mSpritePool[6];
                    break;
                case '7':
                    mFirstDigit0.sprite = mSpritePool[7];
                    break;
                case '8':
                    mFirstDigit0.sprite = mSpritePool[8];
                    break;
                case '9':
                    mFirstDigit0.sprite = mSpritePool[9];
                    break;
            }
            switch (mLvlNumber[1])
            {
                case '0':
                    mSecondDigit0.sprite = mSpritePool[0];
                    break;
                case '1':
                    mSecondDigit0.sprite = mSpritePool[1];
                    break;
                case '2':
                    mSecondDigit0.sprite = mSpritePool[2];
                    break;
                case '3':
                    mSecondDigit0.sprite = mSpritePool[3];
                    break;
                case '4':
                    mSecondDigit0.sprite = mSpritePool[4];
                    break;
                case '5':
                    mSecondDigit0.sprite = mSpritePool[5];
                    break;
                case '6':
                    mSecondDigit0.sprite = mSpritePool[6];
                    break;
                case '7':
                    mSecondDigit0.sprite = mSpritePool[7];
                    break;
                case '8':
                    mSecondDigit0.sprite = mSpritePool[8];
                    break;
                case '9':
                    mSecondDigit0.sprite = mSpritePool[9];
                    break;
            }
        }
    }
    IEnumerator MovingClock()
    {
        //TextMovement
        //float step = speed * Time.deltaTime;
        //mRectTrans.transform.position = Vector3.MoveTowards(mStartPos, mEndPos, step);
        while (mRectTrans.position.x <= mEndPos.x)
        {
            mRectTrans.Translate(new Vector3(8.0f, 0.0f, 0.0f));
            yield return null;
        }
        mRectTrans.position = mStartPos;

    }
}
