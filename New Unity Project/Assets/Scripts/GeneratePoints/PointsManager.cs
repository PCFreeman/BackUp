using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PointsManager : MonoBehaviour {

    public static PointsManager mPointsManager = null;

    public int numberLines;
    private int numberPointsInLine;
    public bool isMovingUp;
    public bool isMovingSides;

    public GameObject pointsAreaPrefab;
    public GameObject pointsLinesPrefab;
    public GameObject pointsPrefab;
    public GameObject selectedPointsPrefab;

    private GameObject pointsArea;
    private List<GameObject> pointsLines;       // This controls the lines structures, not content
    public List<List<GameObject>> points;      //for [x][y]   ,  each X will represent a line and each Y will represent a point in a line X

    private GameObject selectedPointsArea;
    private List<GameObject> selectedPointsLines;
    public List<List<GameObject>> selectedPoints;      //for [x][y]   ,  each X will represent a line and each Y will represent a point in a line X



    private float ScreenXOffset;
    private float ScreenYOffset;

    private int baseScreenResolutionHeight = 540;
    private int pointsAreaHeightPadding = 100;
    //private float areaOffset;
    private float squaredAreaSize;
    private float pointsAreaWidth;
    private float maxAreaWidth;

    private float paddingLine = 10.5f;
    private float lineHeight;

    private int emptyLineAreaSize;

    //public float sizeDotSprite;
    private int sizePoint;
    //private float scalePoint;
    private int scaleSelectedPoint;

    private float startingXposition = -10.0f;
    private float startingYposition = -40.0f;
    private float posXoffset;
    private float posYoffset;


    private void Awake()
    {

        if (mPointsManager == null)
        {
            //if not, set instance to this
            mPointsManager = this;
        }
        else if (mPointsManager != this) //If instance already exists and it's not this:
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

        //Center pos offset
        posXoffset = (startingXposition * 0.0f);
        posYoffset = (startingYposition * ScreenYOffset);

        //Max Area Width
        maxAreaWidth = 600.0f * ScreenXOffset;
    }

    // Use this for initialization
    void Start ()
    {
        GeneratePointsGrid();
        Debug.Log("Canvas Width : " + GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width *
            GameObject.Find("Canvas").GetComponent<RectTransform>().localScale.x);
        Debug.Log("Canvas Height : " + GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height *
            GameObject.Find("Canvas").GetComponent<RectTransform>().localScale.y);
    }
	
	// Update is called once per frame
	void Update () {
		



	}


    public float GetScreenXOffset()
    {
        return ScreenXOffset;
    }

    public float GetScreenYOffset()
    {
        return ScreenYOffset;
    }


    public int GetNumberOfHorizontalPoints()
    {
        return numberPointsInLine;
    }

    //Will genarate the Points Area
    private void GeneratePointsGrid()
    {
        numberPointsInLine = numberLines;
                        
        //Lines Variables
        lineHeight = ((baseScreenResolutionHeight - pointsAreaHeightPadding) * ScreenYOffset) / numberLines;

        pointsPrefab.GetComponent<SpriteRenderer>().sprite.textureRect.size.Set(lineHeight, lineHeight);




        //Points Variables
        sizePoint = Mathf.FloorToInt(lineHeight * 0.6f);// / pointsPrefab.GetComponent<SpriteRenderer>().sprite.rect.width);
        scaleSelectedPoint = Mathf.FloorToInt((lineHeight * 0.6f)*0.5f);// / selectedPointsPrefab.GetComponent<SpriteRenderer>().sprite.rect.width);


        //Variables to set Points positions in a Line
        emptyLineAreaSize = ((int)(((baseScreenResolutionHeight - pointsAreaHeightPadding) * ScreenYOffset) - (2 * paddingLine)) - (numberPointsInLine * (int)sizePoint)) / (numberPointsInLine - 1);

        //Size if Area was squared
        squaredAreaSize = (baseScreenResolutionHeight - pointsAreaHeightPadding) * ScreenYOffset;

        pointsAreaWidth = squaredAreaSize;

        CheckPossibilityOfNewColumn(emptyLineAreaSize + sizePoint);


        GeneratePointsArea();

        //Initialize the List that will hold PointLines
        pointsLines = new List<GameObject>();

        //Initialize the List that will hold SelectedPointLines
        selectedPointsLines = new List<GameObject>();

        //Initialize the 2D List that will hold Points
        points = new List<List<GameObject>>();

        //Initialize the 2D List that will hold SelectedPoints
        selectedPoints = new List<List<GameObject>>();

        //Change size of points prefab
        pointsPrefab.transform.localScale = new Vector3(sizePoint, sizePoint, pointsPrefab.transform.localScale.z);
        
        //Change size of points prefab
        selectedPointsPrefab.transform.localScale = new Vector3(scaleSelectedPoint, scaleSelectedPoint, pointsPrefab.transform.localScale.z);

        GenerateLines();

        for(int i = 0; i < pointsLines.Count;++i)
        {
            GeneratePoints(pointsLines[i], selectedPointsLines[i]);
        }     
    }

    public float GetDistanceBetweenLinePoints()
    {
        return points[0][1].transform.position.x - points[0][0].transform.position.x;
    }

    public float GetDistanceBetweenLines()
    {
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
        pointsArea = (GameObject)Instantiate(pointsAreaPrefab, new Vector3(Mathf.RoundToInt((startingXposition - posXoffset)), posYoffset, -50), Quaternion.identity);

        //Instantiate Points container
        selectedPointsArea = (GameObject)Instantiate(pointsAreaPrefab, new Vector3(Mathf.RoundToInt((startingXposition - posXoffset)), posYoffset, -50), Quaternion.identity);

        pointsArea.name = "Points Area";
        selectedPointsArea.name = "Selected Points Area";

        pointsArea.GetComponent<BoxCollider>().size = new Vector3(pointsAreaWidth,
            (baseScreenResolutionHeight - pointsAreaHeightPadding) * ScreenYOffset,
            pointsArea.GetComponent<BoxCollider>().size.z);

        selectedPointsArea.GetComponent<BoxCollider>().size = new Vector3(pointsAreaWidth,
            (baseScreenResolutionHeight - pointsAreaHeightPadding) * ScreenYOffset,
            pointsArea.GetComponent<BoxCollider>().size.z);


        pointsArea.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
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
                GameObject line = (GameObject)Instantiate(pointsLinesPrefab, new Vector3(((startingXposition - posXoffset) * ScreenXOffset), posYoffset, -50), Quaternion.identity);

                //Instantiate Selected Line
                GameObject selectedLine = (GameObject)Instantiate(pointsLinesPrefab, new Vector3(((startingXposition - posXoffset) * ScreenXOffset), posYoffset, -50), Quaternion.identity);


                //Set it as child of pointsArea
                line.transform.parent = GameObject.Find("Points Area").transform;
                //Set it as child of pointsArea
                selectedLine.transform.parent = GameObject.Find("Selected Points Area").transform;

                line.name = "Line " + i.ToString();
                selectedLine.name = "Selected Line " + i.ToString();

                //Set line height
                line.GetComponent<BoxCollider>().size = new Vector3((int)((pointsAreaWidth - 10.0) - (2 * paddingLine)), lineHeight, line.GetComponent<BoxCollider>().size.z);

                //Set Selectedline height
                selectedLine.GetComponent<BoxCollider>().size = new Vector3((int)((pointsAreaWidth - 10.0) - (2 * paddingLine)), lineHeight, line.GetComponent<BoxCollider>().size.z);

                //Line Goes down
                if (i % 2 == 1)
                {
                    check = check + 1;

                    int yOffset = (int)(-(lineHeight * check));

                    line.transform.position = new Vector3(line.transform.position.x, line.transform.position.y + yOffset, line.transform.position.z);
                    selectedLine.transform.position = new Vector3(selectedLine.transform.position.x, selectedLine.transform.position.y + yOffset, selectedLine.transform.position.z);

                }
                else //Line goes up
                {
                    int yOffset = (int)(lineHeight * check);

                    line.transform.position = new Vector3(line.transform.position.x, line.transform.position.y + yOffset, line.transform.position.z);
                    selectedLine.transform.position = new Vector3(selectedLine.transform.position.x, selectedLine.transform.position.y + yOffset, selectedLine.transform.position.z);

                }

                pointsLines.Add(line);      //Line List
                selectedPointsLines.Add(selectedLine);
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
                GameObject line = (GameObject)Instantiate(pointsLinesPrefab, new Vector3(((startingXposition - posXoffset) * ScreenXOffset), posYoffset, -20), Quaternion.identity);

                //Instantiate Selected Line
                GameObject selectedLine = (GameObject)Instantiate(pointsLinesPrefab, new Vector3(((startingXposition - posXoffset) * ScreenXOffset), posYoffset, -50), Quaternion.identity);

                //Set it as child of pointsArea
                line.transform.parent = GameObject.Find("Points Area").transform;
                //Set it as child of pointsArea
                selectedLine.transform.parent = GameObject.Find("Selected Points Area").transform;

                line.name = "Line " + i.ToString();
                selectedLine.name = "Selected Line " + i.ToString();

                //Set line size
                line.GetComponent<BoxCollider>().size = new Vector3((int)((pointsAreaWidth - 10.0) - (2 * paddingLine)), lineHeight, line.GetComponent<BoxCollider>().size.z);

                //Set selectedLine size
                selectedLine.GetComponent<BoxCollider>().size = new Vector3((int)((pointsAreaWidth - 10.0) - (2 * paddingLine)), lineHeight, line.GetComponent<BoxCollider>().size.z);

                int yOffset = (int)((lineHeight * 0.5) + (lineHeight * lineCheck));


                if (i % 2 == 1)//Line goes down
                {
                    yOffset = -yOffset;
                }

                //Set line position
                line.transform.position = new Vector3(line.transform.position.x, line.transform.position.y + yOffset, line.transform.position.z);

                //Set line position
                selectedLine.transform.position = new Vector3(selectedLine.transform.position.x, selectedLine.transform.position.y + yOffset, selectedLine.transform.position.z);

                pointsLines.Add(line);      //Line List
                selectedPointsLines.Add(selectedLine);
            }

        }
    }

    private void GeneratePoints(GameObject line, GameObject selectedLine)
    {
        List<GameObject> pointsList = new List<GameObject>();
        List<GameObject> selectedPointsList = new List<GameObject>();

        //Helper Variable
        int checkPoints = 0;

        if (numberPointsInLine % 2 == 1)   //Odd number of points
        {
            for (int j = 0; j < numberPointsInLine; ++j)
            {

                //Instantiate Point
                GameObject pointTemp = (GameObject)Instantiate(pointsPrefab, new Vector3(((startingXposition - posXoffset) * ScreenXOffset), 0, -50), Quaternion.identity);

                //Instantiate Selected Point
                GameObject selectedPointTemp = (GameObject)Instantiate(selectedPointsPrefab, new Vector3(((startingXposition - posXoffset) * ScreenXOffset), 0, -50), Quaternion.identity);

                //Set it as child of Line
                pointTemp.transform.parent = line.transform;
                selectedPointTemp.transform.parent = selectedLine.transform;

                pointTemp.name = "Point " + j.ToString();
                selectedPointTemp.name = "Selected Point " + j.ToString();



                if (j % 2 == 1)
                {
                    checkPoints = checkPoints + 1;

                    int xOffset = -((int)(sizePoint + emptyLineAreaSize) * checkPoints);


                    pointTemp.transform.position = new Vector3(pointTemp.transform.position.x + xOffset, line.transform.position.y, pointTemp.transform.position.z);
                    selectedPointTemp.transform.position = new Vector3(selectedPointTemp.transform.position.x + xOffset, line.transform.position.y, selectedPointTemp.transform.position.z);
                }
                else //Line goes up
                {
                    int xOffset = (int)(sizePoint + emptyLineAreaSize) * checkPoints;

                    pointTemp.transform.position = new Vector3(pointTemp.transform.position.x + xOffset, line.transform.position.y, pointTemp.transform.position.z);
                    selectedPointTemp.transform.position = new Vector3(selectedPointTemp.transform.position.x + xOffset, line.transform.position.y, selectedPointTemp.transform.position.z);

                }
                pointsList.Add(pointTemp);  //Temp list
                selectedPointsList.Add(selectedPointTemp);
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
                GameObject pointTemp = (GameObject)Instantiate(pointsPrefab, new Vector3(((startingXposition - posXoffset) * ScreenXOffset), 0, -20), Quaternion.identity);
                
                //Instantiate Selected Point
                GameObject selectedPointTemp = (GameObject)Instantiate(selectedPointsPrefab, new Vector3(((startingXposition - posXoffset) * ScreenXOffset), 0, -50), Quaternion.identity);

                //Set it as child of Line
                pointTemp.transform.parent = line.transform;
                selectedPointTemp.transform.parent = selectedLine.transform;

                pointTemp.name = "Point " + j.ToString();
                selectedPointTemp.name = "Selected Point " + j.ToString();

                int xOffset = (int)(((sizePoint + emptyLineAreaSize) * (0.5f)) + ((sizePoint + emptyLineAreaSize) * checkPoints));

                if (j % 2 == 1)
                {
                    xOffset = -xOffset;
                }

                pointTemp.transform.position = new Vector3(pointTemp.transform.position.x + xOffset,
                    line.transform.position.y,
                    pointTemp.transform.position.z);

                selectedPointTemp.transform.position = new Vector3(selectedPointTemp.transform.position.x + xOffset,
                   line.transform.position.y,
                   selectedPointTemp.transform.position.z);

                pointsList.Add(pointTemp);  //Temp list
                selectedPointsList.Add(selectedPointTemp);
            }
        }

        pointsList.Sort(sortLine);
        selectedPointsList.Sort(sortLine);

        checkPointsDistance(ref pointsList, (sizePoint + emptyLineAreaSize));

        points.Add(pointsList);     //2D List
        selectedPoints.Add(selectedPointsList);
    }

    private void CheckPossibilityOfNewColumn(float distanceBetweenPoints)
    {

        if ((squaredAreaSize + distanceBetweenPoints) > maxAreaWidth)
        {
            return;
        }

        float floatNumberPointsInLine = (maxAreaWidth - squaredAreaSize) / distanceBetweenPoints;

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
