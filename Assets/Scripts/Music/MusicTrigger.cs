using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip project;
    public AudioClip balcony;
    
    private void OnTriggerEnter(Collider other)
    {
        MusicManager.Instance.playMusic(balcony);
    }

    private void OnTriggerExit(Collider other)
    {
        MusicManager.Instance.playMusic(project);
    }
}