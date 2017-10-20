using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TColliders : MonoBehaviour
{

    public GameObject mCurrentShape;
    public GameObject mCurrentPoint;
    public int pointCount;


    // Use this for initialization
    public void Initialize()
    {

        pointCount = 0;

    }

    // Update is called once per frame
    public void update()
    {

        if (mCurrentPoint != null)
        {
            switch (mCurrentShape.GetComponent<Shapes>().GetShpeType())
            {
                case TouchLogic.Shapes.Triangle5X3YUp:

                    break;



                default:
                    Debug.Assert(false, "[Colliders] Wrong Shape type!");
                    break;




            }




        }

    }


    private void ChangeCollidersSizeT5X3Up()
    {
        string linePos = mCurrentPoint.GetComponentInParent<Transform>().name.ToString().Replace("Line ", "");
        string pointPos = mCurrentPoint.GetComponentInParent<Transform>().name.ToString().Replace("Point ", "");
        if (pointCount >= 9)
        {
            ResetCollidersSize();
            return;
        }

        if (pointCount == 1)
        {
            //Top line
            if (linePos == (TPointsManager.mTPointsManager.numberLines - 1).ToString())
            {
                //Right side point
                if (pointPos == (TPointsManager.mTPointsManager.GetNumberOfHorizontalPoints() - 1).ToString())
                {

                }
                else if (pointPos == (0 + 1).ToString())// Left side point
                {


                }

            }
            else if (linePos == (TPointsManager.mTPointsManager.numberLines - 2).ToString())  //Bottom line
            {
                //Right side point
                if (pointPos == (TPointsManager.mTPointsManager.GetNumberOfHorizontalPoints() - 1).ToString())
                {

                }
                else if (pointPos == (0 + 1).ToString())// Left side point
                {


                }

            }
        }


    }

    public void ResetCollidersSize()
    {
        foreach (List<GameObject> Line in TPointsManager.mTPointsManager.points)
        {
            foreach (GameObject Point in Line)
            {
                Point.GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 1.0f);
            }

        }

    }




}
