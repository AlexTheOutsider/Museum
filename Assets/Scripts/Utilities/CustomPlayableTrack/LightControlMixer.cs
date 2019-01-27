using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LightControlMixer : PlayableBehaviour
{
    private Light light;
    private bool firstFrameHappened;

    private Color defaultColor;
    private float defaultIntensity;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        light = playerData as Light;
        if (light == null)
            return;

        if (!firstFrameHappened)
        {
            defaultColor = Color.white;
            defaultIntensity = 1f;

            firstFrameHappened = true;
        }

        int inputCount = playable.GetInputCount();

        Color blendedColor = Color.clear;
        float blendedIntensity = 0f;
        float totalWeight = 0f;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<LightControlBehaviour> inputPlayable =
                (ScriptPlayable<LightControlBehaviour>) playable.GetInput(i);
            LightControlBehaviour behaviour = inputPlayable.GetBehaviour();

            blendedColor += behaviour.color * inputWeight;
            blendedIntensity += behaviour.intensity * inputWeight;

            totalWeight += inputWeight;
        }

        float remainingWeight = 1 - totalWeight;
        light.color = blendedColor + defaultColor * remainingWeight;
        light.intensity = blendedIntensity + defaultIntensity * remainingWeight;
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        firstFrameHappened = false;

        if (light == null)
            return;

        light.color = defaultColor;
        light.intensity = defaultIntensity;
    }
}