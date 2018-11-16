using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip project;
    public AudioClip balcony;
    public MusicController musicController;
    
    private void OnTriggerEnter(Collider other)
    {
        musicController.playMusic(balcony);
    }

    private void OnTriggerExit(Collider other)
    {
        musicController.playMusic(project);
    }
}
