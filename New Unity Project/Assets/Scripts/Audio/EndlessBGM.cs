using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessBGM : MonoBehaviour
{

    private AudioSource endlessBGM;
    // Use this for initialization
    void Start()
    {
        endlessBGM = GetComponent<AudioSource>();
        endlessBGM.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
