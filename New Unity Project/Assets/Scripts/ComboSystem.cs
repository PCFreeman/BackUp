using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour {

    private int corretCount = 0;
    private int maxMultip = 5;
    private int currentMultip = 0;
    public int countToLevelUp = 10;

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
