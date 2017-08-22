using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratingPoints : MonoBehaviour
{

    [SerializeField]
    private GameObject circlePrefab;
    [SerializeField]
    private Transform circlePrefabTransform;
    [SerializeField]
    private float mNumPointsEachAxis;
    [SerializeField]
    private float xAxisSpace = 1;
    [SerializeField]
    private float yAxisSpace = 1;
    [SerializeField]
    private float topLeftCircleX = 458.81f;
    [SerializeField]
    private float topLeftCircleY = 278.97f;
    [SerializeField]
    private Vector3 circleScale = new Vector3(1.0f, 1.0f, 1.0f);

    private GameObject[,] pointsArray;

    //private GameObject[] Points;

    // Use this for initialization
    void Start()
    {
        circlePrefabTransform.localScale = circleScale;
        float x = 0;
        float y = 0;
        for (int i = 0; i < mNumPointsEachAxis; i++)
        {
            x += xAxisSpace;
            y = 0;
            for (int j = 0; j < mNumPointsEachAxis; j++)
            {
                pointsArray[i, j] = (GameObject)Instantiate(circlePrefab, new Vector3(topLeftCircleX + x, topLeftCircleY + y, -1), Quaternion.identity);
                y -= yAxisSpace;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

