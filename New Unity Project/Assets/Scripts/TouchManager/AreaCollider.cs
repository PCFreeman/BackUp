using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        TouchManager.mTouchManager.SetIsDrawing(true);
    }

    void OnTriggerExit(Collider other)
    {
        TouchManager.mTouchManager.SetIsDrawing(false);
    }
}
