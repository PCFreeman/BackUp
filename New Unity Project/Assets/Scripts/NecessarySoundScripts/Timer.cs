using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    FMODUnity.StudioEventEmitter emitter;
    float deciderAmount;

	void ClockAudio (float timeLeft)
    {
        var target = GameObject.Find("Timer");
        emitter = target.GetComponent<FMODUnity.StudioEventEmitter>();

        if (timeLeft > 20)
        {
            deciderAmount = 0f;
        }
        else if (timeLeft <= 20 && timeLeft > 10)
        {
            deciderAmount = 7.51f;
        }
        else if (timeLeft <= 10 && timeLeft > 0)
        {
            deciderAmount = 9.01f;
        }

        if (timeLeft > -1)
        {
            emitter.SetParameterValueByIndex(0, deciderAmount);
        }

        else if (timeLeft > -1 && timeLeft < 0)
        {
            deciderAmount = 9.91f;
        }
    }
}
