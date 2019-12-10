using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MyPlayableBehaviour : PlayableBehaviour
{
    public GameObject MyCamera;
    public float MyFloat;

    PlayableDirector pd;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {
        pd = (PlayableDirector)playable.GetGraph().GetResolver();
        foreach (var track in pd.playableAsset.outputs)
        {
            if(track.streamName == "Attacker Script" || track.streamName == "Victim Script")
            {
                ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
                am.LockUnlockActorController(true);
            }
        }
    }

    public override void OnGraphStop(Playable playable)
    {
        foreach (var track in pd.playableAsset.outputs)
        {
            if (track.streamName == "Attacker Script" || track.streamName == "Victim Script")
            {
                ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
                //am.LockUnlockActorController(false);
            }
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
    }

}
