using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Debug.Log("PointsManeger!!!!!!!");

        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PointsManager.mPointsManager.points[1][1].transform.position.x);
        //for(int i = 0; i < PointsManager.mPointsManager.points.Count; ++i)
           // PointsManager.mPointsManager.points[0][i].SetActive(false);
    }
}
