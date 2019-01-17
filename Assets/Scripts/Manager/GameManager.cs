using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public PlayableDirector playerDirector;
    private Transform mainMenu;
    private Text title;
    private Text start;

    private void Start()
    {
        mainMenu = GameObject.Find("MainMenu").transform;
        title = mainMenu.Find("Title").GetComponent<Text>();
        start = mainMenu.Find("Start").GetComponent<Text>();
        start.DOFade(0.2f, 3).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
    
    private void Update()
    {
        if (Input.anyKey)
        {
            playerDirector.Play();
            title.DOFade(0, 3);
            title.rectTransform.DOScale(3, 3);
            start.DOFade(0, 1);
        }
    }
}