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

    public float DecrementTimeLimit(float totalReduceTime)
    {
        if((timeLimit - totalReduceTime) >= minTimeLimit)
        {
            return (timeLimit - totalReduceTime);
        }
        else
        {
            return minTimeLimit;
        }
    }

    public float IncrementTimeLimit(float totalIncrementTime)
    {
        if ((timeLimit + totalIncrementTime) <= maxTimeLimit)
        {
            return (timeLimit + totalIncrementTime);
        }
        else
        {
            return maxTimeLimit;
        }
    }

}
