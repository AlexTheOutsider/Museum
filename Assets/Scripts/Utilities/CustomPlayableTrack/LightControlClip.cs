using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LightControlClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField]
    private LightControlBehaviour template = new LightControlBehaviour();
    
    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<LightControlBehaviour>.Create(graph, template);
    }
}