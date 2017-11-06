using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//This class will hold functions for touch logic. No need to have Start and Update. Will be element of TouchManager

public class TouchLogic
{

    public enum Shapes
    {
        Triangle5X3YUp,
        Triangle5X3YDown,
        Triangle5X3YRight,
        Triangle5X3YLeft,

        Triangle3X2Up,
        Triangle3X2Down,
        Triangle3X2Right,
        Triangle3X2Left,

        TriangleRectangle3DownLeft,
        TriangleRectangle3UpLeft,
        TriangleRectangle3UpRight,
        TriangleRectangle3DownRight,

        TriangleRectangle4DownLeft,
        TriangleRectangle4UpLeft,
        TriangleRectangle4UpRight,
        TriangleRectangle4DownRight,

        TriangleRectangle5DownLeft,
        TriangleRectangle5UpLeft,
        TriangleRectangle5UpRight,
        TriangleRectangle5DownRight,


        Square2x2,
        Square3x3,
        Square4x4,
        Square5x5,

        Rectangle2x3,
        Rectangle3x2,
        Rectangle3x4,
        Rectangle4x3,

        Diamond2x2,
        Diamond3x3,

<<<<<<< HEAD
        LShapeBottomRight,
        LShapeBottomLeft,
        LShapeTopRight,
        LShapeTopLeft
=======
        CShapeRight,
        CShapeUp,
        CShapeLeft,
        CShapeDown
>>>>>>> 43f510aa2ec479c2dba3cbd5d78d08db5810c8b4


        //Add here all shapes of our game
    }

    public bool checkShapes(Shapes shape, ref List<GameObject> points)
    {

        Debug.Log("Shape = " + shape.ToString());

        switch (shape)
        {
            case Shapes.Triangle5X3YUp:                                     // Points Down
                return CheckIsoscelesTriangleVertical(ref points, true, 5, 3);
                break;
            case Shapes.Triangle5X3YDown:
                return CheckIsoscelesTriangleVertical(ref points, false, 5, 3);  // Points Up
                break;
            case Shapes.Triangle5X3YRight:
                return CheckIsoscelesTriangleHorizontal(ref points, false, 5, 3);          // Points Left
                break;
            case Shapes.Triangle5X3YLeft:
                return CheckIsoscelesTriangleHorizontal(ref points, true, 5, 3);           // Points Right
                break;

            case Shapes.Triangle3X2Up:                                     // Points Down
                return CheckIsoscelesTriangleVertical(ref points, true, 3, 2);
                break;
            case Shapes.Triangle3X2Down:
                return CheckIsoscelesTriangleVertical(ref points, false, 3, 2);  // Points Up
                break;
            case Shapes.Triangle3X2Right:
                return CheckIsoscelesTriangleHorizontal(ref points, false, 3, 2);           // Points Left
                break;
            case Shapes.Triangle3X2Left:
                return CheckIsoscelesTriangleHorizontal(ref points, true, 3, 2);           // Points Right
                break;

            case Shapes.TriangleRectangle3DownLeft:                         // Left means the side of the 90 degree angle
                return CheckTriangleRectangle(ref points, false, true, 3);    // Down means the position of the 90 degree angle compared with the rest
                break;
            case Shapes.TriangleRectangle3UpLeft:
                return CheckTriangleRectangle(ref points, true, true, 3);
                break;
            case Shapes.TriangleRectangle3UpRight:
                return CheckTriangleRectangle(ref points, true, false, 3);
                break;
            case Shapes.TriangleRectangle3DownRight:
                return CheckTriangleRectangle(ref points, false, false, 3);
                break;

            case Shapes.TriangleRectangle4DownLeft:                         // Left means the side of the 90 degree angle
                return CheckTriangleRectangle(ref points, false, true, 4);    // Down means the position of the 90 degree angle compared with the rest
                break;
            case Shapes.TriangleRectangle4UpLeft:
                return CheckTriangleRectangle(ref points, true, true, 4);
                break;
            case Shapes.TriangleRectangle4UpRight:
                return CheckTriangleRectangle(ref points, true, false, 4);
                break;
            case Shapes.TriangleRectangle4DownRight:
                return CheckTriangleRectangle(ref points, false, false, 4);
                break;

            case Shapes.TriangleRectangle5DownLeft:                         // Left means the side of the 90 degree angle
                return CheckTriangleRectangle(ref points, false, true, 5);    // Down means the position of the 90 degree angle compared with the rest
                break;
            case Shapes.TriangleRectangle5UpLeft:
                return CheckTriangleRectangle(ref points, true, true, 5);
                break;
            case Shapes.TriangleRectangle5UpRight:
                return CheckTriangleRectangle(ref points, true, false, 5);
                break;
            case Shapes.TriangleRectangle5DownRight:
                return CheckTriangleRectangle(ref points, false, false, 5);
                break;

            case Shapes.Square2x2:
                return CheckSquare(ref points, 2);
                break;
            case Shapes.Square3x3:
                return CheckSquare(ref points, 3);
                break;
            case Shapes.Square4x4:
                return CheckSquare(ref points, 4);
                break;

            case Shapes.Rectangle2x3:
                return CheckRectangle(ref points, 2, 3);
                break;
            case Shapes.Rectangle3x2:
                return CheckRectangle(ref points, 3, 2);
                break;
            case Shapes.Rectangle3x4:
                return CheckRectangle(ref points, 3, 4);
                break;
            case Shapes.Rectangle4x3:
                return CheckRectangle(ref points, 4, 3);
                break;

<<<<<<< HEAD
            case Shapes.LShapeBottomRight:
                return CheckLShape(ref points, false, false);
                break;
            case Shapes.LShapeBottomLeft:
                return CheckLShape(ref points, false, true);
                break;
            case Shapes.LShapeTopRight:
                return CheckLShape(ref points, true, false);
                break;
            case Shapes.LShapeTopLeft:
                return CheckLShape(ref points, true, true);
=======
            //Add all C shapes - Peter
            case Shapes.CShapeRight:
            case Shapes.CShapeLeft:
                return CheckCShapeSide(ref points);
>>>>>>> 43f510aa2ec479c2dba3cbd5d78d08db5810c8b4
                break;
            default:
                Debug.Log("[TouchLogic]Shape name does not exit.");
                break;
        }

        return false;
    }


    private bool CheckIsoscelesTriangleVertical(ref List<GameObject> points, bool isUp, int numBaseDots, int numHeightDots)
    {

        float distanceBetweenPoints = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();

        //Check number of points
        if (points.Count < ((numBaseDots + (numBaseDots - 2)) + 1) || points.Count > ((numBaseDots + (numBaseDots - 2)) + 1))
        {
            return false;
        }

        //Check if shape was closed
        if (points[0].transform.position != points[points.Count - 1].transform.position)
        {
            return false;
        }

        List<List<GameObject>> Lines = new List<List<GameObject>>();

        for (int i = 0; i < numHeightDots; ++i)
        {
            Lines.Add(new List<GameObject>());
        }

        int lineIndex = 0;
        for (int i = 0; i < (points.Count - 1); ++i)
        {
            if (Lines[0].Count == 0)
            {
                Lines[lineIndex++].Add(points[i]);
            }
            else
            {
                bool check = false;
                foreach (List<GameObject> line in Lines)
                {
                    if (line.Count == 0)
                    {
                        break;
                    }
                    if (line[0].transform.position.y == points[i].transform.position.y)
                    {
                        line.Add(points[i]);
                        check = true;
                        break;
                    }
                }

                if (check == false)
                {
                    Lines[lineIndex++].Add(points[i]);
                }
            }

        }

        Lines.Sort(sortList);


        for (int i = 0; i < Lines.Count - 1; ++i)
        {
            int check = 2;
            if (i == 0)
            {
                check = numBaseDots;
            }
            if (i == Lines.Count - 1)
            {
                check = 1;
            }

            if (Lines[i].Count != check)
            {
                return false;
            }
        }


        //Sort all lines
        foreach (List<GameObject> line in Lines)
        {
            line.Sort(sortLine);
        }

        if (isUp) //Pointing down
        {

            for (int i = 0; i < Lines.Count - 1; ++i)
            {
                //Check if Triangle is Down 
                if (Lines[i][0].transform.position.y < Lines[i + 1][0].transform.position.y)
                {
                    return false;
                }
            }

            //Check dots y distance
            bool check = true;
            for (int i = 1, j = 1, multiplierCheck = (int)(numBaseDots * 0.5f); i < numBaseDots - 1; ++i)
            {
                if (check == true)
                {
                    if ((Lines[0][i].transform.position.y - Lines[j][0].transform.position.y) != (distanceBetweenPoints * j))
                    {
                        return false;
                    }

                    if (j == multiplierCheck)
                    {
                        check = false;
                        --j;
                    }
                    else
                    {
                        ++j;
                    }
                }
                else
                {
                    if ((Lines[0][i].transform.position.y - Lines[j][1].transform.position.y) != (distanceBetweenPoints * j))
                    {
                        return false;
                    }

                    --j;
                }
            }

            //Check dots x distance
            for (int i = 0, distanceMultiplier = numBaseDots - 3; i < Lines.Count - 1; ++i)
            {

                for (int j = 0; j < Lines[i].Count - 1; ++j)
                {
                    int multiplier = 1;
                    if (i > 0)
                    {
                        multiplier = distanceMultiplier;
                    }

                    if ((Lines[i][j + 1].transform.position.x - Lines[i][j].transform.position.x) != (distanceBetweenPoints * multiplier))
                    {
                        return false;
                    }



                    if (multiplier > 1)
                    {
                        distanceMultiplier -= 2;
                    }

                }


            }

        }
        else //Pointing Up
        {

            for (int i = 0; i < Lines.Count - 1; ++i)
            {
                //Check if Triangle is Up 
                if (Lines[i][0].transform.position.y > Lines[i + 1][0].transform.position.y)
                {
                    return false;
                }
            }

            //Check dots y distance
            bool check = true;
            for (int i = 1, j = 1, multiplierCheck = (int)(numBaseDots * 0.5f); i < numBaseDots - 1; ++i)
            {
                if (check == true)
                {
                    if ((Lines[j][0].transform.position.y - Lines[0][i].transform.position.y) != (distanceBetweenPoints * j))
                    {
                        return false;
                    }

                    if (j == multiplierCheck)
                    {
                        check = false;
                        --j;
                    }
                    else
                    {
                        ++j;
                    }
                }
                else
                {
                    if ((Lines[j][1].transform.position.y - Lines[0][i].transform.position.y) != (distanceBetweenPoints * j))
                    {
                        return false;
                    }

                    --j;
                }
            }


            //Check dots x distance
            for (int i = 0, distanceMultiplier = numBaseDots - 3; i < Lines.Count - 1; ++i)
            {

                for (int j = 0; j < Lines[i].Count - 1; ++j)
                {
                    int multiplier = 1;
                    if (i > 0)
                    {
                        multiplier = distanceMultiplier;
                    }

                    if ((Lines[i][j + 1].transform.position.x - Lines[i][j].transform.position.x) != (distanceBetweenPoints * multiplier))
                    {
                        return false;
                    }

                    if (multiplier > 1)
                    {
                        distanceMultiplier -= 2;
                    }

                }


            }

        }



        return true;
    }

    private bool CheckIsoscelesTriangleHorizontal(ref List<GameObject> points, bool isLeft, int numBaseDots, int numHeightDots)
    {

        float distanceBetweenPoints = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();

        //Check number of points
        if (points.Count < ((numBaseDots + (numBaseDots - 2)) + 1) || points.Count > ((numBaseDots + (numBaseDots - 2)) + 1))
        {
            return false;
        }

        //Check if shape was closed
        if (points[0].transform.position != points[points.Count - 1].transform.position)
        {
            return false;
        }

        List<List<GameObject>> Lines = new List<List<GameObject>>();

        new List<List<GameObject>>();

        for (int i = 0; i < numBaseDots; ++i)
        {
            Lines.Add(new List<GameObject>());
        }

        int lineIndex = 0;
        for (int i = 0; i < (points.Count - 1); ++i)
        {
            if (Lines[0].Count == 0)
            {
                Lines[lineIndex++].Add(points[i]);
            }
            else
            {
                bool check = false;
                foreach (List<GameObject> line in Lines)
                {
                    if (line.Count == 0)
                    {
                        break;
                    }
                    if (line[0].transform.position.y == points[i].transform.position.y)
                    {
                        line.Add(points[i]);
                        check = true;
                        break;
                    }
                }

                if (check == false)
                {
                    Lines[lineIndex++].Add(points[i]);
                }
            }

        }

        Lines.Sort(sortListY);


        for (int i = 0; i < Lines.Count - 1; ++i)
        {
            int check = 2;
            if (i == 0 || i == (Lines.Count - 1))
            {
                check = 1;
            }

            if (Lines[i].Count != check)
            {
                return false;
            }
        }


        //Sort all lines
        foreach (List<GameObject> line in Lines)
        {
            line.Sort(sortLine);
        }

        if (isLeft) //Pointing right
        {

            // Check X pos
            bool check = true;
            for (int i = 0, j = 1, multiplierCheck = (int)(numBaseDots * 0.5f); i < Lines.Count - 1; ++i)
            {
                //Check if Triangle is right 
                if (Lines[i][0].transform.position.x != Lines[i + 1][0].transform.position.x)
                {
                    return false;
                }

                //Check X distance
                if (i != 0 && i != Lines.Count - 2)
                {
                    if (check == true)
                    {
                        if ((Lines[i][1].transform.position.x - Lines[i][0].transform.position.x) != (distanceBetweenPoints * j))
                        {
                            return false;
                        }

                        if (j == multiplierCheck)
                        {
                            check = false;
                            --j;
                        }
                        else
                        {
                            ++j;
                        }
                    }
                    else
                    {
                        if ((Lines[i][1].transform.position.x - Lines[i][0].transform.position.x) != (distanceBetweenPoints * j))
                        {
                            return false;
                        }

                        --j;
                    }


                }

            }

            //Compare Y distance
            for (int i = 1, j = numBaseDots - 3; i < (int)(numBaseDots * 0.5f); ++i, j -= 2)
            {
                if ((Lines[i][1].transform.position.y - Lines[(Lines.Count - 1) - i][1].transform.position.y) != distanceBetweenPoints * j)
                {
                    return false;
                }

            }
        }
        else //Pointing Left
        {

            // Check X pos
            bool check = true;
            for (int i = 0, j = 1, multiplierCheck = (int)(numBaseDots * 0.5f); i < Lines.Count - 1; ++i)
            {
                //Check if Triangle is left 
                if (i == Lines.Count - 2)
                {
                    if (Lines[i][1].transform.position.x != Lines[i + 1][0].transform.position.x)
                    {
                        return false;
                    }
                }
                else if (i == 0)
                {
                    if (Lines[i][0].transform.position.x != Lines[i + 1][1].transform.position.x)
                    {
                        return false;
                    }

                }
                else
                {
                    if (Lines[i][1].transform.position.x != Lines[i + 1][1].transform.position.x)
                    {
                        return false;
                    }
                }


                //Check X distance
                if (i != 0 && i != Lines.Count - 2)
                {
                    if (check == true)
                    {
                        if ((Lines[i][1].transform.position.x - Lines[i][0].transform.position.x) != (distanceBetweenPoints * j))
                        {
                            return false;
                        }

                        if (j == multiplierCheck)
                        {
                            check = false;
                            --j;
                        }
                        else
                        {
                            ++j;
                        }
                    }
                    else
                    {
                        if ((Lines[i][1].transform.position.x - Lines[i][0].transform.position.x) != (distanceBetweenPoints * j))
                        {
                            return false;
                        }

                        --j;
                    }


                }

            }

        }

        //Compare Y distance
        for (int i = 1, j = numBaseDots - 3; i < (int)(numBaseDots * 0.5f); ++i, j -= 2)
        {
            if ((Lines[i][0].transform.position.y - Lines[(Lines.Count - 1) - i][0].transform.position.y) != distanceBetweenPoints * j)
            {
                return false;
            }

        }

        return true;
    }

    private bool CheckTriangleRectangle(ref List<GameObject> points, bool isUp, bool isLeft, int numDots)
    {

        Debug.Log("Start Triangle Rectangle Check");

        float distanceBetweenPointsX = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();
        float distanceBetweenPointsY = PointsManager.mPointsManager.GetDistanceBetweenLines();

        Debug.Log(points.Count);

        int totalNumDots = numDots + 1;

        for (int i = 1; i < numDots - 1; ++i)
        {
            totalNumDots += 2;
        }

        //Check number of points
        if (points.Count < (totalNumDots + 1) || points.Count > (totalNumDots + 1))
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
        List<List<GameObject>> Lines = new List<List<GameObject>>();

        for (int i = 0; i < numDots; ++i)
        {
            Lines.Add(new List<GameObject>());
        }

        int lineIndex = 0;
        for (int i = 0; i < (points.Count - 1); ++i)
        {
            if (Lines[0].Count == 0)
            {
                Lines[lineIndex++].Add(points[i]);
            }
            else
            {
                bool check = false;
                foreach (List<GameObject> line in Lines)
                {
                    if (line.Count == 0)
                    {
                        break;
                    }
                    if (line[0].transform.position.y == points[i].transform.position.y)
                    {
                        line.Add(points[i]);
                        check = true;
                        break;
                    }
                }

                if (check == false)
                {
                    Lines[lineIndex++].Add(points[i]);
                }
            }

        }

        if (isUp)
        {
            Lines.Sort(sortListUp);
        }
        else
        {
            Lines.Sort(sortListDown);
        }


        for (int i = 0; i < Lines.Count - 1; ++i)
        {
            if (i == 0 && Lines[i].Count != numDots)
            {
                return false;
            }
            else if (i == (numDots - 1) && Lines[i].Count != 1)
            {
                return false;
            }
            else if (i != (numDots - 1) && i != 0 && Lines[i].Count != 2)
            {
                return false;
            }
        }


        //Sort all lines
        foreach (List<GameObject> line in Lines)
        {
            line.Sort(sortLine);
        }




        if (isUp)  //goes Up
        {

            for (int i = 0; i < Lines.Count - 1; ++i)
            {
                //Check if Triangle is Down (different of the triangle5x3)
                if (Lines[i][0].transform.position.y < Lines[i + 1][0].transform.position.y)
                {
                    return false;
                }
            }

        }
        else   //goes Down
        {
            for (int i = 0; i < Lines.Count - 1; ++i)
            {
                //Check if Triangle is Up (different of the triangle5x3)
                if (Lines[i][0].transform.position.y > Lines[i + 1][0].transform.position.y)
                {
                    return false;
                }
            }
        }


        if (isLeft)  //goes left
        {

            for (int i = 0; i < Lines.Count - 1; ++i)
            {
                //Check if Triangle is Right (different of the triangle5x3)
                if (Lines[i][0].transform.position.x != Lines[i + 1][0].transform.position.x)
                {
                    return false;
                }
            }
        }
        else    //goes right
        {
            for (int i = 0; i < Lines.Count - 1; ++i)
            {
                //Check if Triangle is Left (different of the triangle5x3)
                if (Lines[i][Lines[i].Count - 1].transform.position.x != Lines[i + 1][Lines[i + 1].Count - 1].transform.position.x)
                {
                    return false;
                }
            }


        }


        //Check dots
        if (isUp)  //goes Up
        {

            for (int i = 0; i < Lines.Count - 1; ++i)
            {
                //Check dots y distance
                if ((Lines[i][0].transform.position.y - Lines[i + 1][0].transform.position.y) != distanceBetweenPointsY)
                {
                    return false;
                }
                if (Lines[i + 1].Count > 1)
                {
                    if ((Lines[i][Lines[i].Count - 2].transform.position.y - Lines[i + 1][1].transform.position.y) != distanceBetweenPointsY)
                    {
                        return false;
                    }
                }

            }

        }
        else   //goes Down
        {

            for (int i = 1; i < Lines.Count; ++i)
            {
                //Check dots y distance
                if ((Lines[i][0].transform.position.y - Lines[0][0].transform.position.y) != distanceBetweenPointsY * i)
                {
                    return false;
                }
                if (Lines[i].Count > 1)
                {
                    if ((Lines[i][0].transform.position.y - Lines[0][i].transform.position.y) != distanceBetweenPointsY * i)
                    {
                        return false;
                    }
                }


            }

        }

        if (isLeft)  //goes right
        {
            //FirstLine
            for (int j = 0; j < Lines[0].Count - 1; ++j)
            {
                //Check dots x distance
                if ((Lines[0][j + 1].transform.position.x - Lines[0][j].transform.position.x) != distanceBetweenPointsX)
                {
                    return false;
                }


                for (int i = 1; i < Lines.Count; ++i)
                {
                    //Check dots position
                    if (Lines[0][0].transform.position.x != Lines[i][0].transform.position.x)
                    {
                        return false;
                    }

                    if (i != Lines.Count - 1)
                    {
                        //Check dots position
                        if (Lines[0][Lines[0].Count - (i + 1)].transform.position.x != Lines[i][1].transform.position.x)
                        {
                            return false;
                        }
                    }
                }
            }

            //Other Lines
            for (int i = 1; i < Lines.Count - 1; ++i)
            {
                //Check dots x distance
                if ((Lines[i][1].transform.position.x - Lines[i][0].transform.position.x) != distanceBetweenPointsX * (Lines[0].Count - (i + 1)))
                {
                    return false;
                }

                //Check dots position
                if (Lines[i][0].transform.position.x != Lines[i + 1][0].transform.position.x)
                {
                    return false;
                }
            }
        }
        else    //goes left
        {
            //FirstLine
            for (int j = 0; j < Lines[0].Count - 1; ++j)
            {
                //Check dots x distance
                if ((Lines[0][j + 1].transform.position.x - Lines[0][j].transform.position.x) != distanceBetweenPointsX)
                {
                    return false;
                }


                for (int i = 1; i < Lines.Count - 1; ++i)
                {
                    //Check dots position
                    if (Lines[0][Lines[0].Count - 1].transform.position.x != Lines[i][1].transform.position.x)
                    {
                        return false;
                    }

                    //Check dots position
                    if (Lines[0][i].transform.position.x != Lines[i][0].transform.position.x)
                    {
                        return false;
                    }

                }
            }

            //Other Lines
            for (int i = 1; i < Lines.Count - 1; ++i)
            {
                //Check dots x distance
                if ((Lines[i][1].transform.position.x - Lines[i][0].transform.position.x) != distanceBetweenPointsX * (Lines[0].Count - (i + 1)))
                {
                    return false;
                }

                //Check dots position
                if (Lines[i][0].transform.position.x + distanceBetweenPointsX != Lines[i + 1][0].transform.position.x)
                {
                    return false;
                }

            }


        }



        return true;
    }

    private bool CheckSquare(ref List<GameObject> points, uint numSidePoints)
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

<<<<<<< HEAD
        Lines.Sort(sortListY);


        for (int i = 0; i < numSidePoints;++i)
=======
        for (int i = 0; i < numSidePoints; ++i)
>>>>>>> 43f510aa2ec479c2dba3cbd5d78d08db5810c8b4
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
        for (int i = 0; i < (Lines[(int)(numSidePoints - 1)].Count - 1); ++i)
        {
            if ((Lines[0][i + 1].transform.position.x - Lines[0][i].transform.position.x) != distanceBetweenPointsX)
            {
                return false;
            }
        }

        if (numSidePoints > 2)
        {
            for (int i = 1; i < (Lines.Count - 1); ++i)
            {
                if ((Lines[i][1].transform.position.x - Lines[i][0].transform.position.x) != (distanceBetweenPointsX * (numSidePoints - 1)))
                {
                    return false;
                }

            }

        }



        return true;

    }

    private bool CheckRectangle(ref List<GameObject> points, uint numHorizontalPoints, uint numVerticalPoints)
    {
        Debug.Log("Start Square Check");


        Debug.Assert(numHorizontalPoints >= 2 && numVerticalPoints >= 2, "[CheckRectangle] wrong size!");


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

        if (Lines.Count != numVerticalPoints)
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

    private bool CheckDiamond(ref List<GameObject> points, uint numSidePoints)
    {
        float distanceBetweenPoints = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();

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


        //Initialize Lines list
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

        //Sort Lines
        Lines.Sort(sortListY);

        //Check first and last lines size
        if (Lines[0].Count != 1 && Lines[Lines.Count - 1].Count != 1)
        {
            return false;
        }

        //Check other lines size
        for (int i = 1; i < Lines.Count - 1; ++i)
        {
            if (Lines[i].Count != 2)
            {
                return false;
            }
        }

        //Sort all lines
        foreach (List<GameObject> line in Lines)
        {
            line.Sort(sortLine);
        }


        //Will divide the check in two halfs of the diamond

        //Top Side

        int startPos = Mathf.FloorToInt(Lines.Count * 0.5f);


        for (int i = startPos; i > 0; --i)
        {
            //Check first element (left element) distance to first of line on top
            if ((Lines[i - 1][0].transform.position.y - Lines[i][0].transform.position.y) != distanceBetweenPoints)
            {
                return false;
            }

            //Check Second element (right element) distance to second of line on top (protect against last line check)
            if (i > 1 && (Lines[i - 1][1].transform.position.y - Lines[i][1].transform.position.y) != distanceBetweenPoints)
            {
                return false;
            }

            //Check distance between points inside same line
            if ((Lines[i][1].transform.position.x - Lines[1][0].transform.position.x) != (i * 2) * distanceBetweenPoints)
            {
                return false;
            }

        }

        //Bottom Side

        for (int i = startPos; i < (numSidePoints + (numSidePoints - 1) - 1); i++)
        {
            //Check first element (left element) distance to first of line on bottom
            if ((Lines[i][0].transform.position.y - Lines[i + 1][0].transform.position.y) != distanceBetweenPoints)
            {
                return false;
            }

            //Check Second element (right element) distance to second of line on bottom (protect against last line check)
            if (i < (numSidePoints + (numSidePoints - 1) - 2) && (Lines[i][1].transform.position.y - Lines[i + 1][1].transform.position.y) != distanceBetweenPoints)
            {
                return false;
            }

            //Check distance between points inside same line
            if ((Lines[i][1].transform.position.x - Lines[1][0].transform.position.x) != ((numSidePoints - i - 1) * 2) * distanceBetweenPoints)
            {
                return false;
            }

        }



        return true;
    }

<<<<<<< HEAD
    private bool CheckLShape(ref List<GameObject> points, bool isTop, bool isLeft)
    {

        float distanceBetweenPoints = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();

        //Check number of points
        if (points.Count != 19 ) //Hard coded because their is no plan for other size LShape
=======
    private bool CheckCShapeSide(ref List<GameObject> playerPoints)
    {
        int mCountLine0 = 0;
        int mCountLine1 = 0;
        int mCountLine2 = 0;
        int mCountLine3 = 0;
        int mCountLine4 = 0;
        float distanceBetweenPoints = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();
        List<GameObject> tempList = new List<GameObject>();

        //Check number of points
        if (playerPoints.Count != 19)
>>>>>>> 43f510aa2ec479c2dba3cbd5d78d08db5810c8b4
        {
            return false;
        }

        //Check if shape was closed
<<<<<<< HEAD
        if (points[0].transform.position != points[points.Count - 1].transform.position)
        {
            return false;
        }

        List<List<GameObject>> Lines = new List<List<GameObject>>();


        //Initialize Lines list
        for (int i = 0; i < (points.Count - 1); ++i)
        {

            for (int j = 0; j < PointsManager.mPointsManager.numberLines; ++j)
            {
                if (Lines.Count == j)
                {
                    Lines.Add(new List<GameObject>());
                    Lines[j].Add(points[i]);
                    
                    break;
                }
                else if (points[i].transform.position.y == Lines[j][0].transform.position.y)
                {
                    Lines[j].Add(points[i]);
                    break;
                }

            }

        }

        //Sort Lines
        Lines.Sort(sortListY);
        
        

        //Sort all lines
        foreach (List<GameObject> line in Lines)
        {
            line.Sort(sortLine);
        }


        //Top 
        if (isTop)
        {
            //Check lines size
            for (int i = 0; i < Lines.Count; ++i)
            {
                if (i >= 2 && i < Lines.Count)
                {
                    if (Lines[i].Count != 2)
                    {
                        return false;
                    }
                }
                else
                {
                    if (Lines[i].Count != 6)
                    {
                        return false;
                    }
                }
            }
        }
        else //Bottom
        {

            //Check lines size
            for (int i = 0; i < Lines.Count; ++i)
            {
                if (i >= 0 && i < 3)
                {
                    if (Lines[i].Count != 2)
                    {
                        return false;
                    }
                }
                else
                {
                    if (Lines[i].Count != 6)
                    {
                        return false;
                    }
                }
            }
        }

        //Left
        if (isLeft)
        {
            for (int i = 1; i < Lines.Count; ++i)
            {
                if (Lines[0][0].transform.position.x != Lines[i][0].transform.position.x &&
                   Lines[0][1].transform.position.x != Lines[i][1].transform.position.x)
                {
                    return false;
                }
            }


            //Check y position
            for (int i = 0; i < Lines.Count - 1; ++i)
            {
                for (int j = 0; j < Lines[i].Count; ++j)
                {
                    if(j == 2 && i == 1)
                    {
                        break;
                    }

                    if((Lines[i][j].transform.position.y - Lines[i + 1][j].transform.position.y) != distanceBetweenPoints)
                    {
                        return false;
                    }
                }
            }

        }
        else //Right
        {
            for (int i = 1; i < Lines.Count; ++i)
            {
                if (Lines[0][Lines[0].Count - 2].transform.position.x != Lines[i][Lines[i].Count - 2].transform.position.x &&
                   Lines[0][Lines[0].Count - 1].transform.position.x != Lines[i][Lines[i].Count - 1].transform.position.x)
                {
                    return false;
                }
            }

            //Check y position
            for (int i = 0; i < Lines.Count - 1; ++i)
            {
                for (int j = 0; j < Lines[i].Count; ++j)
                {
                    if (Lines[i].Count < Lines[i+1].Count)
                    {
                        if ((Lines[i][j].transform.position.y - Lines[i + 1][Lines[i + 1].Count - 2 + j].transform.position.y) != distanceBetweenPoints)
                        {
                            return false;
                        }                        
                    }

                    if (Lines[i].Count > Lines[i + 1].Count)
                    {
                        if(j == Lines[i +1].Count)
                        {
                            break;
                        }

                        if ((Lines[i][j].transform.position.y - Lines[i + 1][Lines[i + 1].Count - 2 + j].transform.position.y) != distanceBetweenPoints)
                        {
                            return false;
                        }                        
                    }

                    if ((Lines[i][j].transform.position.y - Lines[i + 1][j].transform.position.y) != distanceBetweenPoints)
                    {
                        return false;
                    }
                }
            }


        }


        //Check x position
        foreach(List<GameObject> line in Lines)
        {
            for(int i = 0; i < line.Count -1; ++i)
            {
                if((line[i + 1].transform.position.x - line[i].transform.position.x) != distanceBetweenPoints)
                {
                    return false;
                }
            }
        }
            return true;
    }

    private bool CheckTrapezoid(ref List<GameObject> points, int direction, int baseDotsNum, int topDotsNum, int height)
    {
        //Direction:
        // 0 : Top
        // 1 : Bottom
        // 2 : Left
        // 3 : Right

        Debug.Assert(direction <= 3 , "[Check Trapezoid] wrong direction!");

        float distanceBetweenPoints = PointsManager.mPointsManager.GetDistanceBetweenLinePoints();

        //Check number of points
        if (points.Count != (baseDotsNum + topDotsNum + ((height - 2) * 2)) + 1) 
        {
            return false;
        }

        //Check if shape was closed
        if (points[0].transform.position != points[points.Count - 1].transform.position)
        {
            return false;
        }

        List<List<GameObject>> Lines = new List<List<GameObject>>();


        //Initialize Lines list
        for (int i = 0; i < (points.Count - 1); ++i)
        {

            for (int j = 0; j < PointsManager.mPointsManager.numberLines; ++j)
            {
                if (Lines.Count == j)
                {
                    Lines.Add(new List<GameObject>());
                    Lines[j].Add(points[i]);

                    break;
                }
                else if (points[i].transform.position.y == Lines[j][0].transform.position.y)
                {
                    Lines[j].Add(points[i]);
                    break;
                }

            }

        }

        //Sort Lines
        Lines.Sort(sortListY);



        //Sort all lines
        foreach (List<GameObject> line in Lines)
        {
            line.Sort(sortLine);
        }

        if(direction == 0) //Top
        {
            //check dots in each line
            for(int i = 0; i < Lines.Count; ++i)
            {
                if(i == 0 && Lines[i].Count != baseDotsNum)
                {
                    return false;
                }
                else if (i == (Lines.Count - 1) && Lines[i].Count != topDotsNum)
                {
                    return false;
                }
                else if (Lines[i].Count != 2)
                {
                    return false;
                }
            }

            //check x distance
            for (int i = 0; i < Lines.Count; ++i)
            {
                for(int j = 0; j < Lines[i].Count -1;++j)
                {
                    if(( i == 0 || i == Lines.Count -1 ) &&
                        ((Lines[i][j + 1].transform.position.x - Lines[i][j].transform.position.x) != distanceBetweenPoints))
                    {
                        return false;
                    }

                    if ((i > 0 && i < Lines.Count - 1) &&
                        ((Lines[i][j + 1].transform.position.x - Lines[i][j].transform.position.x) != (distanceBetweenPoints * (baseDotsNum -(1 +( 2 * i))))))
                    {
                        return false;
                    }
                }
            }

            //Check y distance
            ////////////////////////////////////int m
            //Right side
            ////////////////////for(int i = ; i < baseDotsNum - 1; ++i)
            ////////////////////{
            ////////////////////    
            ////////////////////}


        }



        return true;
=======
        if (playerPoints[0].transform.position != playerPoints[playerPoints.Count - 1].transform.position)
        {
            return false;
        }
        //List<List<GameObject>> Lines = new List<List<GameObject>>();
        //Check number of points in line
        for (int i = 0; i < playerPoints.Count - 1; ++i)
        {
            if (playerPoints[i].transform.position.y == PointsManager.mPointsManager.points[0][0].transform.position.y)
            {
                tempList.Add(playerPoints[i]);
                mCountLine0++;
            }
            if (playerPoints[i].transform.position.y == PointsManager.mPointsManager.points[1][0].transform.position.y)
            {
                tempList.Add(playerPoints[i]);
                mCountLine1++;
            }
            if (playerPoints[i].transform.position.y == PointsManager.mPointsManager.points[2][0].transform.position.y)
            {
                tempList.Add(playerPoints[i]);
                mCountLine2++;
            }
            if (playerPoints[i].transform.position.y == PointsManager.mPointsManager.points[3][0].transform.position.y)
            {
                tempList.Add(playerPoints[i]);
                mCountLine3++;
            }
            if (playerPoints[i].transform.position.y == PointsManager.mPointsManager.points[4][0].transform.position.y)
            {
                tempList.Add(playerPoints[i]);
                mCountLine4++;
            }
            if (Vector3.Distance(playerPoints[i].transform.position, playerPoints[i + 1].transform.position) != distanceBetweenPoints)
                return false;
        }

        if (mCountLine0 == 2 && mCountLine1 == 4 && mCountLine2 == 4 && mCountLine3 == 4 && mCountLine4 == 4)
            return true;
        else
            return false;
>>>>>>> 43f510aa2ec479c2dba3cbd5d78d08db5810c8b4
    }


    //=========================================================================
    //========================  Support Functions  ============================
    //=========================================================================


    //Crescent order 
    public int sortLine(GameObject GO1, GameObject GO2)
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

    //Decrescent order
    public int sortList(List<GameObject> list1, List<GameObject> list2)
    {
        if (list1.Count == 0)
        {
            if (list2.Count == 0)
            {
                // If list1 is null and list2 is null, they're
                // equal. 
                return 0;
            }
            else
            {
                // If list1 is null and list2 is not null, list2
                // is greater. 
                return 1;
            }
        }
        else
        {
            // If list1 is not null...
            //
            if (list2.Count == 0)
            {
                return -1;
            }
            else
            {
                // ...and list2 is not null, compare the 
                // number of GameObjects.
                //
                if (list1.Count > list2.Count)
                {
                    return -1;
                }
                else if (list1.Count < list2.Count)
                {
                    return 1;
                }
                else
                {

                    return 0;

                }

            }
        }


    }

    //Decrescent order
    public int sortListUp(List<GameObject> list1, List<GameObject> list2)
    {
        if (list1.Count == 0)
        {
            if (list2.Count == 0)
            {
                // If list1 is null and list2 is null, they're
                // equal. 
                return 0;
            }
            else
            {
                // If list1 is null and list2 is not null, list2
                // is greater. 
                return 1;
            }
        }
        else
        {
            // If list1 is not null...
            //
            if (list2.Count == 0)
            {
                return -1;
            }
            else
            {
                // ...and list2 is not null, compare the 
                // number of GameObjects.
                //
                if (list1.Count > list2.Count)
                {
                    return -1;
                }
                else if (list1.Count < list2.Count)
                {
                    return 1;
                }
                else
                {
                    if (list1[0].transform.position.y > list2[0].transform.position.y)
                    {
                        return -1;
                    }
                    else if (list1[0].transform.position.y < list2[0].transform.position.y)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

                }

            }
        }


    }

    //Decrescent order
    public int sortListDown(List<GameObject> list1, List<GameObject> list2)
    {
        if (list1.Count == 0)
        {
            if (list2.Count == 0)
            {
                // If list1 is null and list2 is null, they're
                // equal. 
                return 0;
            }
            else
            {
                // If list1 is null and list2 is not null, list2
                // is greater. 
                return 1;
            }
        }
        else
        {
            // If list1 is not null...
            //
            if (list2.Count == 0)
            {
                return -1;
            }
            else
            {
                // ...and list2 is not null, compare the 
                // number of GameObjects.
                //
                if (list1.Count > list2.Count)
                {
                    return -1;
                }
                else if (list1.Count < list2.Count)
                {
                    return 1;
                }
                else
                {
                    if (list1[0].transform.position.y > list2[0].transform.position.y)
                    {
                        return 1;
                    }
                    else if (list1[0].transform.position.y < list2[0].transform.position.y)
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

    //Decrescent order
    public int sortListY(List<GameObject> list1, List<GameObject> list2)
    {
        if (list1.Count == 0)
        {
            if (list2.Count == 0)
            {
                // If list1 is null and list2 is null, they're
                // equal. 
                return 0;
            }
            else
            {
                // If list1 is null and list2 is not null, list2
                // is greater. 
                return 1;
            }
        }
        else
        {
            // If list1 is not null...
            //
            if (list2.Count == 0)
            {
                return -1;
            }
            else
            {
                // ...and list2 is not null, compare the 
                // number of GameObjects.
                //
                if (list1[0].transform.position.y > list2[0].transform.position.y)
                {
                    return -1;
                }
                else if (list1[0].transform.position.y < list2[0].transform.position.y)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
        }


    }

}
