using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMovement : MonoBehaviour
{

    private GameObject[] linePoints;
    private PointsManager pointGenerationScript;
    [SerializeField]
    private float yDistance = 5.0f;
    private Vector3 moveUpDistance;

    [SerializeField]
    private float xDistance = 5.0f;
    private Vector3 moveSideDistance;

    [SerializeField]
    private float moveEveryXSeconds = 5.0f;
    private bool isInitialize = false;
    private float tempTime= 0.0f;

    [SerializeField]
    bool isMoving = true;
    // Use this for initialization
    void Start()
    {
        Debug.Log("[PointsMovement] Hi PointsMovement!");
        moveUpDistance = new Vector3(0.0f, yDistance, 0.0f);
        moveSideDistance = new Vector3(xDistance, 0.0f, 0.0f);
        pointGenerationScript = GameObject.Find("GeneratePoints").GetComponent<PointsManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isInitialize)
        {
            linePoints = GameObject.FindGameObjectsWithTag("PointsLines");
            isInitialize = true;
        }

        tempTime += Time.deltaTime;
        if (tempTime > moveEveryXSeconds && isMoving)
        {
            tempTime = 0.0f;
            PointsMovingUp();
            //PointsMovingSide();
        }
    }

    private void PointsMovingUp()
    {
        Debug.Log("PointsMoveingUp HI");
        for (int i = 0; i < pointGenerationScript.numberLines; ++i)
        {
            linePoints[i].transform.position += moveUpDistance;
        }
    }

    private void PointsMovingSide()
    {
        Debug.Log("PointsMoveingUp HI");
        for (int i = 0; i < pointGenerationScript.numberLines; ++i)
        {
            linePoints[i].transform.position += moveSideDistance;
        }
    }

}
