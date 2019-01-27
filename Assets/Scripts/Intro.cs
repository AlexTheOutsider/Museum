using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public PlayableDirector playerDirector;
    
    private Transform introMenu;
    private Text title;
    private Text start;

    private void Awake()
    {
        DOTween.SetTweensCapacity(500, 500);
    }

    private void Start()
    {
        introMenu = GameObject.Find("IntroMenu").transform;
        title = introMenu.Find("Title").GetComponent<Text>();
        start = introMenu.Find("Start").GetComponent<Text>();
        
        start.DOFade(0.2f, 3).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
    
    private void Update()
    {
        if (Input.anyKey)
        {
            //camera animation
            playerDirector.Play();
            //ui animation
            title.DOFade(0, 3).SetEase(Ease.OutSine);
            title.rectTransform.DOScale(3, 3).SetEase(Ease.OutSine);
            start.DOFade(0, 1).SetEase(Ease.OutFlash);
        }
    }
}