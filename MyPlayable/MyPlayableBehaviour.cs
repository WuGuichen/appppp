using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MyPlayableBehaviour : PlayableBehaviour
{
    public GameObject MyCamera;
    public float MyFloat;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {
        PlayableDirector pd = (PlayableDirector)playable.GetGraph().GetResolver();
        //Debug.Log(pd);
        foreach (var track in pd.playableAsset.outputs)
        {
            //Debug.Log(track.streamName);
            if(track.streamName == "My Playable Track")
            {
                Debug.Log(pd.GetGenericBinding(track.sourceObject));
            }
        }
    }

    public override void OnGraphStop(Playable playable)
    {
        base.OnGraphStop(playable);
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
    }

}
