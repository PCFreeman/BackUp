using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TPointsManager : MonoBehaviour {

    public static TPointsManager mTPointsManager = null;

    public int numberLines;
    private int numberPointsInLine;
    public bool isMovingUp;
    public bool isMovingSides;

    public GameObject pointsAreaPrefab;
    public GameObject pointsLinesPrefab;
    public GameObject pointsPrefab;

    private GameObject pointsArea;
    private List<GameObject> pointsLines;       // This controls the lines structures, not content
    public List<List<GameObject>> points;      //for [x][y]   ,  each X will represent a line and each Y will represent a point in a line X

    private float ScreenXOffset;
    private float ScreenYOffset;

    private int baseScreenResolutionHeight = 540;
    private int pointsAreaHeightPadding = 0;
    private float areaOffset;
    private float squaredAreaSize;
    private float pointsAreaWidth;

    private float paddingLine = 10.5f;
    private float lineHeight;

    private int emptyLineAreaSize;

    private int sizePoint;

    private float startingXposition = -65;


    private void Awake()
    {

        if (mTPointsManager == null)
        {
            //if not, set instance to this
            mTPointsManager = this;
        }
        else if (mTPointsManager != this) //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a TouchManager.
            Destroy(gameObject);
        }
        

        isMovingUp = false;
        isMovingSides = false;

        //Set x offset
        ScreenXOffset = Screen.width / GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution.x;

        //Set y offset
        ScreenYOffset = Screen.height / GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution.y;
        Debug.Log("Y = " + ScreenYOffset.ToString() + "    X = " + ScreenXOffset.ToString());
    }
    

    // Use this for initialization
    void Start () {

        GeneratePointsGrid();
        Debug.Log("Teste");

    }
	
	// Update is called once per frame
	void Update () {
		



	}

    public int GetNumberOfHorizontalPoints()
    {
        return numberPointsInLine;
    }

    //Will genarate the Points Area
    private void GeneratePointsGrid()
    {
        numberPointsInLine = numberLines;
        
        //PointArea variables
        areaOffset = (GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height) / 540.0f;
        
        //Lines Variables
        lineHeight = (baseScreenResolutionHeight - pointsAreaHeightPadding) / numberLines;
 
        //Points Variables
        sizePoint = (int)(lineHeight * 0.7f);

        //Variables to set Points positions in a Line
        emptyLineAreaSize = ((int)((baseScreenResolutionHeight - 10.0) - (2 * paddingLine)) - (numberPointsInLine * sizePoint)) / (numberPointsInLine - 1);

        //Size if Area was squared
        squaredAreaSize = (baseScreenResolutionHeight - pointsAreaHeightPadding) * areaOffset;

        pointsAreaWidth = squaredAreaSize;

        CheckPossibilityOfNewColumn(emptyLineAreaSize + sizePoint);


        GeneratePointsArea();

        //Initialize the List that will hold PointLines
        pointsLines = new List<GameObject>();

        //Initialize the 2D List that will hold Points
        points = new List<List<GameObject>>();
        
        //Change size of points prefab
        pointsPrefab.transform.localScale = new Vector3(sizePoint, sizePoint, pointsPrefab.transform.localScale.z) ;



        GenerateLines();

        foreach(GameObject lines in pointsLines)
        {
            GeneratePoints(lines);
        }     
    }


    public float GetDistanceBetweenLinePoints()
    {
        return points[0][1].transform.position.x - points[0][0].transform.position.x;
    }

    public float GetDistanceBetweenLines()
    {
        //Debug.Log("P1  = " + points[2][0].transform.position.y.ToString() + points[2][0].transform.position.x.ToString());
        //Debug.Log("P2  = " + points[0][0].transform.position.y.ToString() + points[0][0].transform.position.x.ToString());

        return points[2][0].transform.position.y - points[0][0].transform.position.y;
    }

    private void checkPointsDistance(ref List<GameObject> pointsList, float distance)
    {
        float offset = 0.0f;

        for (int i = 1 ; i < pointsList.Count; ++i)
        {
            if ((pointsList[i].transform.position.x - pointsList[i - 1].transform.position.x) != distance)
            {
                if(((pointsList[i].transform.position.x + offset) - pointsList[i - 1].transform.position.x) != distance)
                { 
                    offset += distance - (pointsList[i].transform.position.x - pointsList[i - 1].transform.position.x);
                }

                float xPos = pointsList[i].transform.position.x;
                xPos += offset;

                pointsList[i].transform.position = new Vector3 (xPos,
                                                      pointsList[i].transform.position.y,
                                                      pointsList[i].transform.position.z);
            }

        }

        

    }


    private void GeneratePointsArea()
    {    

        //Instantiate Points container
        pointsArea = (GameObject)Instantiate(pointsAreaPrefab, new Vector3(Mathf.RoundToInt(startingXposition * ScreenXOffset), 0, -20), Quaternion.identity);

        pointsArea.name = "Points Area";

        pointsArea.GetComponent<BoxCollider>().size = new Vector3((pointsAreaWidth - pointsAreaHeightPadding) * areaOffset,
            (baseScreenResolutionHeight - pointsAreaHeightPadding) * areaOffset,
            pointsArea.GetComponent<BoxCollider>().size.z);
    }

    private void GenerateLines()
    {
        //Helper variables
        int check = 0;

        if (numberLines % 2 == 1)
        {
            //For Loop to instantiate Lines
            for (int i = 0; i < numberLines; ++i)
            {
                //Instantiate Line
                GameObject line = (GameObject)Instantiate(pointsLinesPrefab, new Vector3(startingXposition, 0, -20), Quaternion.identity);

                //Set it as child of pointsArea
                line.transform.parent = GameObject.Find("Points Area").transform;

                line.name = "Line " + i.ToString();

                //Set line height
                line.GetComponent<BoxCollider>().size = new Vector3((int)((pointsAreaWidth - 10.0) - (2 * paddingLine)), lineHeight, line.GetComponent<BoxCollider>().size.z);


                //Line Goes down
                if (i % 2 == 1)
                {
                    check = check + 1;

                    int yOffset = (int)(-(lineHeight * check));

                    line.transform.position = new Vector3(line.transform.position.x, line.transform.position.y + yOffset, line.transform.position.z);


                }
                else //Line goes up
                {
                    int yOffset = (int)(lineHeight * check);

                    line.transform.position = new Vector3(line.transform.position.x, line.transform.position.y + yOffset, line.transform.position.z);

                }

                pointsLines.Add(line);      //Line List
            }
        }
        else          // Even number of lines
        {
            //For Loop to instantiate Lines
            for (int i = 0; i < numberLines; ++i)
            {
                int lineCheck;

                if (i % 2 == 1)
                {
                    lineCheck = (int)((i - 1) * 0.5f);
                }
                else
                {
                    lineCheck = (int)(i * 0.5f);
                }


                //Instantiate Line
                GameObject line = (GameObject)Instantiate(pointsLinesPrefab, new Vector3(startingXposition, 0, -20), Quaternion.identity);

                //Set it as child of pointsArea
                line.transform.parent = GameObject.Find("Points Area").transform;

                line.name = "Line " + i.ToString();

                //Set line size
                line.GetComponent<BoxCollider>().size = new Vector3((int)((baseScreenResolutionHeight - 10.0) - (2 * paddingLine)), lineHeight, line.GetComponent<BoxCollider>().size.z);

                int yOffset = (int)((lineHeight * 0.5) + (lineHeight * lineCheck));


                if (i % 2 == 1)//Line goes down
                {
                    yOffset = -yOffset;
                }

                //Set line position
                line.transform.position = new Vector3(line.transform.position.x, line.transform.position.y + yOffset, line.transform.position.z);

                pointsLines.Add(line);      //Line List

            }

        }



    }

    private void GeneratePoints(GameObject line)
    {
        List<GameObject> pointsList = new List<GameObject>();

        //Helper Variable
        int checkPoints = 0;

        if (numberPointsInLine % 2 == 1)   //Odd number of points
        {
            for (int j = 0; j < numberPointsInLine; ++j)
            {

                //Instantiate Point
                GameObject pointTemp = (GameObject)Instantiate(pointsPrefab, new Vector3(startingXposition, 0, -20), Quaternion.identity);

                //Set it as child of Line
                pointTemp.transform.parent = line.transform;

                pointTemp.name = "Point " + j.ToString();



                if (j % 2 == 1)
                {
                    checkPoints = checkPoints + 1;

                    int xOffset = -((sizePoint + emptyLineAreaSize) * checkPoints);


                    pointTemp.transform.position = new Vector3(pointTemp.transform.position.x + xOffset, line.transform.position.y, pointTemp.transform.position.z);

                }
                else //Line goes up
                {
                    int xOffset = (sizePoint + emptyLineAreaSize) * checkPoints;

                    pointTemp.transform.position = new Vector3(pointTemp.transform.position.x + xOffset, line.transform.position.y, pointTemp.transform.position.z);

                }


                pointsList.Add(pointTemp);  //Temp list



            }
        }
        else  //Even number of points
        {
            for (int j = 0; j < numberPointsInLine; ++j)
            {


                if (j % 2 == 1)
                {
                    checkPoints = (int)((j - 1.0f) * 0.5f);
                }
                else
                {
                    checkPoints = (int)(j * 0.5f);
                }


                //Instantiate Point
                GameObject pointTemp = (GameObject)Instantiate(pointsPrefab, new Vector3(startingXposition, 0, -20), Quaternion.identity);

                //Set it as child of Line
                pointTemp.transform.parent = line.transform;

                pointTemp.name = "Point " + j.ToString();

                int xOffset = (int)(((sizePoint + emptyLineAreaSize) * (0.5f)) + ((sizePoint + emptyLineAreaSize) * checkPoints));

                if (j % 2 == 1)
                {
                    xOffset = -xOffset;
                }

                pointTemp.transform.position = new Vector3(pointTemp.transform.position.x + xOffset,
                    line.transform.position.y,
                    pointTemp.transform.position.z);

                pointsList.Add(pointTemp);  //Temp list
                                
            }
        }

        pointsList.Sort(sortLine);

        checkPointsDistance(ref pointsList, (sizePoint + emptyLineAreaSize));

        points.Add(pointsList);     //2D List
    }

    private void CheckPossibilityOfNewColumn(float distanceBetweenPoints)
    {
        float timeWidth = GameObject.Find("Time").GetComponent<RectTransform>().rect.width;
        float maxWidth = 650;

        if ((squaredAreaSize + distanceBetweenPoints) > maxWidth)
        {
            return;
        }

        float floatNumberPointsInLine = (maxWidth - squaredAreaSize) / distanceBetweenPoints;

        numberPointsInLine += (int)Mathf.Floor(floatNumberPointsInLine);



        pointsAreaWidth += distanceBetweenPoints * (numberPointsInLine - numberLines);


    }

    private int sortLine(GameObject GO1, GameObject GO2)
    {

        if (GO1 == null)
        {
            if (GO2 == null)
            {
                // If GO1 is null and GO2 is null, they're
                // equal. 
                return 0;
            }
            else
            {
                // If GO1 is null and GO2 is not null, GO2
                // is greater. 
                return -1;
            }
        }
        else
        {
            // If GO1 is not null...
            //
            if (GO2 == null)
            // ...and GO2 is null, GO1 is greater.
            {
                return 1;
            }
            else
            {
                // ...and GO2 is not null, compare the 
                // lengths of the two strings.
                //
                if (GO1.transform.position.x > GO2.transform.position.x)
                {
                    return 1;
                }
                else if (GO1.transform.position.x < GO2.transform.position.x)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }
    }
}
