using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
	public AudioClip bgm;

	private AudioSource audioSource;
	
	private void Start ()
	{
		//default bgm
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