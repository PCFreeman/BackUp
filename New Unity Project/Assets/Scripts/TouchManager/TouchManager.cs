using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    private GameObject currentLevel;



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


    //Touch Manager 

    public static TouchManager mTouchManager = null;

    public Colliders mColliders;
    public TouchLogic mTouchLogic;
    public DrawTouch mDrawTouch;
    private List<GameObject> mShapes;           //All types of Shapes
    private List<GameObject> mShapesList;       //List of Shapes during GamePlay
    public uint NumberOfShapes;
    private List<GameObject> mShapesInstantied;
    private uint NumberOfShapesInstantiedMax;

    public GameObject redBorder;


    public List<GameObject> pointsSelected;
    private List<GameObject> GOs;


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
    }

        
    // Use this for initialization
    void Start () {
        Debug.Log("[TouchManager]Manager successfully started.");

        mColliders = new Colliders();
        mTouchLogic = new TouchLogic();
        Debug.Log("TouchLogic   " + mTouchLogic.ToString());

        pointsSelected = new List<GameObject>();
        mShapes = new List<GameObject>();
        mShapesList = new List<GameObject>();
        mShapesInstantied = new List<GameObject>();

        mDrawTouch.Initialize();
        mColliders.Initialize();

        NumberOfShapesInstantiedMax = 3;                //Number of Shapes showing in screen
        GenerateShapesList(true);
        InstantiateShapes();

        mColliders.mCurrentShape = GetCurrentShape();

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

        }


        if (mShapesList.Count == 0)
        { 
            //Generate List with random shapes
            for (int i = 0; i < NumberOfShapes; ++i)
            {
                mShapesList.Add(mShapes[Random.Range(0, mShapes.Count - 1)]);
            }
        }
        else
        {
            mShapesList.RemoveRange((int)NumberOfShapesInstantiedMax - 1, mShapesList.Count - ((int)NumberOfShapesInstantiedMax - 1));
            //Complete List with random shapes
            for (int i = 0; i < NumberOfShapes - mShapesList.Count; ++i)
            {
                mShapesList.Add(mShapes[Random.Range(0, mShapes.Count - 1)]);
            }

        }
        Debug.Log("Size of Shapes List" + mShapesList.Count);
    }

    public void InstantiateShapes()
    {
        for (int i = mShapesInstantied.Count; i < NumberOfShapesInstantiedMax; ++i)
        {
            mShapesInstantied.Add(GameObject.Instantiate(mShapesList[i], new Vector3(0.0f,0.0f,-20.0f), Quaternion.identity));

            mShapesInstantied[i].transform.SetParent(GameObject.Find("ShapeSpawnPlace").transform, false);

            mShapesInstantied[i].GetComponent<RectTransform>().sizeDelta = new Vector2(210.0f,210.0f);
        }

        mShapesInstantied[0].GetComponent<RectTransform>().sizeDelta = new Vector2(210.0f, 210.0f);
        mShapesInstantied[1].GetComponent<RectTransform>().sizeDelta = new Vector2(105.0f, 105.0f);
        mShapesInstantied[2].GetComponent<RectTransform>().sizeDelta = new Vector2(5.0f, 5.0f);

        for (int i = 0; i < mShapesInstantied.Count; ++i)
        {
            int yPos = 0;

            switch (i)
            {
                case 0:
                    yPos = 165;
                    break;
                case 1:
                    yPos = -135;
                    break;
                case 2:
                    yPos = -435;
                    break;
                case 3:
                    yPos = -735;
                    break;
                case 4:
                    yPos = -635;
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

    public List<GameObject> GetShapesIniatialized()  // ==================     Freeman use it /////// Move shapes 132 px up          /////    TouchManager.mTouchManager.GetShapesIniatialized();
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
        Destroy(mShapesInstantied[0],3.0f);
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

    public int GetNumShapesInstantied()
    {
        return (int)NumberOfShapesInstantiedMax;
    }




}
