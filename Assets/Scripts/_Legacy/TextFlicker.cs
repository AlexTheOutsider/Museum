using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TextFlicker : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().DOFade(0.2f, 3).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
