using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    //All shapes go here
    
    public GameObject Triangle5x3Up;
    public GameObject Triangle5x3Down;
    public GameObject Triangle5x3Right;
    public GameObject Triangle5x3Left;
    public GameObject TriangleRectangle3UpLeft;
    public GameObject TriangleRectangle3DownLeft;

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

        NumberOfShapesInstantiedMax = 5;                //Number of Shapes showing in screen
        GenerateShapesList();
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


    private void GenerateShapesList()
    {
        mShapes.Add(Triangle5x3Up);
        mShapes.Add(Triangle5x3Down);
        mShapes.Add(Triangle5x3Right);
        mShapes.Add(Triangle5x3Left);
        mShapes.Add(TriangleRectangle3UpLeft);
        mShapes.Add(TriangleRectangle3DownLeft);

        mShapes.Add(Square2x2);
        mShapes.Add(Square3x3);
        mShapes.Add(Square4x4);

        mShapes.Add(Rectangle2x3);
        mShapes.Add(Rectangle3x2);
        mShapes.Add(Rectangle3x4);
        mShapes.Add(Rectangle4x3);

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
            //Complete List with random shapes
            for (int i = 0; i < NumberOfShapes - mShapesList.Count; ++i)
            {
                mShapesList.Add(mShapes[Random.Range(0, mShapes.Count - 1)]);
            }

        }
        Debug.Log("Size of Shapes List" + mShapesList.Count);
        mShapes.Clear();
    }

    public void InstantiateShapes()
    {
        for (int i = mShapesInstantied.Count; i < NumberOfShapesInstantiedMax; ++i)
        {
            mShapesInstantied.Add(GameObject.Instantiate(mShapesList[i], new Vector3(0.0f,0.0f,0.0f), Quaternion.identity));

            mShapesInstantied[i].transform.SetParent(GameObject.Find("ShapeSpawnPlace").transform, false);
        }

        for(int i = 0; i < mShapesInstantied.Count; ++i)
        {
            int yPos = 0;

            switch (i)
            {
                case 0:
                    yPos = 198;
                    break;
                case 1:
                    yPos = 66;
                    break;
                case 2:
                    yPos = -66;
                    break;
                case 3:
                    yPos = -198;
                    break;
                case 4:
                    yPos = -330;
                    break;
                default:
                    Debug.Assert(false, "[TouchManager] Num of shapes bigger than Max");
                    break;
            }
            mShapesInstantied[i].transform.position = new Vector3(mShapesInstantied[i].transform.parent.transform.position.x, yPos, mShapesInstantied[i].transform.parent.transform.position.z - 10);

        }


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

    public void DeleteCurrentShape()
    {
        Destroy(mShapesInstantied[0],3.0f);
        mShapesInstantied.Remove(mShapesInstantied[0]);
        mShapesList.RemoveAt(0);
        InstantiateShapes();

        if(mShapesList.Count <= 5)
        {
            GenerateShapesList();
        }

    }

    public void AddGameObject(GameObject GO)
    {
        GOs.Add(GO);
    }

    public List<GameObject> GetCollidedObjects()
    {
        return GOs;
    }


}
