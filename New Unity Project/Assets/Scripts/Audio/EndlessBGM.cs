using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessBGM : MonoBehaviour
{
    private AudioController mController;
    private AudioSource endlessBGM;
    // Use this for initialization
    void Start()
    {
        mController = GameObject.Find("Audio").GetComponent<AudioController>();
        endlessBGM = GetComponent<AudioSource>();
        endlessBGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //if (AudioController.sInstance.GetMute())
        //{
        //    endlessBGM.volume = 0.0f;
        //}

        //if (!AudioController.sInstance.GetMute())
        //{
        //    endlessBGM.volume = 1.0f;
        //}
    }
}