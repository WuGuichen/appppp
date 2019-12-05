using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public BattleManager bm;
    public Actor_Controller ac;



    void Awake()
    {
        ac = GetComponent<Actor_Controller>();
        GameObject sensor = transform.Find("Sensor").gameObject;
        bm = sensor.GetComponent<BattleManager>();
        if(bm == null)
        {
            bm = sensor.AddComponent<BattleManager>();
        }
        bm.am = this;
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
