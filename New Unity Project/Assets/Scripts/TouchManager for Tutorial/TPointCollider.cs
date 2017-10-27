using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPointCollider : MonoBehaviour {

    List<GameObject> GOs;
    

    private bool check = false;
    void OnTriggerEnter(Collider other)
    {
        GOs = new List<GameObject>();
        GOs = TTouchManager.mTTouchManager.GetCollidedObjects();

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

        TTouchManager.mTTouchManager.AddGameObject(this.gameObject);

        
        check = true;



        //TTouchManager.mTTouchManager.mDrawTouch.SetSelectedPoint(ref other.gameObject);
    }



}
