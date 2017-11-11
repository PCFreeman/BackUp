using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour {

    private int corretCount = 0;
    private int maxMultip = 5;
    private int currentMultip = 0;
    public int countToLevelUp = 10;
    public static ComboSystem instance;

    private void Awake()
    {
        //Check if instance already exist
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this) //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a TouchManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);
    }


        public void IncreaseCount()
    {
        ++corretCount;
        ComboSystemUp();
    }

    public void ResetCount()
    {
        corretCount = 0;
    }

    public int GetMultiplier()
    {
        return currentMultip;
    }

    private void ComboSystemUp()
    {
        if (corretCount >= countToLevelUp)
        {
            if (currentMultip == 0)
                currentMultip = 2;
            else if (currentMultip == maxMultip)
                return;
            else
                ++currentMultip;
        }
    }

}
