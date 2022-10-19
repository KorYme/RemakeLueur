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

    public void ResetCD(float newTimer = -1)
    {
        maxTimer = newTimer > 0 ? newTimer : maxTimer;
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
}
