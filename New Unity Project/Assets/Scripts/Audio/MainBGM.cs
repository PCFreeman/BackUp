using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGM : MonoBehaviour
{

    private AudioController mController;
    public AudioSource mainBGM;
    // Use this for initialization
    void Start()
    {
        mController = GameObject.Find("Audio").GetComponent<AudioController>();
        mainBGM = GetComponent<AudioSource>();
        mainBGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //if (AudioController.sInstance.GetMute())
        //{
        //    mainBGM.volume = 0.0f;
        //}

        //if (!AudioController.sInstance.GetMute())
        //{
        //    mainBGM.volume = 1.0f;
        //}

    }
}