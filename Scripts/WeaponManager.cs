using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{
    //public ActorManager am;
    public Collider weaponColL;
    public Collider weaponColR;

    public GameObject whL;
    public GameObject whR;

    public WeaponController wcL;
    public WeaponController wcR;

    private void Start()
    {
        try
        {
            whL = transform.DeepFind("weaponHandleL").gameObject;
            wcL = BindWeaponController(whL);
            weaponColL = whL.GetComponentInChildren<Collider>();

        }
        catch (System.Exception ex)
        {
            //
            // If there is no "weaponHandleL" or related objects
            //
        }

        try
        {
            whR = transform.DeepFind("weaponHandleR").gameObject;
            wcR = BindWeaponController(whR);
            weaponColR = whR.GetComponentInChildren<Collider>();

        }
        catch (System.Exception ex)
        {
            //
            // If there is no "weaponHandleR" or related objects
            //
        }
    }

    public void UpdateWeaponCollider(string side, Collider col)
    {
        if(side == "L")
        {
            weaponColL = col;
        }
        else if(side == "R")
        {
            weaponColR = col;
        }
        
    }

    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController tempWc;
        tempWc = targetObj.GetComponent<WeaponController>();
        if (tempWc == null)
            tempWc = targetObj.AddComponent<WeaponController>();
        tempWc.wm = this;

        return tempWc;
    }

    public void WeaponEnable()
    {
        if (am.ac.CheckStateTag("attackL"))
            weaponColL.enabled = true;
        else
            weaponColR.enabled = true;
    }
    public void WeaponDisable()
    {
        weaponColL.enabled = false;
        weaponColR.enabled = false;
    }

    public void CounterBackEnable()
    {
        am.SetIsCounterBack(true);
    }

    public void CounterBackDisable()
    {
        am.SetIsCounterBack(false);
    }
}
