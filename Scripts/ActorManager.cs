using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public Actor_Controller ac;

    [Header("======== Auto Generate If Null =======")]
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;


    void Awake()
    {
        ac = GetComponent<Actor_Controller>();
        GameObject model = ac.model;
        GameObject sensor = transform.Find("Sensor").gameObject;

        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
        //sm.Test();
    }

    private T Bind<T>(GameObject go) where T : IActorManagerInterface
    {
        T tempInstance;
        tempInstance = go.GetComponent<T>();
        if(tempInstance == null)
        {
            tempInstance = go.AddComponent<T>();

        }
        tempInstance.am = this;

        return tempInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryDoDamage()
    {
        //if(sm.HP > 0)
        //    sm.AddHP(-5);
        if (sm.isImmortal)
        {
            // 无敌
        }
        else if (sm.isDefense)
        {
            Blocked();
        }
        else
        {
            if (sm.HP <= 0)
            {

            }
            else
            {
                sm.AddHP(-5);
                if (sm.HP > 0)
                {
                    Hit();
                }
                else
                {
                    Die();
                }
            }
        }

    }

    public void Blocked()
    {
        ac.Issuetrigger("blocked");
    }

    public void Hit()
    {
        ac.Issuetrigger("hit");
    }

    public void Die()
    {
        ac.Issuetrigger("die");
        ac.pi.inputEnable = false;
        if (ac.camcon.lockState == true)
        {
            ac.camcon.LockUnlock();
            
        }
        ac.camcon.enabled = false;
    }
}
