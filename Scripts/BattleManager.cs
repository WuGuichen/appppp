﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface
{
    
    //public ActorManager am;
    private CapsuleCollider defCol;

    private void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = Vector3.up*1.0f;
        defCol.height = 2.0f;
        defCol.radius = 0.5f;
        defCol.isTrigger = true;
        
    }
    private void OnTriggerEnter(Collider col)
    {
        WeaponController targetWc = col.GetComponentInParent<WeaponController>();

        GameObject attacker = targetWc.wm.am.gameObject;
        GameObject receiver = am.gameObject;                      //受击者

        Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
        Vector3 counterDir = attacker.transform.position - receiver.transform.position;
       
        float attackingAngle1 = Vector3.Angle(attacker.transform.forward, attackingDir);
        float counterAngle1 = Vector3.Angle(receiver.transform.forward, counterDir);
        float counterAngle2 = Vector3.Angle(attacker.transform.forward, receiver.transform.forward);  // should be close to 180 degree

        bool attackValid = (attackingAngle1 < 45);
        bool counterValid = (counterAngle1 < 30 && (counterAngle2 - 180) < 30);

        print("atta" + attackingDir);
        if (col.tag == "Weapon")
        {
            am.TryDoDamage(targetWc, attackValid, counterValid);
        }
    }

    
}
