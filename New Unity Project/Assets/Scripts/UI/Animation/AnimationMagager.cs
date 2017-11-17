using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMagager : MonoBehaviour
{
    //public GameObject test;
    //public GameObject test2;
    [SerializeField]
    public Vector3 EndPositionTime;
    public Vector3 EndPositionScore;
    Vector3 pp;
    Vector3 ShapePosition;
    public GameObject t0;
    public GameObject t1;
    public GameObject t2;
    public GameObject t3;
    public GameObject t4;
    public GameObject t5;
    public GameObject t6;
    public GameObject t7;
    public GameObject t8;
    public GameObject t9;
    public int sp;
    public int ShapeMoveSpeed;
    public static AnimationMagager mAnimation = null;
    int mTimebonus;
    int mScoreBonus;
    private List<int> digits;
    private List<int> digits2;
    public Animator myAnimator;
    public float maxSize;
    public float growFactor;
    public float waitTime;
    List<GameObject> Temp;
    private void Awake()
    {

        //Check if instance already exist
        if (mAnimation == null)
        {
            //if not, set instance to this
            mAnimation = this;
        }
        else if (mAnimation != this) //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a TouchManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);

        digits = new List<int>();
        digits2 = new List<int>();

        //myAnimator = new Animator();
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Scale(GameObject Shap)
    {
        float timer = 0;

        while (true) // this could also be a condition indicating "alive or dead"
        {
            // we scale all axis, so they will have the same value, 
            // so we can work with a float instead of comparing vectors
            while (maxSize > Shap.transform.localScale.x)
            {
                timer += Time.deltaTime;
                Shap.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }
            // reset the timer
            timer = 0;
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator Move(GameObject point, Vector3 sPosition, Vector3 ePosition, float speed)
    {
        float starttime = Time.time;
        while (Time.time < starttime + speed)
        {
            point.transform.position = Vector3.Lerp(sPosition, ePosition, (Time.time - starttime) / speed);
            yield return null;
        }
        point.transform.position = ePosition;
        Destroy(point);
    }
    IEnumerator MoveShape(GameObject Shape, Vector3 sPosition, Vector3 ePosition, float speed)
    {
        float starttime = Time.time;
        while (Time.time < starttime + speed)
        {
            Shape.transform.position = Vector3.Lerp(sPosition, ePosition, (Time.time - starttime) / speed);
            yield return null;
        }
        Shape.transform.position = ePosition;
    }

    public void TimeAnimation(ref GameObject point, ref GameObject currentshape)
    {
        Vector3 pp = point.transform.position;
        mTimebonus = currentshape.GetComponent<Shapes>().timeBonus;
        int size = mTimebonus.ToString().Length;
        int halfSize;

        int sizeImage = 55;
        float offset = 0;
        bool hasOffset = false;

        for (int i = size - 1; i >= 0; --i)
        {
            digits2.Add(mTimebonus / (int)Mathf.Pow(10, i));
            mTimebonus = mTimebonus % (int)Mathf.Pow(10, i);
        }

        if (size % 2 == 0)
        {
            offset = (float)sizeImage * 0.5f;
            hasOffset = true;
            halfSize = (int)((float)size * 0.5f);
        }
        else
        {
            halfSize = (int)(((float)size - 1) * 0.5f);
        }

        for (int i = 0; i < size; ++i)
        {
  
            switch (digits2[i])
            {
               
                case 0:
                    GameObject P0 = Instantiate(t0, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P0, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P0, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P0, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P0, pp, EndPositionTime, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P0, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                 }
                    break;
                case 1:
                    GameObject P1 = Instantiate(t1, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P1, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P1, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P1, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P1, pp, EndPositionTime, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P1, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    break;

                case 2:
                    GameObject P2 = Instantiate(t2, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P2, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else  //subtract to x
                            StartCoroutine(Move(P2, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                    }
        
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P2, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P2, pp, EndPositionTime, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P2, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    break;
                case 3:
                    GameObject P3 = Instantiate(t3, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P3, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P3, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P3, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P3, pp, EndPositionTime, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P3, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    break;
                case 4:
                    GameObject P4 = Instantiate(t4, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P4, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P4, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P4, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P4, pp, EndPositionTime, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P4, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    break;
                case 5:
                    GameObject P5 = Instantiate(t5, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P5, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P5, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P5, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P5, pp, EndPositionTime, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P5, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    break;
                case 6:
                    GameObject P6 = Instantiate(t6, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P6, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P6, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P6, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P6, pp, EndPositionTime, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P6, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    break;
                case 7:
                    GameObject P7 = Instantiate(t7, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P7, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P7, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P7, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P7, pp, EndPositionTime, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P7, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    break;
                case 8:
                    GameObject P8 = Instantiate(t8, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P8, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P8, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P8, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P8, pp, EndPositionTime, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P8, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    break;
                case 9:
                    GameObject P9 = Instantiate(t9, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P9, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P9, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P9, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P9, pp, EndPositionTime, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P9, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionTime, sp));
                        }
                    }
                    break;
                default:
                    //ASSERT
                    break;
            }

        }
        digits2.Clear();
        }

    public void ScoreAnimation(ref GameObject point, ref GameObject currentshape)
    {
        // get point position
        // play animation to correct position
        Vector3 pp = point.transform.position;
        mScoreBonus = currentshape.GetComponent<Shapes>().points;
        int size = mScoreBonus.ToString().Length;
        int halfSize;

        int sizeImage = 55;
        float offset = 0;
        bool hasOffset = false;

        for (int i = size - 1; i >= 0; --i)
        {
            digits.Add(mScoreBonus / (int)Mathf.Pow(10, i));
            mScoreBonus = mScoreBonus % (int)Mathf.Pow(10, i);
        }

        if (size % 2 == 0)
        {
            offset = (float)sizeImage * 0.5f;
            hasOffset = true;
            halfSize = (int)((float)size * 0.5f);
        }
        else
        {
            halfSize = (int)(((float)size - 1) * 0.5f);
        }

        for (int i = 0; i < size; ++i)
        {
            switch (digits[i])
            {
              
                case 0:
                    GameObject P0 = Instantiate(t0, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P0, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P0, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P0, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P0, pp, EndPositionScore, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P0, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    break;
                case 1:
                    GameObject P1 = Instantiate(t1, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P1, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P1, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P1, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P1, pp, EndPositionScore, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P1, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    break;
                case 2:
                    GameObject P2 = Instantiate(t2, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P2, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P2, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P2, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P2, pp, EndPositionScore, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P2, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    break;
                case 3:
                    GameObject P3 = Instantiate(t3, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P3, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P3, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P3, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P3, pp, EndPositionScore, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P3, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    break;
                case 4:
                    GameObject P4 = Instantiate(t4, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P4, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P4, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P4, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P4, pp, EndPositionScore, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P4, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    break;
                case 5:
                    GameObject P5 = Instantiate(t5, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P5, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P5, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P5, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P5, pp, EndPositionScore, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P5, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    break;
                case 6:
                    GameObject P6 = Instantiate(t6, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P6, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P6, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P6, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P6, pp, EndPositionScore, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P6, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    break;
                case 7:
                    GameObject P7 = Instantiate(t7, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P7, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P7, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P7, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P7, pp, EndPositionScore, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P7, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    break;
                case 8:
                    GameObject P8 = Instantiate(t8, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P8, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P8, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P8, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P8, pp, EndPositionScore, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P8, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    break;
                case 9:
                    GameObject P9 = Instantiate(t9, pp, transform.rotation);
                    if (hasOffset) //even numbers
                    {
                        if (i + 1 > halfSize) //add to x
                        {
                            StartCoroutine(Move(P9, pp + new Vector3((sizeImage * ((halfSize + 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else  //subtract to x
                        {
                            StartCoroutine(Move(P9, pp - new Vector3((sizeImage * ((halfSize - 1) - i)) + offset, 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    else //odd numbers
                    {
                        if (i + 1 <= halfSize) //subtract to x
                        {
                            StartCoroutine(Move(P9, pp - new Vector3((sizeImage * (halfSize - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                        else if (i == halfSize) //Middle position
                        {
                            StartCoroutine(Move(P9, pp, EndPositionScore, sp));
                        }
                        else  //add to x
                        {
                            StartCoroutine(Move(P9, pp + new Vector3((sizeImage * ((halfSize + 2) - i)), 1.0f, 1.0f), EndPositionScore, sp));
                        }
                    }
                    break;
                default:
                    //ASSERT
                    break;
            }

        }

        digits.Clear();

    }

    public void ShapeMoveOut(List<GameObject> ShapeList)
    {
        Temp = TouchManager.mTouchManager.GetShapesIniatialized();
        myAnimator.SetBool("IsOne", true);
       // StartCoroutine(MoveShape(ShapeList[0], ShapeList[0].transform.position, ShapeList[0].transform.position + new Vector3(200,0,0),ShapeMoveSpeed));
        for (int i = 1; i < TouchManager.mTouchManager.GetNumShapesInstantied(); i++)
        {
            if (i == 1)
            {
                StartCoroutine(Scale(Temp[i]));
            }
            StartCoroutine(MoveShape(Temp[i], Temp[i].transform.position, Temp[i].transform.position + new Vector3(0, 300 * PointsManager.mPointsManager.GetScreenYOffset(), 0), ShapeMoveSpeed));
        }
        //TouchManager.mTouchManager.DeleteCurrentShape(false);
    }

}
