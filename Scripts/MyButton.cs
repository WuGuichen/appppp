using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton
{
    public bool isPressing = false;
    public bool onPressed = false;
    public bool OnReleased = false;
    public bool IsExtending = false;
    public bool IsDelaying = false;

    [Header("===== Setting =====")]
    public float extendingDuration = 0.15f;
    public float delayingDuration = 0.5f;

    private bool curState = false;
    private bool lastState = false;
    

    private MyTimer extTimer = new MyTimer();
    private MyTimer delayTimer = new MyTimer();


    public void Tick(bool input)
    {
        //StartTimer(extTimer, 1.0f);

        extTimer.Tick();
        delayTimer.Tick();

        curState = input;

        isPressing = curState;

        onPressed = false;
        OnReleased = false;
        IsExtending = false;
        IsDelaying = false;

        if (curState != lastState)
        {
            if(curState)
            {
                onPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            else
            {
                OnReleased = true;
                StartTimer(extTimer, extendingDuration);
            }
        }

        lastState = curState;
        //Debug.Log(extTimer.state);
        if(extTimer.state == MyTimer.STATE.RUN)
        { 
            IsExtending = true;
        }

        if(delayTimer.state == MyTimer.STATE.RUN)
        {
            IsDelaying = true;
        }
        

    }

    private void StartTimer(MyTimer timer, float duration)
    {
        timer.duration = duration;
        timer.Go();
    }


}
    