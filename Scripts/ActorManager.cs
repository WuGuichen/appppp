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
        sm.Test();
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

    public void DoDamage()
    {
        ac.Issuetrigger("die");
        //sm.Test();
    }
}
