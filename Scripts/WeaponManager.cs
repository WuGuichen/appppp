using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public ActorManager am;
    public Collider weaponCol;

    public GameObject whL;
    public GameObject whR;

    private void Start()
    {
        weaponCol = whR.GetComponentInChildren<Collider>();
        print(transform.DeepFind("weaponHandleL"));
    }

    public void WeaponEnable()
    {
        weaponCol.enabled = true;
    }
    public void WeaponDisable()
    {
        weaponCol.enabled = false;
    }
}
