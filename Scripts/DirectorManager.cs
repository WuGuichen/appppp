using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : IActorManagerInterface
{
    public PlayableDirector pd;

    [Header("====== Timeline assets =====")]
    public TimelineAsset frontStab;

    [Header("===== Assets Settings =====")]
    public ActorManager attacker;
    public ActorManager victim;

    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;
        //pd.playableAsset = Instantiate(frontStab);

        //foreach (var track in pd.playableAsset.outputs)
        //{
        //    if (track.streamName == "Attacker Script")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, attacker);
        //    }
        //    else if (track.streamName == "Victim Script")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, victim);
        //    }
        //    else if (track.streamName == "Attacker Animation")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, attacker.ac.anim);
        //    }
        //    else if (track.streamName == "Victim Animation")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, victim.ac.anim);
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && gameObject.layer == LayerMask.NameToLayer("Player"))
            pd.Play();
    }

    public void PlayFrontStab(string timelineName, ActorManager attacker, ActorManager victim)
    {
        if(timelineName == "frontStab")
        {
            pd.playableAsset = Instantiate(frontStab);

            //取到第一层级Timeline
            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

            //取track
            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Attacker Script")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        //Debug.Log(clip.displayName);
                        MyPlayableClip myclip = (MyPlayableClip)clip.asset;
                        MyPlayableBehaviour mybehav = myclip.template;
                        //Debug.Log(mybehav.MyFloat);
                        mybehav.MyFloat = 7777;
                        //mybehav.MyCamera = GameObject.Find("A");  错的，..不能加场景物件
                        pd.SetReferenceValue(myclip.MyCamera.exposedName, GameObject.Find("A"));
                    }
                }
                else if (track.name == "Victim Script")
                {
                    pd.SetGenericBinding(track, victim);
                    foreach (var clip in track.GetClips())
                    {
                        //Debug.Log(clip.displayName);
                        MyPlayableClip myclip = (MyPlayableClip)clip.asset;
                        MyPlayableBehaviour mybehav = myclip.template;
                        //Debug.Log(mybehav.MyFloat);
                        mybehav.MyFloat = 56666;
                    }
                }
                else if (track.name == "Attacker Animation")
                    pd.SetGenericBinding(track, attacker.ac.anim);
                else if (track.name == "Victim Animation")
                    pd.SetGenericBinding(track, victim.ac.anim);

            }

            //foreach (var trackBinding in pd.playableAsset.outputs)
            //{
            //    if (trackBinding.streamName == "Attacker Script")
            //    {
            //        pd.SetGenericBinding(trackBinding.sourceObject, attacker);
            //    }
            //    else if (trackBinding.streamName == "Victim Script")
            //    {
            //        pd.SetGenericBinding(trackBinding.sourceObject, victim);
            //    }
            //    else if (trackBinding.streamName == "Attacker Animation")
            //    {
            //        pd.SetGenericBinding(trackBinding.sourceObject, attacker.ac.anim);
            //    }
            //    else if (trackBinding.streamName == "Victim Animation")
            //    {
            //        pd.SetGenericBinding(trackBinding.sourceObject, victim.ac.anim);
            //    }
            //}
            pd.Play();
        }
    }
}
