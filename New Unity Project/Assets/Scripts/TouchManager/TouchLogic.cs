using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//This class will hold functions for touch logic. No need to have Start and Update. Will be element of TouchManager


public class TouchLogic {
        
	public enum Shapes
    {
        Triangle5X3YUp,
        Triangle5X3YDown,
        Triangle5X3YRight,
        Triangle5X3YLeft,
        TriangleRectangle3DownLeft,
        TriangleRectangle3UpLeft,


        Square2x2,
        Square3x3,
        Square4x4,

        Rectangle2x3,
        Rectangle3x2,
        Rectangle3x4,
        Rectangle4x3


        //Add here all shapes of our game
    }

    public bool checkShapes(Shapes shape, ref List<GameObject> points)
    {

        Debug.Log("Shape = " + shape.ToString());

        switch(shape)
        {
            case Shapes.Triangle5X3YUp:                                     // Up means the direction that it points
                return checkTriangle5X3Y(ref points,true);
                break;
            case Shapes.Triangle5X3YDown:                                  
                return checkTriangle5X3Y(ref points, false);                // Down means the direction that it points
                break;
            case Shapes.Triangle5X3YRight:
                return checkTriangle5X3YSides(ref points, false);           // Right means the direction that it points
                break;
            case Shapes.Triangle5X3YLeft:
                return checkTriangle5X3YSides(ref points, true);           // Left means the direction that it points
                break;
            case Shapes.TriangleRectangle3DownLeft:                         // Left means the side of the 90 degree angle
                return checkTriangleRectangle3(ref points, false, true);    // Down means the position of the 90 degree angle compared with the rest
                break;
            case Shapes.TriangleRectangle3UpLeft:                        
                return checkTriangleRectangle3(ref points, true, true);
                break;

            case Shapes.Square2x2:
                return checkSquare(ref points, 2);
                break;
            case Shapes.Square3x3:
                return checkSquare(ref points, 3);
                break;
            case Shapes.Square4x4:
                return checkSquare(ref points, 4);
                break;

            case Shapes.Rectangle2x3:
                return checkRectangle(ref points, 2,3);
                break;
            case Shapes.Rectangle3x2:
                return checkRectangle(ref points, 3,2);
                break;
            case Shapes.Rectangle3x4:
                return checkRectangle(ref points, 3, 4 );
                break;
            case Shapes.Rectangle4x3:
                return checkRectangle(ref points, 4, 3);
                break;
            default:
                Debug.Log("[TouchLogic]Shape name does not exit.");
                break;
        }

        return false;
    }






    private bool checkTriangle5X3Y(ref List<GameObject> points, bool isUp)
    {
        Debug.Log("Start Triangle Check");

        float distanceBetweenPointsX = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();
        float distanceBetweenPointsY = PointsManager.mPointsManager.GetDistanceBetweenLines();

        Debug.Log(points.Count);

        //Check number of points
        if (points.Count < (8 + 1) || points.Count > (8 + 1))
        {
            return false;  
        }

        //Check if shape was closed
        if(points[0].transform.position != points[points.Count - 1].transform.position)
        {
            return false;
        }

        //Debug.Log("Correct number of points!");


        //Check for UP

        List<GameObject> Line1 = new List<GameObject>();
        List<GameObject> Line2 = new List<GameObject>();
        List<GameObject> Line3 = new List<GameObject>();

       // Debug.Log("points = " + points.Count);

        for(int i = 0; i < (points.Count - 1); ++i )
            {
                Debug.Log("Line 1 count = " + Line1.Count );

                if (Line1.Count == 0)
                {
                    Line1.Add(points[i]);
                    Debug.Log("Line 1");
                }
                else
                {
                    if(points[i].transform.position.y == Line1[0].transform.position.y)
                    {
                        Line1.Add(points[i]);
                    }
                    else
                    {
                        if(Line2.Count == 0)
                        {
                            Line2.Add(points[i]);
                        }
                        else
                        {
                            if (points[i].transform.position.y == Line2[0].transform.position.y)
                            {
                                Line2.Add(points[i]);
                            }
                            else
                            {
                                if(Line3.Count == 0)
                                {
                                    Line3.Add(points[i]);
                                }
                                else
                                {
                                    if (points[i].transform.position.y == Line3[0].transform.position.y)
                                    {
                                        Line3.Add(points[i]);
                                    }
                                    else
                                    {
                                        //Wrong shape
                                        return false;
                                    }
                                }

                            }

                        }


                    }


                }


            }
        //Debug.Log("Line1 = " + Line1.Count.ToString() + "     Line2 = " + Line2.Count.ToString() + "    Line3 = " + Line3.Count.ToString());



        //Check num of Points in each line 
        if ((Line1.Count == 5 && Line2.Count == 2 && Line3.Count == 1) ||
            (Line1.Count == 5 && Line2.Count == 1 && Line3.Count == 2) ||
             (Line1.Count == 2 && Line2.Count == 5 && Line3.Count == 1) ||
             (Line1.Count == 1 && Line2.Count == 5 && Line3.Count == 2) ||
             (Line1.Count == 2 && Line2.Count == 1 && Line3.Count == 5) ||
             (Line1.Count == 1 && Line2.Count == 2 && Line3.Count == 5))
        {

           

            List<List<GameObject>> LinesList = new List<List<GameObject>>();

            Line1.Sort(sortLine);
            Line2.Sort(sortLine);
            Line3.Sort(sortLine);

            if (Line1.Count == 5)
            {
                if (Line2.Count == 2)
                {
                    LinesList.Add(Line1);
                    LinesList.Add(Line2);
                    LinesList.Add(Line3);
                }
                else if (Line3.Count == 2)
                {
                    LinesList.Add(Line1);
                    LinesList.Add(Line3);
                    LinesList.Add(Line2);
                }
                else
                {
                    Debug.Assert(false, "[TouchLogic] Lists with problems.");
                }
            }
            else if (Line2.Count == 5)
            {
                if (Line1.Count == 2)
                {
                    LinesList.Add(Line2);
                    LinesList.Add(Line1);
                    LinesList.Add(Line3);
                }
                else if (Line3.Count == 2)
                {
                    LinesList.Add(Line2);
                    LinesList.Add(Line3);
                    LinesList.Add(Line1);
                }
                else
                {
                    Debug.Assert(false, "[TouchLogic] Lists with problems.");
                }

            }
            else if (Line3.Count == 5)
            {
                if (Line1.Count == 2)
                {
                    LinesList.Add(Line3);
                    LinesList.Add(Line1);
                    LinesList.Add(Line2);
                }
                else if (Line2.Count == 2)
                {
                    LinesList.Add(Line3);
                    LinesList.Add(Line2);
                    LinesList.Add(Line1);
                }
                else
                {
                    Debug.Assert(false, "[TouchLogic] Lists with problems.");
                }

            }


            if (isUp == true)
            {
                //Check if Triangle is Up
                if (LinesList[0][0].transform.position.y > LinesList[1][0].transform.position.y ||
                    LinesList[0][0].transform.position.y > LinesList[2][0].transform.position.y ||
                    LinesList[1][0].transform.position.y > LinesList[2][0].transform.position.y)
                {
                    return false;
                }
                //Debug.Log("First Check");

                //Check Bottom X distances
                if ((LinesList[0][1].transform.position.x - LinesList[0][0].transform.position.x) != distanceBetweenPointsX ||
                    (LinesList[0][2].transform.position.x - LinesList[0][1].transform.position.x) != distanceBetweenPointsX ||
                    (LinesList[0][3].transform.position.x - LinesList[0][2].transform.position.x) != distanceBetweenPointsX ||
                    (LinesList[0][4].transform.position.x - LinesList[0][3].transform.position.x) != distanceBetweenPointsX)
                {
                    return false;
                }
                //Debug.Log("Last Check - 2");


                //Check Middle X distances
                if ((LinesList[1][1].transform.position.x - LinesList[1][0].transform.position.x) != distanceBetweenPointsX * 2 )
                {
                    return false;
                }

                //Debug.Log("Last Check - 1");
                //
                //
                //Debug.Log("1   = " + (LinesList[1][0].transform.position.y - LinesList[0][1].transform.position.y).ToString());
                //Debug.Log("1   = " + distanceBetweenPointsY.ToString());
                //
                //Debug.Log("2   = " + (LinesList[1][1].transform.position.y - LinesList[0][3].transform.position.y).ToString());
                //Debug.Log("2   = " + distanceBetweenPointsY.ToString());



                //Check Line1 and Line2 points pos
                if ((LinesList[1][0].transform.position.x != LinesList[0][1].transform.position.x) ||
                       (LinesList[1][1].transform.position.x != LinesList[0][3].transform.position.x) ||
                       ((LinesList[1][0].transform.position.y - LinesList[0][1].transform.position.y) != distanceBetweenPointsY) ||
                       ((LinesList[1][1].transform.position.y - LinesList[0][3].transform.position.y) != distanceBetweenPointsY))
                {
                    return false;
                }

                //Debug.Log("Last Check");

                if ((LinesList[2][0].transform.position.x != LinesList[0][2].transform.position.x) ||
                       ((LinesList[2][0].transform.position.y - LinesList[0][2].transform.position.y) != distanceBetweenPointsY*2))
                {
                    return false;
                }

               
            }
            else   //Down
            {
                //Check if Triangle is Down
                if (LinesList[0][0].transform.position.y < LinesList[1][0].transform.position.y ||
                    LinesList[0][0].transform.position.y < LinesList[2][0].transform.position.y ||
                    LinesList[1][0].transform.position.y < LinesList[2][0].transform.position.y)
                {
                    return false;
                }
                //Debug.Log("First Check");

                //Check Top X distances
                if ((LinesList[0][1].transform.position.x - LinesList[0][0].transform.position.x) != distanceBetweenPointsX ||
                    (LinesList[0][2].transform.position.x - LinesList[0][1].transform.position.x) != distanceBetweenPointsX ||
                    (LinesList[0][3].transform.position.x - LinesList[0][2].transform.position.x) != distanceBetweenPointsX ||
                    (LinesList[0][4].transform.position.x - LinesList[0][3].transform.position.x) != distanceBetweenPointsX)
                {
                    return false;
                }
                //Debug.Log("Last Check - 2");


                //Check Middle X distances
                if ((LinesList[1][1].transform.position.x - LinesList[1][0].transform.position.x) != distanceBetweenPointsX * 2)
                {
                    return false;
                }

                //Debug.Log("Last Check - 1");
                //
                //
                //Debug.Log("1   = " + (LinesList[1][0].transform.position.y - LinesList[0][1].transform.position.y).ToString());
                //Debug.Log("1   = " + distanceBetweenPointsY.ToString());
                //
                //Debug.Log("2   = " + (LinesList[1][1].transform.position.y - LinesList[0][3].transform.position.y).ToString());
                //Debug.Log("2   = " + distanceBetweenPointsY.ToString());



                //Check Line1 and Line2 points pos
                if ((LinesList[1][0].transform.position.x != LinesList[0][1].transform.position.x) ||
                       (LinesList[1][1].transform.position.x != LinesList[0][3].transform.position.x) ||
                       ((LinesList[0][1].transform.position.y - LinesList[1][0].transform.position.y) != distanceBetweenPointsY) ||
                       ((LinesList[0][3].transform.position.y - LinesList[1][1].transform.position.y) != distanceBetweenPointsY))
                {
                    return false;
                }

                //Debug.Log("Last Check");

                //Check Line1 and Line3 points pos
                if ((LinesList[2][0].transform.position.x != LinesList[0][2].transform.position.x) ||
                       ((LinesList[0][2].transform.position.y - LinesList[2][0].transform.position.y) != distanceBetweenPointsY * 2))
                {
                    return false;
                }
                
            }

            Debug.Log("Correct Shape");

            return true;
        }

        return false;


    }

    private bool checkTriangle5X3YSides(ref List<GameObject> points, bool isLeft)
    {
        Debug.Log("Start Triangle Check");

        float distanceBetweenPointsX = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();
        float distanceBetweenPointsY = PointsManager.mPointsManager.GetDistanceBetweenLines();

        Debug.Log(points.Count);

        //Check number of points
        if (points.Count < (8 + 1) || points.Count > (8 + 1))
        {
            return false;
        }

        //Check if shape was closed
        if (points[0].transform.position != points[points.Count - 1].transform.position)
        {
            return false;
        }

        //Debug.Log("Correct number of points!");


        //Check for Side

        List<GameObject> Line1 = new List<GameObject>();
        List<GameObject> Line2 = new List<GameObject>();
        List<GameObject> Line3 = new List<GameObject>();

        // Debug.Log("points = " + points.Count);

        for (int i = 0; i < (points.Count - 1); ++i)
        {
            Debug.Log("Line 1 count = " + Line1.Count);

            if (Line1.Count == 0)
            {
                Line1.Add(points[i]);
                Debug.Log("Line 1");
            }
            else
            {
                if (points[i].transform.position.x == Line1[0].transform.position.x)
                {
                    Line1.Add(points[i]);
                }
                else
                {
                    if (Line2.Count == 0)
                    {
                        Line2.Add(points[i]);
                    }
                    else
                    {
                        if (points[i].transform.position.x == Line2[0].transform.position.x)
                        {
                            Line2.Add(points[i]);
                        }
                        else
                        {
                            if (Line3.Count == 0)
                            {
                                Line3.Add(points[i]);
                            }
                            else
                            {
                                if (points[i].transform.position.x == Line3[0].transform.position.x)
                                {
                                    Line3.Add(points[i]);
                                }
                                else
                                {
                                    //Wrong shape
                                    return false;
                                }
                            }

                        }

                    }


                }


            }


        }
        Debug.Log("Line1 = " + Line1.Count.ToString() + "     Line2 = " + Line2.Count.ToString() + "    Line3 = " + Line3.Count.ToString());



        //Check num of Points in each line 
        if ((Line1.Count == 5 && Line2.Count == 2 && Line3.Count == 1) ||
            (Line1.Count == 5 && Line2.Count == 1 && Line3.Count == 2) ||
             (Line1.Count == 2 && Line2.Count == 5 && Line3.Count == 1) ||
             (Line1.Count == 1 && Line2.Count == 5 && Line3.Count == 2) ||
             (Line1.Count == 2 && Line2.Count == 1 && Line3.Count == 5) ||
             (Line1.Count == 1 && Line2.Count == 2 && Line3.Count == 5))
        {

            

            List<List<GameObject>> LinesList = new List<List<GameObject>>();

            Line1.Sort(sortLineY);
            Line2.Sort(sortLineY);
            Line3.Sort(sortLineY);

            if (Line1.Count == 5)
            {
                if (Line2.Count == 2)
                {
                    LinesList.Add(Line1);
                    LinesList.Add(Line2);
                    LinesList.Add(Line3);
                }
                else if (Line3.Count == 2)
                {
                    LinesList.Add(Line1);
                    LinesList.Add(Line3);
                    LinesList.Add(Line2);
                }
                else
                {
                    Debug.Assert(false, "[TouchLogic] Lists with problems.");
                }
            }
            else if (Line2.Count == 5)
            {
                if (Line1.Count == 2)
                {
                    LinesList.Add(Line2);
                    LinesList.Add(Line1);
                    LinesList.Add(Line3);
                }
                else if (Line3.Count == 2)
                {
                    LinesList.Add(Line2);
                    LinesList.Add(Line3);
                    LinesList.Add(Line1);
                }
                else
                {
                    Debug.Assert(false, "[TouchLogic] Lists with problems.");
                }

            }
            else if (Line3.Count == 5)
            {
                if (Line1.Count == 2)
                {
                    LinesList.Add(Line3);
                    LinesList.Add(Line1);
                    LinesList.Add(Line2);
                }
                else if (Line2.Count == 2)
                {
                    LinesList.Add(Line3);
                    LinesList.Add(Line2);
                    LinesList.Add(Line1);
                }
                else
                {
                    Debug.Assert(false, "[TouchLogic] Lists with problems.");
                }

            }

            //Right
            if (isLeft == false)
            {

                Debug.Log("Line1 x  = " + LinesList[0][0].transform.position.x.ToString());
                Debug.Log("Line2 x  = " + LinesList[1][0].transform.position.x.ToString());
                Debug.Log("Line3 x  = " + LinesList[2][0].transform.position.x.ToString());


                //Check if Triangle is Right
                if (LinesList[0][0].transform.position.x > LinesList[1][0].transform.position.x ||
                    LinesList[0][0].transform.position.x > LinesList[2][0].transform.position.x ||
                    LinesList[1][0].transform.position.x > LinesList[2][0].transform.position.x)
                {
                    return false;
                }
                Debug.Log("First Check");

                //Check Left Y distances
                if ((LinesList[0][1].transform.position.y - LinesList[0][0].transform.position.y) != distanceBetweenPointsY ||
                    (LinesList[0][2].transform.position.y - LinesList[0][1].transform.position.y) != distanceBetweenPointsY ||
                    (LinesList[0][3].transform.position.y - LinesList[0][2].transform.position.y) != distanceBetweenPointsY ||
                    (LinesList[0][4].transform.position.y - LinesList[0][3].transform.position.y) != distanceBetweenPointsY)
                {
                    return false;
                }
                Debug.Log("Last Check - 2");


                //Check Middle Y distances
                if ((LinesList[1][1].transform.position.y - LinesList[1][0].transform.position.y) != distanceBetweenPointsY * 2)
                {
                    return false;
                }

                Debug.Log("Last Check - 1");
                //
                //
                //Debug.Log("1   = " + (LinesList[1][0].transform.position.y - LinesList[0][1].transform.position.y).ToString());
                //Debug.Log("1   = " + distanceBetweenPointsY.ToString());
                //
                //Debug.Log("2   = " + (LinesList[1][1].transform.position.y - LinesList[0][3].transform.position.y).ToString());
                //Debug.Log("2   = " + distanceBetweenPointsY.ToString());



                //Check Line1 and Line2 points pos
                if ((LinesList[1][0].transform.position.y != LinesList[0][1].transform.position.y) ||
                       (LinesList[1][1].transform.position.y != LinesList[0][3].transform.position.y) ||
                       ((LinesList[1][0].transform.position.x - LinesList[0][1].transform.position.x) != distanceBetweenPointsX) ||
                       ((LinesList[1][1].transform.position.x - LinesList[0][3].transform.position.x) != distanceBetweenPointsX))
                {
                    return false;
                }

                Debug.Log("Last Check");

                if ((LinesList[2][0].transform.position.y != LinesList[0][2].transform.position.y) ||
                       ((LinesList[2][0].transform.position.x - LinesList[0][2].transform.position.x) != distanceBetweenPointsX * 2))
                {
                    return false;
                }


            }
            else
            {
                //Check if Triangle is Left
                if (LinesList[0][0].transform.position.x < LinesList[1][0].transform.position.x ||
                    LinesList[0][0].transform.position.x < LinesList[2][0].transform.position.x ||
                    LinesList[1][0].transform.position.x < LinesList[2][0].transform.position.x)
                {
                    return false;
                }
                Debug.Log("First Check");

                //Check Right Y distances
                if ((LinesList[0][1].transform.position.y - LinesList[0][0].transform.position.y) != distanceBetweenPointsY ||
                    (LinesList[0][2].transform.position.y - LinesList[0][1].transform.position.y) != distanceBetweenPointsY ||
                    (LinesList[0][3].transform.position.y - LinesList[0][2].transform.position.y) != distanceBetweenPointsY ||
                    (LinesList[0][4].transform.position.y - LinesList[0][3].transform.position.y) != distanceBetweenPointsY)
                {
                    return false;
                }
                //Debug.Log("Last Check - 2");


                //Check Middle Y distances
                if ((LinesList[1][1].transform.position.y - LinesList[1][0].transform.position.y) != distanceBetweenPointsY * 2)
                {
                    return false;
                }

                //Debug.Log("Last Check - 1");
                //
                //
                //Debug.Log("1   = " + (LinesList[1][0].transform.position.y - LinesList[0][1].transform.position.y).ToString());
                //Debug.Log("1   = " + distanceBetweenPointsY.ToString());
                //
                //Debug.Log("2   = " + (LinesList[1][1].transform.position.y - LinesList[0][3].transform.position.y).ToString());
                //Debug.Log("2   = " + distanceBetweenPointsY.ToString());



                //Check Line1 and Line2 points pos
                if ((LinesList[1][0].transform.position.y != LinesList[0][1].transform.position.y) ||
                       (LinesList[1][1].transform.position.y != LinesList[0][3].transform.position.y) ||
                       ((LinesList[0][1].transform.position.x - LinesList[1][0].transform.position.x) != distanceBetweenPointsX) ||
                       ((LinesList[0][3].transform.position.x - LinesList[1][1].transform.position.x) != distanceBetweenPointsX))
                {
                    return false;
                }

                //Debug.Log("Last Check");

                if ((LinesList[2][0].transform.position.y != LinesList[0][2].transform.position.y) ||
                       ((LinesList[0][2].transform.position.x - LinesList[2][0].transform.position.x) != distanceBetweenPointsX * 2))
                {
                    return false;
                }
            }






        }

        return true;

    }

    private bool checkTriangleRectangle3(ref List<GameObject> points, bool isUp, bool isLeft)
    {

        Debug.Log("Start Triangle Rectangle Check");

        float distanceBetweenPointsX = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();
        float distanceBetweenPointsY = PointsManager.mPointsManager.GetDistanceBetweenLines();

        Debug.Log(points.Count);

        //Check number of points
        if (points.Count < (6 + 1) || points.Count > (6 + 1))
        {
            return false;
        }


        //Check if shape was closed
        if (points[0].transform.position != points[points.Count - 1].transform.position)
        {
            return false;
        }
        //Debug.Log("Correct number of points!");


        //Check for UP

        List<GameObject> Line1 = new List<GameObject>();
        List<GameObject> Line2 = new List<GameObject>();
        List<GameObject> Line3 = new List<GameObject>();

        // Debug.Log("points = " + points.Count);


        //Divide the points in three lines based on each point Y axis
        for (int i = 0; i < (points.Count - 1); ++i)
        {
            Debug.Log("Line 1 count = " + Line1.Count);

            if (Line1.Count == 0)
            {
                Line1.Add(points[i]);
                Debug.Log("Line 1");
            }
            else
            {
                if (points[i].transform.position.y == Line1[0].transform.position.y)
                {
                    Line1.Add(points[i]);
                }
                else
                {
                    if (Line2.Count == 0)
                    {
                        Line2.Add(points[i]);
                    }
                    else
                    {
                        if (points[i].transform.position.y == Line2[0].transform.position.y)
                        {
                            Line2.Add(points[i]);
                        }
                        else
                        {
                            if (Line3.Count == 0)
                            {
                                Line3.Add(points[i]);
                            }
                            else
                            {
                                if (points[i].transform.position.y == Line3[0].transform.position.y)
                                {
                                    Line3.Add(points[i]);
                                }
                                else
                                {
                                    //Wrong shape
                                    return false;
                                }
                            }

                        }

                    }


                }


            }


        }
        //Debug.Log("Line1 = " + Line1.Count.ToString() + "     Line2 = " + Line2.Count.ToString() + "    Line3 = " + Line3.Count.ToString());

        //Check num of Points in each line 
        if ((Line1.Count == 3 && Line2.Count == 2 && Line3.Count == 1) ||
            (Line1.Count == 3 && Line2.Count == 1 && Line3.Count == 2) ||
             (Line1.Count == 2 && Line2.Count == 3 && Line3.Count == 1) ||
             (Line1.Count == 1 && Line2.Count == 3 && Line3.Count == 2) ||
             (Line1.Count == 2 && Line2.Count == 1 && Line3.Count == 3) ||
             (Line1.Count == 1 && Line2.Count == 2 && Line3.Count == 3))
        {

            List<List<GameObject>> LinesList = new List<List<GameObject>>();

            Line1.Sort(sortLine);
            Line2.Sort(sortLine);
            Line3.Sort(sortLine);

            if (Line1.Count == 3)
            {
                if (Line2.Count == 2)
                {
                    LinesList.Add(Line1);
                    LinesList.Add(Line2);
                    LinesList.Add(Line3);
                }
                else if (Line3.Count == 2)
                {
                    LinesList.Add(Line1);
                    LinesList.Add(Line3);
                    LinesList.Add(Line2);
                }
                else
                {
                    Debug.Assert(false, "[TouchLogic] Lists with problems.");
                }
            }
            else if (Line2.Count == 3)
            {
                if (Line1.Count == 2)
                {
                    LinesList.Add(Line2);
                    LinesList.Add(Line1);
                    LinesList.Add(Line3);
                }
                else if (Line3.Count == 2)
                {
                    LinesList.Add(Line2);
                    LinesList.Add(Line3);
                    LinesList.Add(Line1);
                }
                else
                {
                    Debug.Assert(false, "[TouchLogic] Lists with problems.");
                }

            }
            else if (Line3.Count == 3)
            {
                if (Line1.Count == 2)
                {
                    LinesList.Add(Line3);
                    LinesList.Add(Line1);
                    LinesList.Add(Line2);
                }
                else if (Line2.Count == 2)
                {
                    LinesList.Add(Line3);
                    LinesList.Add(Line2);
                    LinesList.Add(Line1);
                }
                else
                {
                    Debug.Assert(false, "[TouchLogic] Lists with problems.");
                }

            }

            if(isUp == false && isLeft == true)
            {
                //Check if Triangle is Down (different of the triangle5x3)
                if (LinesList[0][0].transform.position.y > LinesList[1][0].transform.position.y ||
                    LinesList[0][0].transform.position.y > LinesList[2][0].transform.position.y ||
                    LinesList[1][0].transform.position.y > LinesList[2][0].transform.position.y)
                {
                    return false;
                }
                //Debug.Log("First Check");

                //Check if is left
                if(LinesList[0][0].transform.position.x != LinesList[1][0].transform.position.x ||
                    LinesList[0][0].transform.position.x != LinesList[2][0].transform.position.x)
                {
                    return false;
                }
                
                //Check Bottom X distances
                if ((LinesList[0][1].transform.position.x - LinesList[0][0].transform.position.x) != distanceBetweenPointsX ||
                    (LinesList[0][2].transform.position.x - LinesList[0][1].transform.position.x) != distanceBetweenPointsX)
                {
                    return false;
                }
                //Debug.Log("Last Check - 2");


                //Check Middle X distances
                if ((LinesList[1][1].transform.position.x - LinesList[1][0].transform.position.x) != distanceBetweenPointsX)
                {
                    return false;
                }

                //Debug.Log("Last Check - 1");
                //
                //
                //Debug.Log("1   = " + (LinesList[1][0].transform.position.y - LinesList[0][1].transform.position.y).ToString());
                //Debug.Log("1   = " + distanceBetweenPointsY.ToString());
                //
                //Debug.Log("2   = " + (LinesList[1][1].transform.position.y - LinesList[0][3].transform.position.y).ToString());
                //Debug.Log("2   = " + distanceBetweenPointsY.ToString());



                //Check Line1 and Line2 points pos
                if ((LinesList[1][0].transform.position.x != LinesList[0][0].transform.position.x) ||
                       (LinesList[1][1].transform.position.x != LinesList[0][1].transform.position.x) ||
                       ((LinesList[1][0].transform.position.y - LinesList[0][0].transform.position.y) != distanceBetweenPointsY) ||
                       ((LinesList[1][1].transform.position.y - LinesList[0][1].transform.position.y) != distanceBetweenPointsY))
                {
                    return false;
                }

                //Debug.Log("Last Check");

                if ((LinesList[2][0].transform.position.x != LinesList[0][0].transform.position.x) ||
                       ((LinesList[2][0].transform.position.y - LinesList[0][0].transform.position.y) != distanceBetweenPointsY * 2))
                {
                    return false;
                }
                Debug.Log("Correct Shape!");

            }
            else if (isUp == true && isLeft == true)
            {
                //Check if Triangle is Up
                if (LinesList[0][0].transform.position.y < LinesList[1][0].transform.position.y ||
                    LinesList[0][0].transform.position.y < LinesList[2][0].transform.position.y ||
                    LinesList[1][0].transform.position.y < LinesList[2][0].transform.position.y)
                {
                    return false;
                }
                //Debug.Log("First Check");

                //Check if is left
                if (LinesList[0][0].transform.position.x != LinesList[1][0].transform.position.x ||
                    LinesList[0][0].transform.position.x != LinesList[2][0].transform.position.x)
                {
                    return false;
                }

                //Check Top X distances
                if ((LinesList[0][1].transform.position.x - LinesList[0][0].transform.position.x) != distanceBetweenPointsX ||
                    (LinesList[0][2].transform.position.x - LinesList[0][1].transform.position.x) != distanceBetweenPointsX)
                {
                    return false;
                }
                //Debug.Log("Last Check - 2");


                //Check Middle X distances
                if ((LinesList[1][1].transform.position.x - LinesList[1][0].transform.position.x) != distanceBetweenPointsX)
                {
                    return false;
                }

                //Debug.Log("Last Check - 1");
                //
                //
                //Debug.Log("1   = " + (LinesList[1][0].transform.position.y - LinesList[0][1].transform.position.y).ToString());
                //Debug.Log("1   = " + distanceBetweenPointsY.ToString());
                //
                //Debug.Log("2   = " + (LinesList[1][1].transform.position.y - LinesList[0][3].transform.position.y).ToString());
                //Debug.Log("2   = " + distanceBetweenPointsY.ToString());



                //Check Line1 and Line2 points pos
                if ((LinesList[1][0].transform.position.x != LinesList[0][0].transform.position.x) ||
                       (LinesList[1][1].transform.position.x != LinesList[0][1].transform.position.x) ||
                       ((LinesList[0][0].transform.position.y - LinesList[1][0].transform.position.y) != distanceBetweenPointsY) ||
                       ((LinesList[0][1].transform.position.y - LinesList[1][1].transform.position.y) != distanceBetweenPointsY))
                {
                    return false;
                }

                //Debug.Log("Last Check");

                if ((LinesList[2][0].transform.position.x != LinesList[0][0].transform.position.x) ||
                       ((LinesList[0][0].transform.position.y - LinesList[2][0].transform.position.y) != distanceBetweenPointsY * 2))
                {
                    return false;
                }
                Debug.Log("Correct Shape!");
            }




        }
        return true;

    }

    private bool checkSquare(ref List<GameObject> points, uint numSidePoints)
    {
        Debug.Log("Start Square Check");

        float distanceBetweenPointsX = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();
        float distanceBetweenPointsY = PointsManager.mPointsManager.GetDistanceBetweenLines();

        Debug.Log(points.Count);

        //Check number of points
        if (points.Count != ((4 * (numSidePoints - 1)) + 1))
        {
            return false;
        }


        //Check if shape was closed
        if (points[0].transform.position != points[points.Count - 1].transform.position)
        {
            return false;
        }

        List<List<GameObject>> Lines = new List<List<GameObject>>();


        // Debug.Log("points = " + points.Count);



        for (int i = 0; i < (points.Count - 1); ++i)
        {

            for (int j = 0; j < PointsManager.mPointsManager.numberLines; ++j)
            {
                if (Lines.Count == j)
                {
                    Lines.Add(new List<GameObject>());
                    Lines[j].Add(points[i]);
                    Debug.Log("Line " + j.ToString());
                    break;
                }
                else if (points[i].transform.position.y == Lines[j][0].transform.position.y)
                {
                    Lines[j].Add(points[i]);
                    break;
                }

            }

        }
        
        for (int i = 0; i < numSidePoints;++i)
        {
            Lines[i].Sort(sortLine);
        }

        Lines.OrderByDescending(line => line[0].transform.position.y);



        //Check num of Points in each line 
        if (Lines[0].Count != numSidePoints && Lines[(int)(numSidePoints - 1)].Count != numSidePoints)
        {
            return false;
        }
        if (numSidePoints > 2)
        {
            for (int i = 1; i < (int)(numSidePoints - 1); ++i)
            {
                if (Lines[i].Count != 2)
                {
                    return false;
                }

            }
        }

        //Necessary Checks For Y axis

        for(int i = 0; i < (Lines.Count - 1);++i)
        {
            if((Lines[i][0].transform.position.y - Lines[i + 1][0].transform.position.y) != distanceBetweenPointsY)
            {
                return false;
            }
        }


        //Necessary Checks For X axis

        //First Line

        for (int i = 0; i < (Lines[0].Count - 1);++i)
        {
            if((Lines[0][i + 1].transform.position.x - Lines[0][i].transform.position.x) != distanceBetweenPointsX)
            {
                return false;
            }
        }

        //Last Line
        for (int i = 0; i < (Lines[(int)(numSidePoints - 1)].Count - 1); ++i)
        {
            if ((Lines[0][i + 1].transform.position.x - Lines[0][i].transform.position.x) != distanceBetweenPointsX)
            {
                return false;
            }
        }

        if(numSidePoints > 2)
        {
            for (int i = 1; i < (Lines.Count - 1); ++i)
            {
                if((Lines[i][1].transform.position.x - Lines[i][0].transform.position.x) != (distanceBetweenPointsX * (numSidePoints - 1)))
                {
                    return false;
                }

            }

        }



        return true;

    }

    private bool checkRectangle(ref List<GameObject> points, uint numHorizontalPoints, uint numVerticalPoints)
    {
        Debug.Log("Start Square Check");


        Debug.Assert(numHorizontalPoints >= 2 && numVerticalPoints >= 2, "[checkRectangle] wrong size!");


        float distanceBetweenPointsX = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();
        float distanceBetweenPointsY = PointsManager.mPointsManager.GetDistanceBetweenLines();

        Debug.Log(points.Count);

        //Check number of points
        if (points.Count != (((numHorizontalPoints * 2) + ((numVerticalPoints - 2) * 2)) + 1))
        {
            return false;
        }


        //Check if shape was closed
        if (points[0].transform.position != points[points.Count - 1].transform.position)
        {
            return false;
        }

        List<List<GameObject>> Lines = new List<List<GameObject>>();


        // Debug.Log("points = " + points.Count);


        //Add points to Lines list
        for (int i = 0; i < (points.Count - 1); ++i)
        {

            for (int j = 0; j < PointsManager.mPointsManager.numberLines; ++j)
            {
                if (Lines.Count == j)
                {
                    Lines.Add(new List<GameObject>());
                    Lines[j].Add(points[i]);
                    Debug.Log("Line " + j.ToString());
                    break;
                }
                else if (points[i].transform.position.y == Lines[j][0].transform.position.y)
                {
                    Lines[j].Add(points[i]);
                    break;
                }

            }

        }

        if(Lines.Count != numVerticalPoints)
        {
            return false;
        }

        for (int i = 0; i < numVerticalPoints; ++i)
        {
            Lines[i].Sort(sortLine);
        }

        Lines.OrderByDescending(line => line[0].transform.position.y);



        //Check num of Points in each line 
        if (Lines[0].Count != numHorizontalPoints && Lines[(int)(numVerticalPoints - 1)].Count != numHorizontalPoints)
        {
            return false;
        }
        if (numVerticalPoints > 2)
        {
            for (int i = 1; i < (int)(numVerticalPoints - 1); ++i)
            {
                if (Lines[i].Count != 2)
                {
                    return false;
                }

            }
        }

        //Necessary Checks For Y axis

        for (int i = 0; i < (Lines.Count - 1); ++i)
        {
            if ((Lines[i][0].transform.position.y - Lines[i + 1][0].transform.position.y) != distanceBetweenPointsY)
            {
                return false;
            }
        }


        //Necessary Checks For X axis

        //First Line

        for (int i = 0; i < (Lines[0].Count - 1); ++i)
        {
            if ((Lines[0][i + 1].transform.position.x - Lines[0][i].transform.position.x) != distanceBetweenPointsX)
            {
                return false;
            }
        }

        //Last Line
        for (int i = 0; i < (Lines[(int)(numVerticalPoints - 1)].Count - 1); ++i)
        {
            if ((Lines[0][i + 1].transform.position.x - Lines[0][i].transform.position.x) != distanceBetweenPointsX)
            {
                return false;
            }
        }

        if (numVerticalPoints > 2)
        {
            for (int i = 1; i < (Lines.Count - 1); ++i)
            {
                if ((Lines[i][1].transform.position.x - Lines[i][0].transform.position.x) != (distanceBetweenPointsX * (numHorizontalPoints - 1)))
                {
                    return false;
                }

            }

        }



        return true;

    }


    //=========================================================================
    //========================  Support Functions  ============================
    //=========================================================================


    //Crescent order 
    private int sortLine(GameObject GO1, GameObject GO2)
    {

        if (GO1 == null)
        {
            if (GO2 == null)
            {
                // If GO1 is null and GO2 is null, they're
                // equal. 
                return 0;
            }
            else
            {
                // If GO1 is null and GO2 is not null, GO2
                // is greater. 
                return -1;
            }
        }
        else
        {
            // If GO1 is not null...
            //
            if (GO2 == null)
            // ...and GO2 is null, GO1 is greater.
            {
                return 1;
            }
            else
            {
                // ...and GO2 is not null, compare the 
                // lengths of the two strings.
                //
                if (GO1.transform.position.x > GO2.transform.position.x)
                {
                    return 1;
                }
                else if (GO1.transform.position.x < GO2.transform.position.x)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }
    }

    //Crescent order 
    private int sortLineY(GameObject GO1, GameObject GO2)
    {

        if (GO1 == null)
        {
            if (GO2 == null)
            {
                // If GO1 is null and GO2 is null, they're
                // equal. 
                return 0;
            }
            else
            {
                // If GO1 is null and GO2 is not null, GO2
                // is greater. 
                return -1;
            }
        }
        else
        {
            // If GO1 is not null...
            //
            if (GO2 == null)
            // ...and GO2 is null, GO1 is greater.
            {
                return 1;
            }
            else
            {
                // ...and GO2 is not null, compare the 
                // lengths of the two strings.
                //
                if (GO1.transform.position.y > GO2.transform.position.y)
                {
                    return 1;
                }
                else if (GO1.transform.position.y < GO2.transform.position.y)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }
    }

}
