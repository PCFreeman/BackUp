using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawTouch : MonoBehaviour {

    public GameObject linePrefab;
    public GameObject lineColliderPrefab;
    private GameObject thisLine;
    private Vector3 startPosition;
    private Plane objectPlane;
    private GameObject coll; // line collider
    GameObject firstPoint;
    GameObject curShape;

    private float timeLimit;

    private float timeColor = 0.0f;

    private bool LastShapeCorect;

    public void Awake()
    {
        coll = (GameObject)Instantiate(lineColliderPrefab, new Vector3(5000.0f, 0.0f, 0.0f), Quaternion.identity);

    }

    public void Initialize()
    {
        LastShapeCorect = false;
        //pointsSelected = new List<GameObject>();
        objectPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

        firstPoint = new GameObject();
        curShape = new GameObject();

        timeLimit = -5;


        //Show level 1 animation

        // ref here is bullshit! no need to use it is just an UINT that is never changed inside function
        uint firstLevel = 1;
        TouchManager.mTouchManager.mLevelAnimation.GetComponent<LevelDisplay>().LevelMovement(ref firstLevel);

    }

    // Update is called once per frame
    public void update()
    {
        if(timeLimit == -5)
        {
            timeLimit = TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().timeLimit;
        }
        
        if(LastShapeCorect)
        {
            timeColor += Time.deltaTime;
            if(timeColor >= 1.0f)
            {
                foreach (GameObject GO in TouchManager.mTouchManager.pointsSelected)
                {
                    GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                }

                TouchManager.mTouchManager.pointsSelected.Clear();
                LastShapeCorect = false;
                timeColor = 0.0f;
            }
        }



        //This function can be use for Touch or mouse click
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
        {
            if(LastShapeCorect == true)
            {
                foreach (GameObject GO in TouchManager.mTouchManager.pointsSelected)
                {
                    GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                }

                TouchManager.mTouchManager.pointsSelected.Clear();
                LastShapeCorect = false;
                timeColor = 0.0f;
            }

            if(thisLine == null)
            {
                thisLine = (GameObject)Instantiate(linePrefab, this.transform.position, Quaternion.identity);
                thisLine.name = "Line";
            }

            //Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);           //Use This for Mouse test

            float rayDistance;
            if (objectPlane.Raycast(mRay, out rayDistance))    //This check the contact of RayCast with plane and return the distance
            {
                startPosition = mRay.GetPoint(rayDistance);
            }

        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || (Input.GetMouseButton(0)))
        {
            

           // Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);           //Use This for Mouse test

            float rayDistance;
            if (objectPlane.Raycast(mRay, out rayDistance))    //This check the contact of RayCast with plane and return the distance
            {
                //coll = (GameObject)Instantiate(lineColliderPrefab, new Vector3(5000.0f, 0.0f, 0.0f), Quaternion.identity);

                thisLine.transform.position = mRay.GetPoint(rayDistance);

                if (startPosition.x == thisLine.transform.position.x && startPosition.y == thisLine.transform.position.y)
                {
                    //do nothing they are in same position
                }
                else if (startPosition.x == thisLine.transform.position.x && startPosition.y != thisLine.transform.position.y)
                {
                    //Vertical Line

                    float distance = thisLine.transform.position.y - startPosition.y;

                    coll.transform.position = new Vector3(startPosition.x, (distance * 0.5f) + startPosition.y, startPosition.z);
                    coll.GetComponent<BoxCollider>().size = new Vector3(5.0f , distance, 1.0f);
                    coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

                }
                else if (startPosition.x != thisLine.transform.position.x && startPosition.y == thisLine.transform.position.y)
                {
                    //Horizontal Line
                   
                    float distance = thisLine.transform.position.x - startPosition.x;

                    coll.transform.position = new Vector3((distance * 0.5f) + startPosition.x, startPosition.y, startPosition.z);
                    coll.GetComponent<BoxCollider>().size = new Vector3(distance, 5.0f, 1.0f);
                    coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

                }
                else
                {
                    float distance = GetPointsDistance(startPosition, thisLine.transform.position);
                    float distanceX = thisLine.transform.position.x - startPosition.x;
                    float distanceY = thisLine.transform.position.y - startPosition.y;
                    
                    coll.transform.position = new Vector3((distanceX * 0.5f) + startPosition.x, (distanceY * 0.5f) + startPosition.y, startPosition.z);
                    coll.GetComponent<BoxCollider>().size = new Vector3(distance, 5.0f, 1.0f);

                    coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, GetRotation(startPosition, thisLine.transform.position));
                }
                

            }

            startPosition = thisLine.transform.position;

        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary))
        {
            //coll = (GameObject)Instantiate(lineColliderPrefab, new Vector3(5000.0f, 0.0f, 0.0f), Quaternion.identity);

            coll.GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 1.0f);
            coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Canceled))
        {
            //ResetCollider();
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
        {

            //TouchManager.mTouchManager.pointsSelected = LineTouch.GetCollidedObjects();
            TouchManager.mTouchManager.pointsSelected = TouchManager.mTouchManager.GetCollidedObjects();

            Debug.Log("points selected = " + TouchManager.mTouchManager.pointsSelected.ToString()); 


            // Check if the line makes the corect shape
            //if (TouchManager.mTouchManager.mTouchLogic.checkShapes(TouchLogic.Shapes.Trapezoid422Top, ref TouchManager.mTouchManager.pointsSelected))
            if(TouchManager.mTouchManager.mTouchLogic.checkShapes(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().GetShpeType(), ref TouchManager.mTouchManager.pointsSelected))
            {

                AudioController.sInstance.SuccessMoveSFX();

                curShape = TouchManager.mTouchManager.GetCurrentShape();
                firstPoint = TouchManager.mTouchManager.pointsSelected[0];

                Debug.Log("..........." +curShape.name);
                Debug.Log("***********" + firstPoint.name);

                AnimationMagager.mAnimation.ScoreAnimation( ref firstPoint, ref curShape);
                AnimationMagager.mAnimation.TimeAnimation(ref firstPoint, ref curShape);

               Debug.Log("Correct Shape");

               AnimationMagager.mAnimation.ShapeMoveOut(TouchManager.mTouchManager.GetShapesIniatialized());


               //Destroy(thisLine);

               foreach(GameObject GO in TouchManager.mTouchManager.pointsSelected)
               {
                   GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
               }

                LastShapeCorect = true;

                //Add points to score
                UIManage.instance.AddScore(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().points);

                //Add points to score
                UIManage.instance.AddTime(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().timeBonus);

                ResetCollider();

                TouchManager.mTouchManager.mColliders.mCurrentShape = TouchManager.mTouchManager.GetCurrentShape();


                LevelManager.mLevelManager.DecreaseShapesToNext();


                //TouchManager.mTouchManager.mColliders.pointCount = 0;
                

                //Call the winning animation or add points or ...

            }
            else
            {
                Debug.Log("Wrong Shape");

                //AudioController.sInstance.ErrorSFX();

                //Destroi the line , may add some stuff in future to make player know that made mistake
                //Destroy(thisLine);
                Debug.Log("GOs 2 size = " + TouchManager.mTouchManager.pointsSelected.Count.ToString());
                foreach (GameObject GO in TouchManager.mTouchManager.pointsSelected)
                {
                    GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                }

                ResetCollider();

                TouchManager.mTouchManager.pointsSelected.Clear();

                LevelManager.mLevelManager.DecreaseShapesTry();

                //TouchManager.mTouchManager.mColliders.pointCount = 0;
                
            }

            timeLimit = TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().timeLimit;

            Destroy(thisLine);
        }

        UIManage.instance.UpdateNextLevel(LevelManager.mLevelManager.GetToNext());
        UIManage.instance.UpdateShapesTry(LevelManager.mLevelManager.GetNumTry());
        UIManage.instance.UpdateShapesTimeLimit(timeLimit);

        DecrementTime();
    }

    public void SetSelectedPoint(ref GameObject point)
    {
        Debug.Log(" -----------    "+point.name.ToString());


        TouchManager.mTouchManager.pointsSelected.Add(point);
    }

    private float GetPointsDistance(Vector3 initialPos, Vector3 finalPos)
    {
        float xDistance = finalPos.x - initialPos.x;
        float yDistance = finalPos.y - initialPos.y;

        return Mathf.Sqrt((xDistance* xDistance) + (yDistance* yDistance));
        
    }

    private float GetRotation(Vector3 initialPos, Vector3 finalPos)
    {
        float xDistance = finalPos.x - initialPos.x;
        float yDistance = finalPos.y - initialPos.y;
                   
        float temp = (Mathf.Atan2(yDistance, xDistance) * Mathf.Rad2Deg);

        return temp;
    }

    private void ResetCollider()
    {
        coll.transform.position = new Vector3(5000.0f, 0.0f, 0.0f);
        coll.GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 1.0f);
        coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void DestroyLine()
    {
        Destroy(thisLine);
        ResetCollider();
    }

    private void DecrementTime()
    {
        timeLimit -= Time.deltaTime;

        //Put value inside time container
        //Mathf.FloorToInt() to get an int

        if(timeLimit <= 0.0f)
        {
            if(lineColliderPrefab != null)
            {
                Destroy(thisLine);
            }
            
            if(TouchManager.mTouchManager.pointsSelected.Count > 0)
            {
                foreach (GameObject GO in TouchManager.mTouchManager.pointsSelected)
                {
                    GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                }
            }


            ResetCollider();

            TouchManager.mTouchManager.pointsSelected.Clear();

            LevelManager.mLevelManager.DecreaseShapesTry();
                       

            AnimationMagager.mAnimation.ShapeMoveOut(TouchManager.mTouchManager.GetShapesIniatialized());
            TouchManager.mTouchManager.DeleteCurrentShape(false); //Delete current shape and Instantiate a new one

            timeLimit = TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().timeLimit;


        }
    }

}
