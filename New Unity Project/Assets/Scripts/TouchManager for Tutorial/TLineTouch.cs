using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLineTouch : MonoBehaviour {

    private static List<GameObject> GOs = new List<GameObject>();

    private bool check = false;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collides line =  " + other.name);


            other.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.0f, 0.0f, 1.0f);

            GOs.Add(other.gameObject);

            TTouchManager.mTTouchManager.mTColliders.mCurrentPoint = other.gameObject;
            TTouchManager.mTTouchManager.mTColliders.pointCount += 1; 

            check = true;



        //TTouchManager.mTTouchManager.mDrawTouch.SetSelectedPoint(ref other.gameObject);
    }
    

   

    public static List<GameObject> GetCollidedObjects()
    {
        Debug.Log("GOs size = " + GOs.Count.ToString());
      
        return GOs;
    }
    


}
