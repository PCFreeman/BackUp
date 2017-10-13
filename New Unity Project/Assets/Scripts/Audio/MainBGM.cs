using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGM : MonoBehaviour
{

    public AudioSource mainBGM;
    // Use this for initialization
    void Start()
    {
        mainBGM = GetComponent<AudioSource>();
        mainBGM.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
