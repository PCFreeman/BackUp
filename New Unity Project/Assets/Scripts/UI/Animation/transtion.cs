using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transtion : MonoBehaviour {
    public Animator myAnimator;

    public void Diactive()
    {
        myAnimator.SetBool("IsOne", false);
    }
}
