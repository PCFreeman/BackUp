using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TManager : MonoBehaviour {

    public GameObject First;
    public GameObject Second;
    public GameObject Third;
    public GameObject Fourth;
    public GameObject PointArea;
    public GameObject RB;
    public GameObject mHand;
    private float mOriginPos;
    private float mCurrentPos;
    public static TManager mTutorial = null;
    public float MovingSpeed;
    bool Check;
    private int count = 1;
    private void Awake()
    {

        //Check if instance already exist
        if (mTutorial == null)
        {
            //if not, set instance to this
            mTutorial = this;
        }
        else if (mTutorial != this) //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a TouchManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);

    }

    IEnumerator move(GameObject Object, Vector3 sPosition, Vector3 ePosition, float speed)
    {
        float starttime = Time.time;
        while (Time.time < starttime + speed)
        {
            Object.transform.position = Vector3.Lerp(sPosition, ePosition, (Time.time - starttime) / speed);
            yield return null;
        }
        Object.transform.position = ePosition;
    }

    void Start()
    {
        Fanimation();
        Check = false;
        First.SetActive(true);
        Second.SetActive(true);
        Third.SetActive(true);
        mOriginPos = mHand.transform.position.y;
        
        //TTouchManager.mTTouchManager.InstantiateShapes();



    }
    void HandMove()
    {
        mCurrentPos = mHand.transform.position.y;
        if (mOriginPos - mCurrentPos >= 200.0f)
        {
            mHand.transform.position = new Vector3(-45.0f, 233.0f, -25.0f);
        }
        mHand.transform.Translate(0.0f, -1.5f, 0.0f);
    }


    void EnablEverything()
    {
        PointArea.SetActive(true);
        TTouchManager.mTTouchManager.InstantiateShapes();
        RB.SetActive(true);
    }
    void Fanimation()
    {
        StartCoroutine(move(First, First.transform.position,
            First.transform.position + new Vector3(0, 300, 0),
            MovingSpeed));
        count++;
    }
    void Sanimation()
    {
     StartCoroutine(move(First, First.transform.position,
           First.transform.position - new Vector3(0, 300, 0),
           MovingSpeed));

        EnablEverything();

        StartCoroutine(move(Second, Second.transform.position,
     Second.transform.position + new Vector3(130, 0, 0),
     MovingSpeed));
        count++;
    }

    void Tanimation()
    {
        StartCoroutine(move(Second, Second.transform.position,
     Second.transform.position - new Vector3(130, 0, 0),
     MovingSpeed));
        mHand.SetActive(true);

        StartCoroutine(move(Third, Third.transform.position,
    Third.transform.position + new Vector3(125, 0, 0),
    MovingSpeed));
        Check = true;
    }

    public void AnimatioonEnd()
    {
        StartCoroutine(move(Third, Third.transform.position,
   Third.transform.position - new Vector3(125, 0, 0),
   MovingSpeed));
        mHand.SetActive(false);

    }
    public void AfterDraw()
    {
        Fourth.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        HandMove();
        if (Check == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch(count)
                {
                    case 1:
                        Fanimation();
                        break;
                    case 2:
                        Sanimation();
                        break;
                    case 3:
                        Tanimation();
                        break;
                }
                
            }
        }
    }
}
