using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer
{

    public enum STATE
    {
        IDLE,
        RUN,
        FINISHED,
    }
    public STATE state;

    public float duration = 1.0f;

    private float elapsedTime = 0f;

    public void Tick()
    {
        // go to Button
        if (state == STATE.IDLE)
        {

        }
        else if(state == STATE.RUN)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime>=duration)
            {
                state = STATE.FINISHED;
            }
        }
        else if(state == STATE.FINISHED)
        {

        }
        else
        {
            Debug.Log("false");
        }
    }

    public void Go()
    {
        elapsedTime = 0f;
        state = STATE.RUN;
    }

}
