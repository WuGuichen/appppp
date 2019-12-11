using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManagerInterface
{
    private CapsuleCollider interCol;

    void Start()
    {
        interCol = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        EventCasterManager[] ecastms = col.GetComponents<EventCasterManager>();
        foreach (var ecastm in ecastms)
        {
            //print("EventName  "+ecastm.eventName);
            //print("Active  " + ecastm.active);
        }
    }
}
