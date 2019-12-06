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
        
        bm = sensor.GetComponent<BattleManager>();
        if(bm == null)
            bm = sensor.AddComponent<BattleManager>();
        bm.am = this;

        wm = model.GetComponent<WeaponManager>();
        if (wm == null)
            wm = model.AddComponent<WeaponManager>();
        wm.am = this;

        sm = model.GetComponent<StateManager>();
        if (sm == null)
            sm = model.AddComponent<StateManager>();
        sm.am = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoDamage()
    {
        ac.Issuetrigger("die");
        //sm.Test();
    }
}
