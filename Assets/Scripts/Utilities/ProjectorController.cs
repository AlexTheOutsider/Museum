using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ProjectorController : MonoBehaviour
{
	public int activatedNum = 0;
	public InteractiveDoor door;
	public InteractiveDoor door2;
	public MusicController musicController;
	public AudioClip music;
	public AudioClip sound;
	public Dialogue dialogue;
	
	private bool isActivated = false;
	public GameObject player;
	public Transform star;
	Sequence mySequence;

	private void OnEnable()
	{
		EventManager.Instance.StartListening("Switch",ActivateSwitch);
	}

	private void OnDisable()
	{
		if (EventManager.Instance == null) return;
		EventManager.Instance.StopListening("Switch",ActivateSwitch);
	}

	void Update ()
	{
		if (activatedNum == 4 && !isActivated)
		{
			StartCoroutine(StartProject());
			isActivated = true;
		}
	}

	IEnumerator StartProject()
	{
		door2.CloseDoorAuto();
		GetComponent<Projector>().enabled = true;
		DialogueManager.Instance.StartDialogue(dialogue,DialogueManager.DialogueType.Fade,true);
		player.GetComponent<FirstPersonController>().enabled = false;
		mySequence = DOTween.Sequence();
		mySequence.Append(player.transform.DOLookAt(star.position,1))
			.AppendCallback(() =>
				{
					print("before player: "+player.transform.eulerAngles);
					print("before cam: "+player.transform.GetChild(0).eulerAngles);
				}
				)
			.AppendInterval(3f)
			.AppendCallback(() =>
			{
				float xOffset = player.transform.eulerAngles.x;
				player.transform.localRotation = Quaternion.Euler(0f,player.transform.eulerAngles.y,0f);
				print("X Offset:  "+xOffset);
				print("X Base:  "+player.transform.GetChild(0).eulerAngles.x);
				player.transform.GetChild(0).localRotation = Quaternion.Euler(player.transform.GetChild(0).eulerAngles.x+xOffset,0f,0f);
				//player.transform.GetChild(0).Rotate(xOffset,0f,0f);
				print("after player: "+player.transform.eulerAngles);
				print("after cam: "+player.transform.GetChild(0).eulerAngles);
				player.GetComponent<FirstPersonController>().enabled = true;
				print("after player: "+player.transform.eulerAngles);
				print("after cam: "+player.transform.GetChild(0).eulerAngles);
			});
		
		yield return new WaitForSeconds(3);
		
		musicController.GetComponent<AudioSource>().loop = false;
		musicController.playMusic(sound);
		transform.Find("Spot Light").GetComponent<Light>().enabled = true;
		yield return new WaitForSeconds(2);
		
		transform.Find("Point Light").GetComponent<Light>().enabled = true;
		transform.Find("Spot Light").GetComponent<Light>().enabled = false;
		musicController.playMusic(sound);
		yield return new WaitForSeconds(1);
		
		GetComponent<Rotator>().enabled = true;
		musicController.GetComponent<AudioSource>().loop = true;
		musicController.playMusic(music);
		
		door.isLocked = false;
	}

	public void ActivateSwitch()
	{
		if (activatedNum < 4)
		{
			activatedNum++;
		}
	}
}