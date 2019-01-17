using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.3f,0.3f,0.3f)]
[TrackBindingType(typeof(Light))]
[TrackClipType(typeof(LightControlClip))]
public class LightControlTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<LightControlMixer>.Create(graph, inputCount);
    }
}
