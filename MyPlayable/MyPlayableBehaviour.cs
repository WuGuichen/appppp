using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MyPlayableBehaviour : PlayableBehaviour
{
    public Camera MyCamera;
    public float MyFloat;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }
}
