using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private DataBase weaponDB;
    private WeaponFactory weaponFact;

    public WeaponManager testWm;
    
    void Awake()
    {
        CheckGameObject();
        CheckSingle();
        

        
    }

    private void Start()
    {
        InitWeaponDB();
        InitWeaponFactory();

        testWm.UpdateWeaponCollider("R", weaponFact.CreateWeapon("Sword", "R", testWm));
    }

    void Update()
    {
        
    }
    private void InitWeaponDB()
    {
        weaponDB = new DataBase();
    }

    private void InitWeaponFactory()
    {
        weaponFact = new WeaponFactory(weaponDB);
    }


    private void CheckSingle()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this);
    }

    private void CheckGameObject()
    {
        if (tag == "GM")
            return;
        else
            Destroy(this);
    }
}
