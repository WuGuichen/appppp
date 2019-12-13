using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Make it a singleton....later
    /// </summary>

    private static GameManager instance;


    
    void Awake()
    {
        CheckGameObject();
        CheckSingle();
    }

    
    void Update()
    {
        
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
