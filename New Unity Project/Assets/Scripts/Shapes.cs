using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Shapes : MonoBehaviour {

    public int points;
    public int timeBonus;
    private TouchLogic.Shapes shapeType;

    public float timeLimit;
    public float minTimeLimit;
    public float maxTimeLimit;

    public int probability;


    private void Start()
    {
        shapeType = (TouchLogic.Shapes)Enum.Parse(typeof(TouchLogic.Shapes), this.transform.name.ToString().Replace("(Clone)",""));
    }

    public TouchLogic.Shapes GetShpeType()
    {
        return shapeType; 
    }

    public void DecrementTimeLimit(float t)
    {
        if((timeLimit - t) >= minTimeLimit)
        {
            timeLimit -= t;
        }
        else
        {
            timeLimit = minTimeLimit;
        }
    }

    public void IncrementTimeLimit(float t)
    {
        if ((timeLimit + t) <= maxTimeLimit)
        {
            timeLimit += t;
        }
        else
        {
            timeLimit = maxTimeLimit;
        }
    }

}
