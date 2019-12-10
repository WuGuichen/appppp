﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TexterDirector : MonoBehaviour
{
    public PlayableDirector pd;

    public Animator attacker;

    public Animator victim;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (var track in pd.playableAsset.outputs)
            {
                if(track.streamName == "Attacker Animation")
                {
                    pd.SetGenericBinding(track.sourceObject, attacker);
                }
                else if(track.streamName == "Victim Animation")
                {
                    pd.SetGenericBinding(track.sourceObject, victim);
                }
                
            }

            //pd.time = 0;
            //pd.Stop();
            //pd.Evaluate();
            pd.Play();
        }
    }
}
