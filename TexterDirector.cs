using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TexterDirector : MonoBehaviour
{
    public PlayableDirector pd;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            pd.time = 0;
            pd.Stop();
            pd.Evaluate();
            pd.Play();
        }
    }
}
