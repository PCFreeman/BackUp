using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawTouch : MonoBehaviour {

    //Change Screen Variables
    private Vector3 screenChangeStartPosition;
    private Vector3 screenChangeFinalPosition;

    public float distanceToScreenChange;
    private bool isMoving = false;
    private bool isLeft = false;
    private bool wasInstantiated = false;
    private float totalCanvasMove;
    private GameObject canvasClone;



    //Draw Variables
    public GameObject linePrefab;
    public GameObject lineColliderPrefab;
    private GameObject thisLine;
    private Vector3 startPosition;
    private Plane objectPlane;
    private GameObject coll; // line collider
    GameObject firstPoint;
    GameObject curShape;
    public GameObject comboSystem;


    public float timeLimit = -5.0f;

    private float timeColor = 0.0f;

    private bool LastShapeCorect;

    private float lineColliderSize;

    public void Awake()
    {
        coll = (GameObject)Instantiate(lineColliderPrefab, new Vector3(5000.0f, 0.0f, 0.0f), Quaternion.identity);

        distanceToScreenChange *= PointsManager.mPointsManager.GetScreenXOffset();
        totalCanvasMove = GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution.x * PointsManager.mPointsManager.GetScreenXOffset();
    }

    public void Initialize()
    {
        linePrefab.GetComponent<TrailRenderer>().startWidth = 5 * PointsManager.mPointsManager.GetScreenYOffset();
        linePrefab.GetComponent<TrailRenderer>().endWidth = 5 * PointsManager.mPointsManager.GetScreenYOffset();

        lineColliderSize = linePrefab.GetComponent<TrailRenderer>().startWidth;
        //Debug.Log("Line width: " + linePrefab.GetComponent<TrailRenderer>().startWidth.ToString());


        LastShapeCorect = false;
        //pointsSelected = new List<GameObject>();
        objectPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

        firstPoint = new GameObject();
        curShape = new GameObject();



        //Show level 1 animation

        // ref here is bullshit! no need to use it is just an UINT that is never changed inside function
        uint firstLevel = 1;
        TouchManager.mTouchManager.mLevelAnimation.GetComponent<LevelDisplay>().LevelMovement(ref firstLevel);

    }
    
    // Update is called once per frame
    public void update()
    {
        if (!isMoving)
        {
            if (timeLimit == -5.0f)
            {
                timeLimit = TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().DecrementTimeLimit(LevelManager.mLevelManager.GetTotalReduceTime());
            }

            if (LastShapeCorect)
            {
                timeColor += Time.deltaTime;
                if (timeColor >= 1.0f)
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

            if (Input.touchCount == 1)
            {
                //This function can be use for Touch or mouse click
                if ((Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
                {
                    if (LastShapeCorect == true)
                    {
                        foreach (GameObject GO in TouchManager.mTouchManager.pointsSelected)
                        {
                            GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                        }
                        TouchManager.mTouchManager.pointsSelected.Clear();
                        LastShapeCorect = false;
                        timeColor = 0.0f;
                    }

                    thisLine = (GameObject)Instantiate(linePrefab, this.transform.position, Quaternion.identity);
                    thisLine.name = "Line";



                    Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    //Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);           //Use This for Mouse test


                    float rayDistance;
                    if (objectPlane.Raycast(mRay, out rayDistance))    //This check the contact of RayCast with plane and return the distance
                    {
                        startPosition = mRay.GetPoint(rayDistance);
                    }

                }
                else if ((Input.GetTouch(0).phase == TouchPhase.Moved) || (Input.GetMouseButton(0)))
                {


                    Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    //Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);           //Use This for Mouse test

                    float rayDistance;
                    if (objectPlane.Raycast(mRay, out rayDistance))    //This check the contact of RayCast with plane and return the distance
                    {
                        //coll = (GameObject)Instantiate(lineColliderPrefab, new Vector3(5000.0f, 0.0f, 0.0f), Quaternion.identity);
                        if (thisLine == null)
                        {
                            thisLine = (GameObject)Instantiate(linePrefab, this.transform.position, Quaternion.identity);
                            thisLine.name = "Line";
                        }
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
                            coll.GetComponent<BoxCollider>().size = new Vector3(lineColliderSize, distance, 1.0f);
                            coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

                        }
                        else if (startPosition.x != thisLine.transform.position.x && startPosition.y == thisLine.transform.position.y)
                        {
                            //Horizontal Line


                            float distance = thisLine.transform.position.x - startPosition.x;

                            coll.transform.position = new Vector3((distance * 0.5f) + startPosition.x, startPosition.y, startPosition.z);
                            coll.GetComponent<BoxCollider>().size = new Vector3(distance, lineColliderSize, 1.0f);
                            coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

                        }
                        else
                        {
                            float distance = GetPointsDistance(startPosition, thisLine.transform.position);
                            float distanceX = thisLine.transform.position.x - startPosition.x;
                            float distanceY = thisLine.transform.position.y - startPosition.y;

                            coll.transform.position = new Vector3((distanceX * 0.5f) + startPosition.x, (distanceY * 0.5f) + startPosition.y, startPosition.z);
                            coll.GetComponent<BoxCollider>().size = new Vector3(distance, lineColliderSize, 1.0f);

                            coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, GetRotation(startPosition, thisLine.transform.position));
                        }
                    }

                    startPosition = thisLine.transform.position;

                }
                else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
                {
                    coll.GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 1.0f);
                    coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled)
                {

                }
                else if ((Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
                {

                    //TouchManager.mTouchManager.pointsSelected = LineTouch.GetCollidedObjects();
                    TouchManager.mTouchManager.pointsSelected = TouchManager.mTouchManager.GetCollidedObjects();

                    //Debug.Log("points selected = " + TouchManager.mTouchManager.pointsSelected.ToString()); 


                    // Check if the line makes the corect shape
                    //if (TouchManager.mTouchManager.mTouchLogic.checkShapes(TouchLogic.Shapes.Triangle5X3YDown, ref TouchManager.mTouchManager.pointsSelected))
                    if (TouchManager.mTouchManager.mTouchLogic.checkShapes(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().GetShpeType(), ref TouchManager.mTouchManager.pointsSelected))
                    {


                        AudioController.sInstance.SuccessMoveSFX();
                        comboSystem.GetComponent<ComboSystem>().IncreaseCount();

                        curShape = TouchManager.mTouchManager.GetCurrentShape();
                        firstPoint = TouchManager.mTouchManager.pointsSelected[0];

                        Debug.Log("..........." + curShape.name);
                        Debug.Log("***********" + firstPoint.name);



                        AnimationMagager.mAnimation.ScoreAnimation(ref firstPoint, ref curShape);
                        AnimationMagager.mAnimation.TimeAnimation(ref firstPoint, ref curShape);

                        Debug.Log("Correct Shape");

                        AnimationMagager.mAnimation.ShapeMoveOut(TouchManager.mTouchManager.GetShapesIniatialized());

                        LevelManager.mLevelManager.DecreaseShapesToNext();

                        //Destroy(thisLine);

                        foreach (GameObject GO in TouchManager.mTouchManager.pointsSelected)
                        {
                            GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                        }

                        LastShapeCorect = true;

                        //Add points to score
                        //Peter: Add Multiplier = 0 check
                        if (comboSystem.GetComponent<ComboSystem>().GetMultiplier() == 0)
                            UIManage.instance.AddScore(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().points);
                        else
                            UIManage.instance.AddScore(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().points * comboSystem.GetComponent<ComboSystem>().GetMultiplier());
                        //Add points to score
                        UIManage.instance.AddTime(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().timeBonus);

                        ResetCollider();



                        //TouchManager.mTouchManager.mColliders.pointCount = 0;


                        //Call the winning animation or add points or ...

                    }
                    else
                    {
                        Debug.Log("Wrong Shape");

                        if (TouchManager.mTouchManager.GetCollidedObjects().Count > 0)
                        {
                            LevelManager.mLevelManager.DecreaseShapesTry();
                        }
                        AudioController.sInstance.ErrorSFX();
                        comboSystem.GetComponent<ComboSystem>().ResetCount();

                        //Destroi the line , may add some stuff in future to make player know that made mistake
                        //Destroy(thisLine);
                        Debug.Log("GOs 2 size = " + TouchManager.mTouchManager.pointsSelected.Count.ToString());
                        foreach (GameObject GO in TouchManager.mTouchManager.pointsSelected)
                        {
                            GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                        }

                        ResetCollider();

                        TouchManager.mTouchManager.pointsSelected.Clear();


                    }

                    //checkTouch = false;
                    timeLimit = TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().DecrementTimeLimit(LevelManager.mLevelManager.GetTotalReduceTime());
                    Destroy(thisLine);
                }
            }
            else if (Input.touchCount == 2)
            {
                Debug.Log("============================Two Touch");


                if (thisLine != null)
                {
                    Destroy(thisLine);
                }

                //Changing level by moving screen with two fingers
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(1).position);

                    float rayDistance;
                    if (objectPlane.Raycast(mRay, out rayDistance))    //This check the contact of RayCast with plane and return the distance
                    {
                        screenChangeStartPosition = mRay.GetPoint(rayDistance);
                    }
                }
                else if (Input.GetTouch(1).phase == TouchPhase.Canceled)
                {

                }
                else if (Input.GetTouch(1).phase == TouchPhase.Ended)
                {
                    Debug.Log("============================Two Touch finished");
                    Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(1).position);

                    float rayDistance;
                    if (objectPlane.Raycast(mRay, out rayDistance))    //This check the contact of RayCast with plane and return the distance
                    {
                        screenChangeFinalPosition = mRay.GetPoint(rayDistance);
                    }

                    //Check for distance necessary for level change
                    if ((screenChangeFinalPosition.x - screenChangeStartPosition.x) < 0.0f)
                    {
                        Debug.Log("============================isMoving");

                        //Drecrease level
                        if (Mathf.Abs(screenChangeFinalPosition.x - screenChangeStartPosition.x) >= distanceToScreenChange)
                        {
                            //start moving
                            isMoving = true;
                            isLeft = true;
                        }
                    }
                    else
                    {
                        Debug.Log("============================isMoving");
                        //Increase level
                        if (Mathf.Abs(screenChangeFinalPosition.x - screenChangeStartPosition.x) >= distanceToScreenChange)
                        {
                            //start moving
                            isMoving = true;
                            isLeft = false;
                        }
                    }
                }
            }
            else //==================================================delete for final version, mouse debug
            {
                //This function can be use for Touch or mouse click
                if ((Input.GetMouseButtonDown(0)))
                {
                    if (LastShapeCorect == true)
                    {
                        foreach (GameObject GO in TouchManager.mTouchManager.pointsSelected)
                        {
                            GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                        }
                        TouchManager.mTouchManager.pointsSelected.Clear();
                        LastShapeCorect = false;
                        timeColor = 0.0f;
                    }

                    thisLine = (GameObject)Instantiate(linePrefab, this.transform.position, Quaternion.identity);
                    thisLine.name = "Line";



                    //Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);           //Use This for Mouse test


                    float rayDistance;
                    if (objectPlane.Raycast(mRay, out rayDistance))    //This check the contact of RayCast with plane and return the distance
                    {
                        startPosition = mRay.GetPoint(rayDistance);
                    }

                }
                else if ((Input.GetMouseButton(0)))
                {


                    //Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);           //Use This for Mouse test

                    float rayDistance;
                    if (objectPlane.Raycast(mRay, out rayDistance))    //This check the contact of RayCast with plane and return the distance
                    {
                        //coll = (GameObject)Instantiate(lineColliderPrefab, new Vector3(5000.0f, 0.0f, 0.0f), Quaternion.identity);
                        if (thisLine == null)
                        {
                            thisLine = (GameObject)Instantiate(linePrefab, this.transform.position, Quaternion.identity);
                            thisLine.name = "Line";
                        }
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
                            coll.GetComponent<BoxCollider>().size = new Vector3(lineColliderSize, distance, 1.0f);
                            coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

                        }
                        else if (startPosition.x != thisLine.transform.position.x && startPosition.y == thisLine.transform.position.y)
                        {
                            //Horizontal Line


                            float distance = thisLine.transform.position.x - startPosition.x;

                            coll.transform.position = new Vector3((distance * 0.5f) + startPosition.x, startPosition.y, startPosition.z);
                            coll.GetComponent<BoxCollider>().size = new Vector3(distance, lineColliderSize, 1.0f);
                            coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

                        }
                        else
                        {
                            float distance = GetPointsDistance(startPosition, thisLine.transform.position);
                            float distanceX = thisLine.transform.position.x - startPosition.x;
                            float distanceY = thisLine.transform.position.y - startPosition.y;

                            coll.transform.position = new Vector3((distanceX * 0.5f) + startPosition.x, (distanceY * 0.5f) + startPosition.y, startPosition.z);
                            coll.GetComponent<BoxCollider>().size = new Vector3(distance, lineColliderSize, 1.0f);

                            coll.GetComponent<BoxCollider>().transform.eulerAngles = new Vector3(0.0f, 0.0f, GetRotation(startPosition, thisLine.transform.position));
                        }
                    }

                    startPosition = thisLine.transform.position;

                }
                else if ((Input.GetMouseButtonUp(0)))
                {

                    //TouchManager.mTouchManager.pointsSelected = LineTouch.GetCollidedObjects();
                    TouchManager.mTouchManager.pointsSelected = TouchManager.mTouchManager.GetCollidedObjects();

                    //Debug.Log("points selected = " + TouchManager.mTouchManager.pointsSelected.ToString()); 


                    // Check if the line makes the corect shape
                    //if (TouchManager.mTouchManager.mTouchLogic.checkShapes(TouchLogic.Shapes.Triangle5X3YDown, ref TouchManager.mTouchManager.pointsSelected))
                    if (TouchManager.mTouchManager.mTouchLogic.checkShapes(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().GetShpeType(), ref TouchManager.mTouchManager.pointsSelected))
                    {


                        AudioController.sInstance.SuccessMoveSFX();
                        comboSystem.GetComponent<ComboSystem>().IncreaseCount();

                        curShape = TouchManager.mTouchManager.GetCurrentShape();
                        firstPoint = TouchManager.mTouchManager.pointsSelected[0];

                        Debug.Log("..........." + curShape.name);
                        Debug.Log("***********" + firstPoint.name);



                        AnimationMagager.mAnimation.ScoreAnimation(ref firstPoint, ref curShape);
                        AnimationMagager.mAnimation.TimeAnimation(ref firstPoint, ref curShape);

                        Debug.Log("Correct Shape");

                        AnimationMagager.mAnimation.ShapeMoveOut(TouchManager.mTouchManager.GetShapesIniatialized());

                        LevelManager.mLevelManager.DecreaseShapesToNext();

                        //Destroy(thisLine);

                        foreach (GameObject GO in TouchManager.mTouchManager.pointsSelected)
                        {
                            GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                        }

                        LastShapeCorect = true;

                        //Add points to score
                        //Peter: Add Multiplier = 0 check
                        if (comboSystem.GetComponent<ComboSystem>().GetMultiplier() == 0)
                            UIManage.instance.AddScore(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().points);
                        else
                            UIManage.instance.AddScore(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().points * comboSystem.GetComponent<ComboSystem>().GetMultiplier());
                        //Add points to score
                        UIManage.instance.AddTime(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().timeBonus);

                        ResetCollider();



                        //TouchManager.mTouchManager.mColliders.pointCount = 0;


                        //Call the winning animation or add points or ...

                    }
                    else
                    {
                        Debug.Log("Wrong Shape");

                        if (TouchManager.mTouchManager.GetCollidedObjects().Count > 0)
                        {
                            LevelManager.mLevelManager.DecreaseShapesTry();
                        }
                        AudioController.sInstance.ErrorSFX();
                        comboSystem.GetComponent<ComboSystem>().ResetCount();

                        //Destroi the line , may add some stuff in future to make player know that made mistake
                        //Destroy(thisLine);
                        Debug.Log("GOs 2 size = " + TouchManager.mTouchManager.pointsSelected.Count.ToString());
                        foreach (GameObject GO in TouchManager.mTouchManager.pointsSelected)
                        {
                            GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                        }

                        ResetCollider();

                        TouchManager.mTouchManager.pointsSelected.Clear();


                    }

                    //checkTouch = false;
                    timeLimit = TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().DecrementTimeLimit(LevelManager.mLevelManager.GetTotalReduceTime());
                    Destroy(thisLine);
                }
            }

            DecrementTime();


            UIManage.instance.UpdateNextLevel(LevelManager.mLevelManager.GetToNext());
            UIManage.instance.UpdateShapesTry(LevelManager.mLevelManager.GetNumTry());
            UIManage.instance.UpdateShapesTimeLimit(timeLimit);

        }
        else
        {
            //Move screen code

            //Check if is possible
            if (isLeft )//&& LevelManager.mLevelManager.GetCurrentLevelIndex() > 1)
            {
                GameObject canvas =  GameObject.Find("Level");
                
                if (!wasInstantiated)
                {
                canvasClone = GameObject.Instantiate(canvas, new Vector3(canvas.transform.position.x + totalCanvasMove,
                    canvas.transform.position.y, canvas.transform.position.z), Quaternion.identity, GameObject.Find("Canvas").transform);

                    wasInstantiated = true;
                }



                float canvasMoveVariable = 100.0f;
                if(totalCanvasMove > canvasMoveVariable)
                {
                    canvas.transform.position = new Vector3(canvas.transform.position.x - canvasMoveVariable,
                        canvas.transform.position.y,
                        canvas.transform.position.z);

                    canvasClone.transform.position = new Vector3(canvasClone.transform.position.x - canvasMoveVariable,
                        canvasClone.transform.position.y,
                        canvasClone.transform.position.z);

                    totalCanvasMove -= canvasMoveVariable;
                }
                else
                {
                    canvas.transform.position = new Vector3(canvas.transform.position.x - totalCanvasMove,
                        canvas.transform.position.y,
                        canvas.transform.position.z);

                    canvasClone.transform.position = new Vector3(canvasClone.transform.position.x - totalCanvasMove,
                        canvasClone.transform.position.y,
                        canvasClone.transform.position.z);


                    totalCanvasMove = 0;

                    isMoving = false;
                    Destroy(canvas);
                    canvasClone.name = "Level";
                }

            }
            else if (!isLeft && LevelManager.mLevelManager.GetCurrentLevelIndex() < GameManager.mGameManager.GetHighLevel())
            {

            }

        }       
        
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

            timeLimit = TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().DecrementTimeLimit(LevelManager.mLevelManager.GetTotalReduceTime());


        }
    }

}
