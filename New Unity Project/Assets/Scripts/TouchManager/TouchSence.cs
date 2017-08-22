using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSence : MonoBehaviour
{



    public Material mat;
    private LineRenderer line;
    private List<Vector3> pointsList;
    private bool isTouching;
    private Vector3 touchPosition;
    private bool check;

    struct myLine
    {
        public Vector3 StartPoint;
        public Vector3 EndPoint;
    };


    // Use this for initialization
    void Start()
    {

        line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = 0;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.startColor = Color.green;
        line.endColor = Color.green;
        line.material = mat;
        line.useWorldSpace = true;
        isTouching = false;
        pointsList = new List<Vector3>();
        check = false;


    }

    // Update is called once per frame
    void Update()
    {


        if (Input.touchCount == 1)
        {
            if(check == false)
            { 
            Debug.Log("Touching");

            isTouching = true;
            line.positionCount = 0;
            pointsList.RemoveRange(0, pointsList.Count);
            line.startColor = Color.green;
            line.endColor = Color.green;
            check = true;
            }
        }
        if (Input.touchCount == 0)
        {
            isTouching = false;

            check = false;
        }


        // Drawing line when mouse is moving(presses)
        if (isTouching)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).deltaPosition.x, Input.GetTouch(0).deltaPosition.y, 0.0f));
            Debug.Log("isTouching");
            if (!pointsList.Contains(touchPosition))
            {
                Debug.Log("DrawLine");
                pointsList.Add(touchPosition);
                Debug.Log(pointsList.Count.ToString());
                line.positionCount = pointsList.Count;
                line.SetPosition(pointsList.Count - 1, (Vector3)pointsList[pointsList.Count - 1]);
                //if (isLineCollide())
                //{
                //    isMousePressed = false;
                //    line.SetColors(Color.red, Color.red);
                //}
            }
        }












        //if (Input.touchCount >= 1)
        //{
        //    Debug.Log("Touches = " + Input.touchCount.ToString());
        //
        //    if (startPos.Equals(check))
        //    {
        //        startPos.Set(Input.GetTouch(0).deltaPosition.x, Input.GetTouch(0).deltaPosition.y);
        //        Debug.Log("Update 1");
        //    }
        //    else if (endPos.Equals(check))
        //    {
        //        endPos.Set(Input.GetTouch(0).deltaPosition.x, Input.GetTouch(0).deltaPosition.y);
        //        Debug.Log("Update 2");
        //
        //        CreateLine();
        //
        //        line.SetPosition(0, new Vector3(startPos.x, startPos.y, 0.0f));
        //        line.SetPosition(1, new Vector3(endPos.x, endPos.y, 0.0f));
        //
        //        //DrawLine(startPos, endPos);
        //
        //    }
        //    else
        //    {
        //        startPos = endPos;
        //
        //        endPos.Set(Input.GetTouch(0).deltaPosition.x, Input.GetTouch(0).deltaPosition.y);
        //        Debug.Log("Update 3");
        //
        //        CreateLine();
        //
        //        line.SetPosition(0, new Vector3(startPos.normalized.x, startPos.normalized.y, 0.0f));
        //        line.SetPosition(1, new Vector3(endPos.normalized.x, endPos.normalized.y, 0.0f));
        //
        //        // DrawLine(startPos.normalized, endPos.normalized);
        //    }
        //
        //
        //
        //}
        //else
        //{
        //    startPos.Set(0, 0);
        //    endPos.Set(0, 0);
        //    Debug.Log("Rest");
        //}





    }


    public void DrawLine(Vector2 startPos, Vector2 endPos)
    {

        Debug.Log("Start = " + startPos.ToString());

        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(new Vector3(startPos.x, startPos.y, 0));
        GL.Vertex(new Vector3(endPos.x, endPos.y, 0));
        GL.End();
        GL.PopMatrix();

    }
   //void OnPostRender()
   //{
   //    //DrawLine
   //}

    void CreateLine()
    {

        line = new GameObject("Line").AddComponent<LineRenderer>();
        line.material = mat;
        line.positionCount = 2;
        line.startWidth = 0.15f;
        line.useWorldSpace = true;

    }

}
