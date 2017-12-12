using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseSystem : MonoBehaviour
{

    private UIManage mUIManager;
    public int TimeCost = 500;
    public int ChanceCost = 500;
    public int TimeGain = 5;
    public int ChanceGain = 2;
    public int timeCostIncrease = 100;
    public int chanceCostIncrease = 100;
    // Use this for initialization
    void Start()
    {
        mUIManager = GameObject.Find("Canvas").GetComponent<UIManage>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PurchaseTime()
    {
        if(mUIManager.Score >= 500)
        {
            UIManage.instance.AddTime(TimeGain);
            mUIManager.Score -= 500;
        }
    }

    public void PurchaseChance()
    {
        if (mUIManager.Score >= 500)
        {
            LevelManager.mLevelManager.AddNumTry(ChanceGain);
            mUIManager.Score -= 500;
        }
    }

    public void IncreaseTimeCost()
    {
        TimeCost += timeCostIncrease;
    }

    public void IncreaseChanceCost()
    {
        ChanceCost += chanceCostIncrease;
    }
}
