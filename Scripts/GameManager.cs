using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private DataBase weaponDB;


    
    void Awake()
    {
        CheckGameObject();
        CheckSingle();
        InitWeaponDB();

        
    }

    private void Start()
    {

    }

    void Update()
    {
        
    }

    private void InitWeaponDB()
    {
        weaponDB = new DataBase();
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
