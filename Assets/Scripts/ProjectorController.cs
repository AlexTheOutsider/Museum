using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorController : MonoBehaviour
{
	public int activatedNum = 0;
	public InteractiveDoor door;
	public MusicController musicController;
	public AudioClip music;
	public AudioClip sound;
	
	private bool isActivated = false;

	void Update ()
	{
		if (activatedNum == 4 && !isActivated)
		{
			StartCoroutine(StartProject());
			door.isLocked = false;
			isActivated = true;
		}
	}

	IEnumerator StartProject()
	{
		transform.GetChild(0).GetComponent<Light>().enabled = true;
		musicController.GetComponent<AudioSource>().loop = false;
		musicController.playMusic(sound);
		yield return new WaitForSeconds(2);
		
		transform.GetChild(0).GetComponent<Light>().enabled = false;
		GetComponent<Projector>().enabled = true;
		GetComponent<Rotator>().enabled = true;
		musicController.GetComponent<AudioSource>().loop = true;
		musicController.playMusic(music);
	}

	public void ActivateSwitch()
	{
		if (activatedNum < 4)
		{
			activatedNum++;
		}
	}
}