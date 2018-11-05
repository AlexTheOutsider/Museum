using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
	public float interactiveRange = 2f;
	
	private Transform player;
	private Transform contextMenu;
	
	private float distance;

	private void Start()
	{
		player = GameObject.FindWithTag("Player").transform.GetChild(0);
		contextMenu = transform.Find("ContextMenu");
	}

	private void Update ()
	{
		distance = Vector3.Distance(transform.position, player.Find("Guide").position);
		if (distance >= interactiveRange)
		{
			OnInteractiveObjExit();
		}
		else
		{
			OnInteractiveObjEnter();
			Play();
		}
	}
	
	void OnInteractiveObjEnter()
	{
		contextMenu.gameObject.SetActive(true);
	}
	
	public void OnInteractiveObjExit()
	{
		contextMenu.gameObject.SetActive(false);
	}

	void Play()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			GetComponent<AudioSource>().Play();
		}
	}
}