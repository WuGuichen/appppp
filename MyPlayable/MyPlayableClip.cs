using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MyPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public MyPlayableBehaviour template = new MyPlayableBehaviour ();
    public ExposedReference<Camera> MyCamera;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MyPlayableBehaviour>.Create (graph, template);
        MyPlayableBehaviour clone = playable.GetBehaviour ();
        clone.MyCamera = MyCamera.Resolve (graph.GetResolver ());
        return playable;
    }
}
