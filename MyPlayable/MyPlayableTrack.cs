using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.3948817f, 1f, 0f)]
[TrackClipType(typeof(MyPlayableClip))]
[TrackBindingType(typeof(ActorManager))]
public class MyPlayableTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<MyPlayableMixerBehaviour>.Create (graph, inputCount);
    }
}
