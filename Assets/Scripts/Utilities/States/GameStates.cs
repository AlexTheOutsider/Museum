using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WelcomeState : FSM<Intro>.State
{
    public override void OnEnter()
    {
        EventManager.Instance.StartListening("SwitchEvent", SwitchEvent);
    }

    private void SwitchEvent()
    {
        Parent.TransitionTo<PlayState>();
    }

    public override void Update()
    {
        if (Input.anyKey)
        {
            //camera animation
            Context.playerDirector.Play();
            //ui animation
            Context.title.DOFade(0, 3).SetEase(Ease.OutSine);
            Context.title.rectTransform.DOScale(3, 3).SetEase(Ease.OutSine);
            Context.start.DOFade(0, 1).SetEase(Ease.OutFlash);
        }
    }

    public override void OnExit()
    {
        Context.introMenu.gameObject.SetActive(false);
        Context.menuCam.gameObject.SetActive(false);
        EventManager.Instance.StopListening("SwitchEvent", SwitchEvent);
    }
}

public class PlayState : FSM<Intro>.State
{
    public override void OnEnter()
    {
        Context.playerController.GetComponent<FirstPersonController>().enabled = true;
        Context.playerCam.enabled = true;
        
        EventManager.Instance.StartListening("CutSceneEvent", CutSceneEvent);
    }

    public override void OnExit()
    {
        EventManager.Instance.StopListening("CutSceneEvent", CutSceneEvent);
    }

    private void CutSceneEvent()
    {
        TransitionTo<CutsceneState>();
    }
}

public class CutsceneState : FSM<Intro>.State
{
    public override void OnEnter()
    {
        Context.playerController.GetComponent<FirstPersonController>().enabled = false;
        EventManager.Instance.StartListening("ResumeGameEvent", ResumeGame);
    }

    public override void OnExit()
    {
        EventManager.Instance.StopListening("ResumeGameEvent", ResumeGame);
    }

    private void ResumeGame()
    {
        TransitionTo<PlayState>();
    }
}
