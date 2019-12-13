using System.Collections;
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
        if (targetWc == null)
            return;

        GameObject attacker = targetWc.wm.gameObject;
        GameObject receiver = am.ac.model;                      //受击者

        

        //print("atta" + attackingDir);
        if (col.tag == "Weapon")
        {
            am.TryDoDamage(targetWc, CheckAngleTarget(receiver,attacker,70), CheckAnglePlayer(receiver,attacker,30));
        }

        
    }

    //victim 用
    public static bool CheckAnglePlayer(GameObject player, GameObject target, float playerAngleLimit)
    {
        Vector3 counterDir = target.transform.position - player.transform.position;

        float counterAngle1 = Vector3.Angle(player.transform.forward, counterDir);
        float counterAngle2 = Vector3.Angle(target.transform.forward, player.transform.forward);  // should be close to 180 degree

        bool counterValid = (counterAngle1 < playerAngleLimit && Mathf.Abs(counterAngle2 - 180) < playerAngleLimit);
        return counterValid;
    }

    //attacker 用
    public static bool CheckAngleTarget(GameObject player, GameObject target, float targetAngleLimit)
    {
        Vector3 attackingDir = player.transform.position - target.transform.position;

        float attackingAngle1 = Vector3.Angle(target.transform.forward, attackingDir);

        bool attackValid = (attackingAngle1 < targetAngleLimit);
        return attackValid;
    }
    
}
