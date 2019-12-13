using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Make it a singleton....later
    /// </summary>


    
    void Start()
    {
        GameObject prefab = (GameObject) Resources.Load("Falchion");
        Instantiate(prefab, Vector3.zero, Quaternion.identity);

        //Resources.UnloadUnusedAssets(); // not necessary
    }

    
    void Update()
    {
        
    }
}
