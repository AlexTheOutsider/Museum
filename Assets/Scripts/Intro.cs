using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Intro : Singleton<Intro>
{
    public enum State
    {
        Welcome,
        Playing,
        Cutscenes,
    }
    
    public PlayableDirector playerDirector;
    public State gameState;
    private State gameStatePrev;

    private FSM<Intro> _fsm;
    
    public Transform introMenu;
    public Transform playerController;
    public Text title;
    public Text start;
    
    public Camera playerCam;
    public Camera menuCam;

    private void Awake()
    {
        DOTween.SetTweensCapacity(500, 500);
        //gameState = State.Welcome;
        //gameStatePrev = State.Cutscenes;
        
        _fsm = new FSM<Intro>(this);
    }

    private void Start()
    {
        playerController = GameObject.Find("FPSController").transform;
        introMenu = GameObject.Find("IntroMenu").transform;
        title = introMenu.Find("Title").GetComponent<Text>();
        start = introMenu.Find("Start").GetComponent<Text>();
        playerCam = playerController.GetChild(0).GetComponent<Camera>();
        menuCam = GameObject.Find("GameManager").transform.GetChild(0).GetComponent<Camera>();
        
        start.DOFade(0.2f, 3).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
        
        _fsm.TransitionTo<WelcomeState>();
    }
    
    private void Update()
    {
        _fsm.Update();
        return;
        
        switch (gameState)
        {
            case State.Welcome:
                if (Input.anyKey)
                {
                    //camera animation
                    playerDirector.Play();
                    //ui animation
                    title.DOFade(0, 3).SetEase(Ease.OutSine);
                    title.rectTransform.DOScale(3, 3).SetEase(Ease.OutSine);
                    start.DOFade(0, 1).SetEase(Ease.OutFlash);
                }
                break;
            
            case State.Playing:
                playerController.GetComponent<FirstPersonController>().enabled = true;
                playerCam.enabled = true;

                introMenu.gameObject.SetActive(false);
                menuCam.gameObject.SetActive(false);
                break;
            
            case State.Cutscenes:
                playerController.GetComponent<FirstPersonController>().enabled = false;
                break;
        }

        //gameStatePrev = gameState;
    }
}