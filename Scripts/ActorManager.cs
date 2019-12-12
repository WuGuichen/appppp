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
    public DirectorManager dm;
    public InteractionManager im;


    void Awake()
    {
        ac = GetComponent<Actor_Controller>();
        GameObject model = ac.model;
        GameObject sensor = transform.Find("Sensor").gameObject;
        
        
        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectorManager>(gameObject);
        im = Bind<InteractionManager>(sensor);
        //sm.Test();

        ac.OnAction += DoAction;
    }

    public void DoAction()
    {
        //print("action1 !!!!!");
        if(im.overlapEcastms.Count != 0)
        {
            //print(im.overlapEcastms[0].eventName);
            //I should play corresponding(eventName) timeline here
            if (im.overlapEcastms[0].eventName == "frontStab")
                dm.PlayFrontStab("frontStab", this, im.overlapEcastms[0].am);
            else if (im.overlapEcastms[0].eventName == "openBox")
                dm.PlayFrontStab("openBox", this, im.overlapEcastms[0].am);
        }
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

    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnable = value;
    }

    public void TryDoDamage(WeaponController targetWc, bool attackValid, bool counterValid)
    {
        //if(sm.HP > 0)
        //    sm.AddHP(-5);
        if (sm.isCounterBackSuccess)
        {
            if (counterValid)
                targetWc.wm.am.Stunned();
        }
        else if(sm.isCounterBackFailure)
        {
            if (attackValid)
                HitOrDie(false);
        }
        else if (sm.isImmortal)
        {
            // 无敌
        }
        else if (sm.isDefense)
        {
            Blocked();
        }
        else
        {
            if (attackValid)
                HitOrDie(true);
        }

    }

    public void HitOrDie(bool doHitAnimation)
    {
        if (sm.HP <= 0)
        {

        }
        else
        {
            sm.AddHP(-5);
            if (sm.HP > 0)
            {
                if (doHitAnimation)
                    Hit();
                // do some VFX. like splatter blood......
            }
            else
            {
                Die();
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

    public void Stunned()
    {
        ac.Issuetrigger("stunned");
    }

    public void TestEcho()
    {
        print("Echo ..........");
    }

    public void LockUnlockActorController(bool value)
    {
        ac.SetBool("lock", value);
    }


    
}
