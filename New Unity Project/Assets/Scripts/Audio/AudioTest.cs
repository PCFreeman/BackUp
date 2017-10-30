using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioController.sInstance.SuccessMoveSFX();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            AudioController.sInstance.GameOverSFX();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SPACE PRESSED!!!");
            AudioController.sInstance.MuteMainBGM();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioController.sInstance.ErrorSFX();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioController.sInstance.LevelUpSFX();
        }

    }
}
