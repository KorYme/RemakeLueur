using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldowns
{
    public float maxTimer;
    public float currentTimer;
    public bool finished;

    public Cooldowns(float maxTime)
    {
        this.maxTimer = maxTime;
        currentTimer = 0;
        finished = true;
    }

    public void ResetCD()
    {
        currentTimer = maxTimer;
        finished = false;
    }

    public void DecreaseCD()
    {
        if (finished) return;
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            finished = true;
        }
    }

    public void SetNewTimer(float newTimer)
    {
        maxTimer = newTimer;
        if (currentTimer > maxTimer)
        {
            currentTimer = maxTimer;
        }
    }
}
