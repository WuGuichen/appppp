﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManagerInterface
{
    //public ActorManager am;

    public float HPMax = 15.0f;
    public float HP = 15.0f;

    [Header("1st order state flags")]
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;
    public bool isCounterBack = false;       // rlate to state
    public bool isCounterBackEnable = false;     // relate to animation events

    [Header("2nd order state flag")]
    public bool isAllowDefense;
    public bool isImmortal;
    public bool isCounterBackSuccess;
    public bool isCounterBackFailure;


    private void Start()
    {
        HP = HPMax;
    }

    private void Update()
    {
        isGround = am.ac.CheckState("ground");
        isJump = am.ac.CheckState("jump");
        isFall = am.ac.CheckState("fall");
        isRoll = am.ac.CheckState("roll");
        isJab = am.ac.CheckState("jab");
        isAttack = am.ac.CheckStateTag("attackR") || am.ac.CheckStateTag("attackL");   // 特殊状态机
        isHit = am.ac.CheckState("hit");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("blocked");
        //isDefense = am.ac.CheckState("defense1h", "defense");     //查defense层
        isCounterBack = am.ac.CheckState("counterBack");

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ac.CheckState("defense1h", "defense");
        isImmortal = isRoll || isJab;
        isCounterBackSuccess = isCounterBackEnable;
        isCounterBackFailure = isCounterBack && !isCounterBackEnable;

    }

    public void Test()
    {
        //print("HP IS" + HP);
    }

    public void AddHP(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMax);
        
    }

}
