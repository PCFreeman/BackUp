using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawTouch : MonoBehaviour {

    public GameObject linePrefab;
    public GameObject lineColliderPrefab;
    private GameObject thisLine;
    private Vector3 startPosition;
    private Plane objectPlane;
    private List<GameObject> collidersList;
    private bool LastShapeCorect;


    public void Initialize()
    {
        LastShapeCorect = false;
        //pointsSelected = new List<GameObject>();
        objectPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

        collidersList = new List<GameObject>();
    }

    // Update is called once per frame
    public void update()
    {

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
            }


            thisLine = (GameObject)Instantiate(linePrefab, this.transform.position, Quaternion.identity);

            Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            //Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);           //Use This for Mouse test

            float rayDistance;
            if (objectPlane.Raycast(mRay, out rayDistance))    //This check the contact of RayCast with plane and return the distance
            {
                startPosition = mRay.GetPoint(rayDistance);
            }
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || (Input.GetMouseButton(0)))
        {
            //GameObject coll = (GameObject)Instantiate(lineColliderPrefab, this.transform.position, Quaternion.identity);
               
            Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            //Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);           //Use This for Mouse test

            float rayDistance;
            if (objectPlane.Raycast(mRay, out rayDistance))    //This check the contact of RayCast with plane and return the distance
            {
                
                thisLine.transform.position = mRay.GetPoint(rayDistance);

                if (startPosition.x == thisLine.transform.position.x && startPosition.y == thisLine.transform.position.y)
                {
                    //do nothing they are in same position
                }
                else if (startPosition.x == thisLine.transform.position.x && startPosition.y != thisLine.transform.position.y)
                {
                    //Vertical Line

                    float distance = thisLine.transform.position.y - startPosition.y;

                    //coll.transform.position = new Vector3(startPosition.x, (distance * 0.5f) + startPosition.y, startPosition.z);
                    //coll.GetComponent<BoxCollider>().size = new Vector3(1.0f , distance, 1.0f);                  

                }
                else if (startPosition.x != thisLine.transform.position.x && startPosition.y == thisLine.transform.position.y)
                {
                    //Horizontal Line

                    float distance = thisLine.transform.position.x - startPosition.x;

                    //coll.transform.position = new Vector3((distance * 0.5f) + startPosition.x, startPosition.y, startPosition.z);
                    //coll.GetComponent<BoxCollider>().size = new Vector3(distance, 1.0f, 1.0f);

                }
                else
                {
                    float distance = GetPointsDistance(startPosition, thisLine.transform.position);
                    float distanceX = thisLine.transform.position.x - startPosition.x;
                    float distanceY = thisLine.transform.position.y - startPosition.y;

                    //coll.transform.position = new Vector3((distanceX * 0.5f) + startPosition.x, (distanceY * 0.5f) + startPosition.y, startPosition.z);
                    //coll.GetComponent<BoxCollider>().size = new Vector3(distance, 1.0f, 1.0f);


                    Debug.Log("Rotation = " + GetRotation(startPosition, thisLine.transform.position).ToString());

                    //coll.GetComponent<BoxCollider>().transform.Rotate(0.0f, 0.0f, GetRotation(startPosition, thisLine.transform.position));
                }
                

            }

            startPosition = thisLine.transform.position;

            //collidersList.Add(coll);

        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
        {

            TouchManager.mTouchManager.pointsSelected = LineTouch.GetCollidedObjects();

            Debug.Log("points selected = " + TouchManager.mTouchManager.pointsSelected.ToString()); 
            // Check if the line makes the corect shape
            //if (TouchManager.mTouchManager.mTouchLogic.checkShapes(TouchLogic.Shapes.Triangle5X3YLeft, ref pointsSelected))
            if(TouchManager.mTouchManager.mTouchLogic.checkShapes(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().GetShpeType(), ref TouchManager.mTouchManager.pointsSelected))
            {
                GameObject curShape = new GameObject();
                GameObject firstPoint = new GameObject();

                curShape = TouchManager.mTouchManager.GetCurrentShape();
                firstPoint = TouchManager.mTouchManager.pointsSelected[0];

                Debug.Log("..........." +curShape.name);
                Debug.Log("***********" + firstPoint.name);

                AnimationMagager.mAnimation.ScoreAnimation( ref firstPoint, ref curShape);
                AnimationMagager.mAnimation.TimeAnimation(ref firstPoint, ref curShape);

               Debug.Log("Correct Shape");
               TouchManager.mTouchManager.DeleteCurrentShape(); //Delete current shape and Instantiate a new one


               Destroy(thisLine);

               foreach(GameObject GO in TouchManager.mTouchManager.pointsSelected)
               {
                   GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
               }

                LastShapeCorect = true;

                //Add points to score
                UIManage.instance.AddScore(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().points);

                //Add points to score
                UIManage.instance.AddTime(TouchManager.mTouchManager.GetCurrentShape().GetComponent<Shapes>().timeBonus);


                TouchManager.mTouchManager.mColliders.mCurrentShape = TouchManager.mTouchManager.GetCurrentShape();


                TouchManager.mTouchManager.mColliders.pointCount = 0;
                //Reset Collidrrs size

                //Call the winning animation or add points or ...

            }
           else
           {
               Debug.Log("Wrong Shape");

               //Destroi the line , may add some stuff in future to make player know that made mistake
               Destroy(thisLine);
               Debug.Log("GOs 2 size = " + TouchManager.mTouchManager.pointsSelected.Count.ToString());
               foreach (GameObject GO in TouchManager.mTouchManager.pointsSelected)
               {
                   GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
               }

                TouchManager.mTouchManager.pointsSelected.Clear();

                TouchManager.mTouchManager.mColliders.pointCount = 0;
                //Reset Colliders size
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

        return (Mathf.Atan2(yDistance, xDistance) * Mathf.Rad2Deg);

    }


}
