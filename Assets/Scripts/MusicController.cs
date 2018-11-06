using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

	public AudioClip bgm;

	private AudioSource audioSource;
	
	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
		playMusic(bgm);
	}

	public void playMusic(AudioClip music)
	{
		audioSource.clip = music;
		audioSource.Play();
		//print("Play: "+ music.name);
	}
}
