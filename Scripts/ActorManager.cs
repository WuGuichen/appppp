using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public BattleManager bm;
    public Actor_Controller ac;
    public WeaponManager wm;


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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoDamage()
    {
        ac.Issuetrigger("hit");
    }
}
