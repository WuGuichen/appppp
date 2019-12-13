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
        GameObject sensor = null;
        try
        {
            sensor = transform.Find("Sensor").gameObject;

        }
        catch (System.Exception ex)
        {
            //
            // If there is no "sensor" object
            //
        }
        
        
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
            if (im.overlapEcastms[0].active == true && !dm.IsPlaying())
            {
                //I should play corresponding(eventName) timeline here
                if (im.overlapEcastms[0].eventName == "frontStab")
                    dm.PlayFrontStab("frontStab", this, im.overlapEcastms[0].am);
                else if (im.overlapEcastms[0].eventName == "openBox")
                {
                    if (BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject, 180f))
                    {
                        im.overlapEcastms[0].active = false;
                        transform.position = im.overlapEcastms[0].am.transform.position + im.overlapEcastms[0].transform.TransformVector(im.overlapEcastms[0].offset);
                        ac.model.transform.LookAt(im.overlapEcastms[0].transform, Vector3.up);
                        dm.PlayFrontStab("openBox", this, im.overlapEcastms[0].am);
                    }
                }
                else if (im.overlapEcastms[0].eventName == "leverUp")
                {
                    if (BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject, 180f))
                    {
                        im.overlapEcastms[0].active = false;
                        transform.position = im.overlapEcastms[0].am.transform.position + im.overlapEcastms[0].transform.TransformVector(im.overlapEcastms[0].offset);
                        ac.model.transform.LookAt(im.overlapEcastms[0].transform, Vector3.up);
                        dm.PlayFrontStab("leverUp", this, im.overlapEcastms[0].am);
                    }
                }
            }
        }
    }


    private T Bind<T>(GameObject go) where T : IActorManagerInterface
    {
        T tempInstance;
        if (go == null)
            return null;
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
                HitOrDie(targetWc,false);
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
                HitOrDie(targetWc,true);
        }

    }

    public void HitOrDie(WeaponController targetWc ,bool doHitAnimation)
    {
        if (sm.HP <= 0)
        {

        }
        else
        {
            sm.AddHP(-1 * targetWc.GetATK());
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
