﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{
    //public ActorManager am;
    public Collider weaponColL;
    public Collider weaponColR;

    public GameObject whL;
    public GameObject whR;

    private void Start()
    {
        whL = transform.DeepFind("weaponHandleL").gameObject;
        whR = transform.DeepFind("weaponHandleR").gameObject;

        weaponColL = whL.GetComponentInChildren<Collider>();
        weaponColR = whR.GetComponentInChildren<Collider>();
    }

    public void WeaponEnable()
    {
        if (am.ac.CheckStateTag("attackL"))
            weaponColL.enabled = true;
        else
            weaponColR.enabled = true;
    }
    public void WeaponDisable()
    {
        weaponColL.enabled = false;
        weaponColR.enabled = false;
    }
}
