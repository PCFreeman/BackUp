using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject Step1;
    public GameObject Pointer1;
    public Vector3 EndLocationForStep1;
    Vector3 PreviousLocation1;
    public GameObject PointArea;
    public int Speed;
    float Timer;
	// Use this for initialization
	void Start () {
        Timer = 5;
        PointArea.SetActive(false);
        PreviousLocation1 = Step1.GetComponent<RectTransform>().position;
    }
	
	// Update is called once per frame
	void Update () {
        FirstStep();

    }

    public void FirstStep()
    {
        if(Step1.GetComponent<RectTransform>().position.y< EndLocationForStep1.y)
        {
            Step1.GetComponent<RectTransform>().Translate(0, Speed, 0);
        }
        else
        {
        Timer -= Time.deltaTime;
        }
        if(Timer<=0)
        {
            if( Step1.GetComponent<RectTransform>().position.y>PreviousLocation1.y)
            {
                Step1.GetComponent<RectTransform>().Translate(0, -Speed, 0);
            }
            PointArea.SetActive(true);
            Pointer1.SetActive(true);
        }
    }
}
