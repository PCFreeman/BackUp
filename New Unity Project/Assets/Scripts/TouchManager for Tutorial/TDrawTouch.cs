﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TDrawTouch : MonoBehaviour {

    public GameObject linePrefab;
    public GameObject lineColliderPrefab;
    private GameObject thisLine;
    private Vector3 startPosition;
    private Plane objectPlane;
    private GameObject coll; // line collider

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
        
    }

    // Update is called once per frame
    public void update()
    {
        //ResetCollider();

        //This function can be use for Touch or mouse click
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
        {
            if(LastShapeCorect == true)
            {
                foreach (GameObject GO in TTouchManager.mTTouchManager.pointsSelected)
                {
                    GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                }

                TTouchManager.mTTouchManager.pointsSelected.Clear();
                LastShapeCorect = false;
            }

            if(thisLine == null)
            {
            thisLine = (GameObject)Instantiate(linePrefab, this.transform.position, Quaternion.identity);
            }

            Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);      //===========================================================================================================
            //Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);           //Use This for Mouse test

            float rayDistance;
            if (objectPlane.Raycast(mRay, out rayDistance))    //This check the contact of RayCast with plane and return the distance
            {
                startPosition = mRay.GetPoint(rayDistance);
            }
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || (Input.GetMouseButton(0)))
        {
            

            Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);      //===========================================================================================================
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
                    //coll = (GameObject)Instantiate(lineColliderPrefab, new Vector3(5000.0f, 0.0f, 0.0f), Quaternion.identity);
                    float distance = thisLine.transform.position.y - startPosition.y;

                    coll.transform.position = new Vector3(startPosition.x, (distance * 0.5f) + startPosition.y, startPosition.z);
                    coll.GetComponent<BoxCollider>().size = new Vector3(5.0f , distance, 1.0f);
                    coll.GetComponent<BoxCollider>().transform.rotation.eulerAngles.Set(0.0f, 0.0f, 0.0f);

                }
                else if (startPosition.x != thisLine.transform.position.x && startPosition.y == thisLine.transform.position.y)
                {
                    //Horizontal Line
                   // coll = (GameObject)Instantiate(lineColliderPrefab, new Vector3(5000.0f, 0.0f, 0.0f), Quaternion.identity);
                    float distance = thisLine.transform.position.x - startPosition.x;

                    coll.transform.position = new Vector3((distance * 0.5f) + startPosition.x, startPosition.y, startPosition.z);
                    coll.GetComponent<BoxCollider>().size = new Vector3(distance, 5.0f, 1.0f);
                    coll.GetComponent<BoxCollider>().transform.rotation.eulerAngles.Set(0.0f, 0.0f, 0.0f);

                }
                else
                {
                    float distance = GetPointsDistance(startPosition, thisLine.transform.position);
                    float distanceX = thisLine.transform.position.x - startPosition.x;
                    float distanceY = thisLine.transform.position.y - startPosition.y;
                    //coll = (GameObject)Instantiate(lineColliderPrefab, new Vector3(5000.0f, 0.0f, 0.0f), Quaternion.identity);
                    coll.transform.position = new Vector3((distanceX * 0.5f) + startPosition.x, (distanceY * 0.5f) + startPosition.y, startPosition.z);
                    coll.GetComponent<BoxCollider>().size = new Vector3(distance, 5.0f, 1.0f);


                    //Debug.Log("Rotation = " + GetRotation(startPosition, thisLine.transform.position).ToString());

                    //coll.GetComponent<BoxCollider>().transform.Rotate(0.0f, 0.0f, GetRotation(startPosition, thisLine.transform.position));
                    coll.GetComponent<BoxCollider>().transform.rotation.eulerAngles.Set(0.0f, 0.0f, GetRotation(startPosition, thisLine.transform.position));
                }
                

            }

            startPosition = thisLine.transform.position;
                        
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary))
        {
            coll.GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 1.0f);
            coll.GetComponent<BoxCollider>().transform.rotation.eulerAngles.Set(0.0f, 0.0f, 0.0f);
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
        {

            //TTouchManager.mTTouchManager.pointsSelected = LineTouch.GetCollidedObjects();
            TTouchManager.mTTouchManager.pointsSelected = TTouchManager.mTTouchManager.GetCollidedObjects();

            Debug.Log("points selected = " + TTouchManager.mTTouchManager.pointsSelected.ToString()); 
            // Check if the line makes the corect shape
            //if (TTouchManager.mTTouchManager.mTouchLogic.checkShapes(TTouchLogic.Shapes.Triangle5X3YUp, ref TTouchManager.mTTouchManager.pointsSelected))
            if(TTouchManager.mTTouchManager.mTTouchLogic.checkShapes(TTouchManager.mTTouchManager.GetCurrentShape().GetComponent<TShapes>().GetShpeType(), ref TTouchManager.mTTouchManager.pointsSelected))    ////////======================================================
            {
                GameObject curShape = new GameObject();
                GameObject firstPoint = new GameObject();

                curShape = TTouchManager.mTTouchManager.GetCurrentShape();
                firstPoint = TTouchManager.mTTouchManager.pointsSelected[0];

                Debug.Log("..........." +curShape.name);
                Debug.Log("***********" + firstPoint.name);

                AnimationMagager.mAnimation.ScoreAnimation( ref firstPoint, ref curShape);
                AnimationMagager.mAnimation.TimeAnimation(ref firstPoint, ref curShape);

               Debug.Log("Correct Shape");

               AnimationMagager.mAnimation.ShapeMoveOut(TTouchManager.mTTouchManager.GetShapesIniatialized());
               TTouchManager.mTTouchManager.DeleteCurrentShape(); //Delete current shape and Instantiate a new one


               //Destroy(thisLine);

               foreach(GameObject GO in TTouchManager.mTTouchManager.pointsSelected)
               {
                   GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
               }

                LastShapeCorect = true;

                //Add points to score
                UIManage.instance.AddScore(TTouchManager.mTTouchManager.GetCurrentShape().GetComponent<Shapes>().points);

                //Add points to score
                UIManage.instance.AddTime(TTouchManager.mTTouchManager.GetCurrentShape().GetComponent<Shapes>().timeBonus);

                //ResetCollider();

                TTouchManager.mTTouchManager.mColliders.mCurrentShape = TTouchManager.mTTouchManager.GetCurrentShape();


                TTouchManager.mTTouchManager.mColliders.pointCount = 0;
                
                //Reset Collider Pos
                coll.transform.position = new Vector3(5000.0f, 0.0f, 0.0f);
                coll.GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 1.0f);
                coll.GetComponent<BoxCollider>().transform.rotation.eulerAngles.Set(0.0f, 0.0f, 0.0f);

                //Call the winning animation or add points or ...

            }
           else
           {
               Debug.Log("Wrong Shape");

               //Destroi the line , may add some stuff in future to make player know that made mistake
               //Destroy(thisLine);
               Debug.Log("GOs 2 size = " + TTouchManager.mTTouchManager.pointsSelected.Count.ToString());
               foreach (GameObject GO in TTouchManager.mTTouchManager.pointsSelected)
               {
                   GO.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
               }

                //ResetCollider();

                TTouchManager.mTTouchManager.pointsSelected.Clear();

                TTouchManager.mTTouchManager.mColliders.pointCount = 0;
                //Reset Colliders size
                coll.transform.position = new Vector3(5000.0f, 0.0f, 0.0f);
                coll.GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 1.0f);
                coll.GetComponent<BoxCollider>().transform.rotation.eulerAngles.Set(0.0f, 0.0f, 0.0f);
            }

            Destroy(thisLine.gameObject);
        }
    }

    public void SetSelectedPoint(ref GameObject point)
    {
        Debug.Log(" -----------    "+point.name.ToString());


        TTouchManager.mTTouchManager.pointsSelected.Add(point);
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

    //private void ResetCollider()
    //{
    //    coll.transform.position = new Vector3(5000.0f, 0.0f, 0.0f);
    //    coll.GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 1.0f);
    //    coll.GetComponent<BoxCollider>().transform.Rotate(0.0f, 0.0f, 0.0f);
    //}
}
