using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    public GameObject mHand;
    private float mOriginPos;
    private float mCurrentPos;
    // Use this for initialization
    void Start()
    {
        mHand.SetActive(true);
        mOriginPos = mHand.transform.position.y;
        //mHand.SetActive(false);
    }

    void HandMove()
    {
        mCurrentPos = mHand.transform.position.y;
        if (mOriginPos - mCurrentPos >= 200.0f)
        {
            mHand.transform.position = new Vector3(-45.0f, 233.0f, -1.0f);
        }
        mHand.transform.Translate(0.0f, -1.5f, 0.0f);
    }
    // Update is called once per frame
    void Update()
    {
        HandMove();
        if(Input.GetKeyDown(KeyCode.B))
        {
            mHand.SetActive(false);
        }
    }
}
