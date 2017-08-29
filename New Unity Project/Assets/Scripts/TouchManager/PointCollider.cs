using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollider : MonoBehaviour {


    private static List<GameObject> GOs = new List<GameObject>();

    private bool check = false;
    void OnTriggerEnter(Collider other)
    {
        
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.0f, 0.0f, 1.0f);

        GOs.Add(other.gameObject);

        TouchManager.mTouchManager.mColliders.mCurrentPoint = this.gameObject;
        TouchManager.mTouchManager.mColliders.pointCount += 1;

        check = true;



        //TouchManager.mTouchManager.mDrawTouch.SetSelectedPoint(ref other.gameObject);
    }




    public static List<GameObject> GetCollidedObjects()
    {
        Debug.Log("GOs size = " + GOs.Count.ToString());

        return GOs;
    }
}
