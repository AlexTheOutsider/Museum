using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void OnEnable()
	{
		EventManager.Instance.StartListening("Switch",ActivateSwitch);
	}

	private void OnDisable()
	{
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