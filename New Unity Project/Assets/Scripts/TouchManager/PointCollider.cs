using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollider : MonoBehaviour {

    List<GameObject> GOs;
    

    private bool check = false;
    void OnTriggerEnter(Collider other)
    {
        GOs = new List<GameObject>();
        GOs = TouchManager.mTouchManager.GetCollidedObjects();

        if (GOs.Count != 0)
        { 
            if(this.gameObject.GetComponent<SpriteRenderer>().color == new Color(0.1f, 0.0f, 0.0f, 1.0f)
                && this.gameObject != GOs[0]
                && GOs[GOs.Count-1] != GOs[0])
            {
                return;
            }
        }

        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.0f, 0.0f, 1.0f);

        TouchManager.mTouchManager.AddGameObject(this.gameObject);

        TouchManager.mTouchManager.mColliders.mCurrentPoint = this.gameObject;
        TouchManager.mTouchManager.mColliders.pointCount += 1;

        check = true;



        //TouchManager.mTouchManager.mDrawTouch.SetSelectedPoint(ref other.gameObject);
    }



}
