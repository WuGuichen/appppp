using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : MonoBehaviour
{
    
    public ActorManager am;
    private CapsuleCollider defCol;

    private void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = Vector3.up*1.0f;
        defCol.height = 2.0f;
        defCol.radius = 0.25f;
        defCol.isTrigger = true;
        
    }
    private void OnTriggerEnter(Collider col)
    {
        print(col.tag);
        if(col.tag == "Weapon")
        {
            print(col.name);
            am.DoDamage();
        }
    }

    
}
