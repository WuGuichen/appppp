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
    public TimelineAsset openBox;
    public TimelineAsset leverUp;

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
        //if (pd.playableAsset != null)
        //   return;                             //消除连刺
        if (pd.state == PlayState.Playing)
            return;
        
        if(timelineName == "frontStab")
        {
            //pd.Evaluate();
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
                        
                        MyPlayableClip myclip = (MyPlayableClip)clip.asset;
                        MyPlayableBehaviour mybehav = myclip.template;
                        
                        mybehav.MyFloat = 7777;
                        myclip.am.exposedName = System.Guid.NewGuid().ToString();
                        
                        pd.SetReferenceValue(myclip.am.exposedName, attacker);
                        
                    }
                }
                else if (track.name == "Victim Script")
                {
                    pd.SetGenericBinding(track, victim);
                    foreach (var clip in track.GetClips())
                    {
                        MyPlayableClip myclip = (MyPlayableClip)clip.asset;
                        MyPlayableBehaviour mybehav = myclip.template;
                        mybehav.MyFloat = 56666;
                        myclip.am.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myclip.am.exposedName, victim);
                    }
                }
                else if (track.name == "Attacker Animation")
                    pd.SetGenericBinding(track, attacker.ac.anim);
                else if (track.name == "Victim Animation")
                    pd.SetGenericBinding(track, victim.ac.anim);

            }

            

            pd.Evaluate();
            pd.Play();
        }


        else if (timelineName == "openBox")
        {
            pd.playableAsset = Instantiate(openBox);

            //取到第一层级Timeline
            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

            //取track
            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Player Script")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {

                        MyPlayableClip myclip = (MyPlayableClip)clip.asset;
                        MyPlayableBehaviour mybehav = myclip.template;
                        myclip.am.exposedName = System.Guid.NewGuid().ToString();

                        pd.SetReferenceValue(myclip.am.exposedName, attacker);

                    }
                }
                else if (track.name == "Box Script")
                {
                    pd.SetGenericBinding(track, victim);
                    foreach (var clip in track.GetClips())
                    {
                        MyPlayableClip myclip = (MyPlayableClip)clip.asset;
                        MyPlayableBehaviour mybehav = myclip.template;
                        myclip.am.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myclip.am.exposedName, victim);
                    }
                }
                else if (track.name == "Player Animation")
                    pd.SetGenericBinding(track, attacker.ac.anim);
                else if (track.name == "Box Animation")
                    pd.SetGenericBinding(track, victim.ac.anim);

            }



            pd.Evaluate();
            pd.Play();
        }
        else if (timelineName == "leverUp")
        {
            pd.playableAsset = Instantiate(leverUp);

            //取到第一层级Timeline
            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

            //取track
            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Player Script")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {

                        MyPlayableClip myclip = (MyPlayableClip)clip.asset;
                        MyPlayableBehaviour mybehav = myclip.template;
                        myclip.am.exposedName = System.Guid.NewGuid().ToString();

                        pd.SetReferenceValue(myclip.am.exposedName, attacker);

                    }
                }
                else if (track.name == "Lever Script")
                {
                    pd.SetGenericBinding(track, victim);
                    foreach (var clip in track.GetClips())
                    {
                        MyPlayableClip myclip = (MyPlayableClip)clip.asset;
                        MyPlayableBehaviour mybehav = myclip.template;
                        myclip.am.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myclip.am.exposedName, victim);
                    }
                }
                else if (track.name == "Player Animation")
                    pd.SetGenericBinding(track, attacker.ac.anim);
                else if (track.name == "Lever Animation")
                    pd.SetGenericBinding(track, victim.ac.anim);

            }



            pd.Evaluate();
            pd.Play();
        }

    }
}
