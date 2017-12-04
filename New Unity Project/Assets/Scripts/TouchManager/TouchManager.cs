using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    private GameObject currentLevel;

    public GameObject mLevelAnimation;


    //All shapes go here
    
    public GameObject Triangle5x3Up;
    public GameObject Triangle5x3Down;
    public GameObject Triangle5x3Right;
    public GameObject Triangle5x3Left;

    public GameObject TriangleRectangle3UpLeft;
    public GameObject TriangleRectangle3DownLeft;
    public GameObject TriangleRectangle3UpRight;
    public GameObject TriangleRectangle3DownRight;

    public GameObject TriangleRectangle4UpLeft;
    public GameObject TriangleRectangle4DownLeft;
    public GameObject TriangleRectangle4UpRight;
    public GameObject TriangleRectangle4DownRight;

    public GameObject TriangleRectangle5UpLeft;
    public GameObject TriangleRectangle5DownLeft;
    public GameObject TriangleRectangle5UpRight;
    public GameObject TriangleRectangle5DownRight;

    public GameObject Square2x2;
    public GameObject Square3x3;
    public GameObject Square4x4;

    public GameObject Rectangle2x3;
    public GameObject Rectangle3x4;
    public GameObject Rectangle3x2;
    public GameObject Rectangle4x3;

    public GameObject Octagon22;
    public GameObject Octagon23;


    //Touch Manager 

    public static TouchManager mTouchManager = null;


    public TouchLogic mTouchLogic;
    public DrawTouch mDrawTouch;
    private List<GameObject> mShapes;           //All types of Shapes
    private List<GameObject> mShapesList;       //List of Shapes during GamePlay
    public uint NumberOfShapes;
    public List<GameObject> mShapesInstantied;
    private uint NumberOfShapesInstantiedMax;

    public GameObject redBorder;


    public List<GameObject> pointsSelected;
    private List<GameObject> GOs;


    //Probability Variables
    private int totalProbability;
    private List<int> mShapesProbabilities;
    private List<float> mShapesProbabilitiesPercentage;


    private void Awake()
    {
        //mDrawTouch = GameObject.Find("TouchManager").GetComponent<DrawTouch>();
        GOs = new List<GameObject>();

        //Check if instance already exist
        if (mTouchManager == null)
        { 
            //if not, set instance to this
            mTouchManager = this;
        }
        else if (mTouchManager != this) //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a TouchManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);

        totalProbability = 0;

    }

        
    // Use this for initialization
    void Start () {
        Debug.Log("[TouchManager]Manager successfully started.");

        mTouchLogic = new TouchLogic();
        Debug.Log("TouchLogic   " + mTouchLogic.ToString());

        pointsSelected = new List<GameObject>();
        mShapes = new List<GameObject>();
        mShapesList = new List<GameObject>();
        mShapesInstantied = new List<GameObject>();

        mShapesProbabilities = new List<int>();
        mShapesProbabilitiesPercentage = new List<float>();


        mDrawTouch.Initialize();


        NumberOfShapesInstantiedMax = 3;                //Number of Shapes showing in screen
        GenerateShapesList(true);
        InstantiateShapes();

    }
	
	// Update is called once per frame
	void Update () {
        mDrawTouch.update();
    }

    void OnApplicationQuit()
    {
        Debug.Log("[TouchManager]Manager successfully finished.");
    }




    public void GenerateShapesList(bool nextLevel)
    {
        if(nextLevel)
        {
            currentLevel = LevelManager.mLevelManager.GetCurrentLevel();

            foreach (GameObject shape in currentLevel.GetComponent<Level>().mShapes)
            {
                mShapes.Add(shape);
            }

            GetCurrentTotalProbability();

            //Delete Old shapes from mShapesList, leave all instatiated shapes

            if(mShapesList.Count > (int)NumberOfShapesInstantiedMax)
            {
                mShapesList.RemoveRange((int)NumberOfShapesInstantiedMax, (int)(mShapesList.Count - NumberOfShapesInstantiedMax));
            }

            
        } 

        

        //Complete List with random shapes
        for (int i = 0; i < NumberOfShapes - mShapesList.Count; ++i)
        {
            float shapePercentage = Random.Range(0, 100);            

            int j;
            float total = mShapesProbabilitiesPercentage[0];
            for (j = 0; total < shapePercentage; )
            {
                total += mShapesProbabilitiesPercentage[j + 1];
                ++j;
            }

            mShapesList.Add(mShapes[j]);
        }

       
        Debug.Log("Size of Shapes List" + mShapesList.Count);
    }

    public void InstantiateShapes()
    {       

        for (int i = mShapesInstantied.Count; i < NumberOfShapesInstantiedMax; ++i)
        {
            mShapesInstantied.Add(GameObject.Instantiate(mShapesList[i], new Vector3(0.0f,0.0f,-20.0f), Quaternion.identity));

            mShapesInstantied[i].transform.SetParent(GameObject.Find("ShapeSpawnPlace").transform, false);

            mShapesInstantied[i].GetComponent<RectTransform>().sizeDelta = new Vector2(210.0f, 210.0f);
        }

        //mShapesInstantied[0].GetComponent<RectTransform>().sizeDelta = new Vector2(210.0f, 210.0f);//--------Freeman
        mShapesInstantied[1].GetComponent<RectTransform>().sizeDelta = new Vector2(105.0f, 105.0f);
        mShapesInstantied[2].GetComponent<RectTransform>().sizeDelta = new Vector2(5.0f, 5.0f);

        for (int i = 0; i < mShapesInstantied.Count; ++i)
        {
            int yPos = 0;

            switch (i)
            {
                case 0:
                    yPos = Mathf.FloorToInt(75 * PointsManager.mPointsManager.GetScreenYOffset());
                    break;
                case 1:
                    yPos = Mathf.FloorToInt(-100 * PointsManager.mPointsManager.GetScreenYOffset());
                    break;
                case 2:
                    yPos = Mathf.FloorToInt(-275 * PointsManager.mPointsManager.GetScreenYOffset());
                    break;
                default:
                    Debug.Assert(false, "[TouchManager] Num of shapes bigger than Max");
                    break;
            }
            mShapesInstantied[i].transform.position = new Vector3(mShapesInstantied[i].transform.parent.transform.position.x, yPos, mShapesInstantied[i].transform.parent.transform.position.z - 10);

        }

        redBorder.transform.SetAsLastSibling();

        Debug.Log("Size of Instantied Shapes List" + mShapesInstantied.Count);

    }

    public List<GameObject> GetShapesIniatialized()    
    {
        return mShapesInstantied;
    }


    public GameObject GetCurrentShape()       ///Make it work
    {
        return mShapesInstantied[0];
    }

    public void DeleteCurrentShape(bool nextLevel)
    {
        GenerateShapesList(nextLevel);
        Destroy(mShapesInstantied[0]);
        mShapesInstantied.Remove(mShapesInstantied[0]);
        mShapesList.RemoveAt(0);
        InstantiateShapes();

    }

    public void AddGameObject(GameObject GO)
    {
        GOs.Add(GO);
    }

    public List<GameObject> GetCollidedObjects()
    {
        return GOs;
    }

    public void ResetCollidedObjects()
    {
        GOs.Clear();
    }

    public int GetNumShapesInstantied()
    {
        return (int)NumberOfShapesInstantiedMax;
    }

    private void GetCurrentTotalProbability()
    {
        foreach (GameObject go in currentLevel.GetComponent<Level>().mShapes)
        {
            int prob = go.GetComponent<Shapes>().probability;
            totalProbability += prob;
            mShapesProbabilities.Add(prob);
        }

        CalculateShapesProbabilitiesProbability();
    }

    private void CalculateShapesProbabilitiesProbability()
    {
        mShapesProbabilitiesPercentage.Clear();

        for (int i = 0; i < mShapesProbabilities.Count; ++i)
        {
            mShapesProbabilitiesPercentage.Add((float)(mShapesProbabilities[i] * 100 ) / (float)totalProbability);
        }
    }


}
