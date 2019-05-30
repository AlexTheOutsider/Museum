using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Projector : MonoBehaviour
{
	public Door doorBalcony;
	public Door doorEntry;
	public GameObject player;
	public Transform star;
	
	public AudioClip music;
    public AudioClip sound;
	public Dialogue dialogue;
	
	private int activatedNum = 0;
	private bool isActivated = false;
	private Sequence mySequence;

	private void OnEnable()
	{
		EventManager.Instance.StartListening("Switch",ActivateSwitch);
	}

	private void OnDisable()
	{
		EventManager.Instance.StopListening("Switch",ActivateSwitch);
	}

	private void Update ()
	{
		if (activatedNum == 4 && !isActivated)
		{
			StartCoroutine(StartProject());
			//Intro.Instance.gameState = Intro.State.Cutscenes;
			EventManager.Instance.TriggerEvent("CutSceneEvent");
			isActivated = true;
		}
	}

	IEnumerator StartProject()
	{
		//EventManager.Instance.TriggerEvent("DoorClose");
		doorEntry.CloseDoorAuto();
		
		GetComponent<UnityEngine.Projector>().enabled = true;
		DialogueManager.Instance.StartDialogue(dialogue,DialogueManager.DialogueType.Fade,true);
		mySequence = DOTween.Sequence();
		mySequence.Append(player.transform.DOLookAt(star.position,1).SetEase(Ease.InOutSine))
			.AppendCallback(() =>
				{
					//print("player rotation after animation: "+player.transform.eulerAngles);
					//print("camera rotation after animation: "+player.transform.GetChild(0).eulerAngles);
					float anglesX = player.transform.eulerAngles.x;
					player.transform.localRotation = Quaternion.Euler(0f,player.transform.eulerAngles.y,0f);
					player.transform.GetChild(0).localRotation = Quaternion.Euler(player.transform.GetChild(0).eulerAngles.x+anglesX,0f,0f);
					//Intro.Instance.gameState = Intro.State.Playing;
					EventManager.Instance.TriggerEvent("ResumeGameEvent");
			});
		yield return new WaitForSeconds(3);
		
		MusicManager.Instance.GetComponent<AudioSource>().loop = false;
		MusicManager.Instance.playMusic(sound);
		transform.Find("Spot Light").GetComponent<Light>().enabled = true;
		yield return new WaitForSeconds(2);
		
		transform.Find("Point Light").GetComponent<Light>().enabled = true;
		transform.Find("Spot Light").GetComponent<Light>().enabled = false;
		MusicManager.Instance.playMusic(sound);
		yield return new WaitForSeconds(1);
		
		GetComponent<Rotator>().enabled = true;
		MusicManager.Instance.GetComponent<AudioSource>().loop = true;
		MusicManager.Instance.playMusic(music);
		
		//doorBalcony.UnlockDoor();
		//EventManager.Instance.TriggerEvent("DoorUnlock");
	}

	private void ActivateSwitch()
	{
		if (activatedNum < 4)
		{
			activatedNum++;
		}
	}
}