using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Shapes : MonoBehaviour {

    public int points;
    public int timeBonus;
    private TouchLogic.Shapes shapeType;


    private void Start()
    {
        shapeType = (TouchLogic.Shapes)Enum.Parse(typeof(TouchLogic.Shapes), this.transform.name.ToString().Replace("(Clone)",""));
    }

    public TouchLogic.Shapes GetShpeType()
    {
        return shapeType; 
    }


}
