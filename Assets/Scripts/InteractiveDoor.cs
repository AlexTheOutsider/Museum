using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveDoor : MonoBehaviour {

	public bool isLocked = true;
	public float interactiveRange = 1f;
	public MusicController musicController;
	public AudioClip music;
	
	private Transform player;
	private Transform contextMenu;
	private Animator animator;
	
	private float distance;
	private bool isDisabled = false;

	void Start()
	{
		player = GameObject.FindWithTag("Player").transform.GetChild(0);
		contextMenu = transform.Find("ContextMenu");
		animator = transform.parent.GetComponent<Animator>();
	}

	void Update ()
	{
		if (isDisabled)
			return;
		
		distance = Vector3.Distance(transform.position, player.Find("Guide").position);
		if (distance >= interactiveRange)
		{
			OnInteractiveObjExit();
		}
		else
		{
			OnInteractiveObjEnter();
			ContextMenuUpdate();
			OpenDoor();
		}
		
		//if(isLocked == false)
		//	GetComponent<MeshRenderer>().material.color = Color.green;
	}

	void OnInteractiveObjEnter()
	{
		contextMenu.gameObject.SetActive(true);
	}

	void OnInteractiveObjExit()
	{
		contextMenu.gameObject.SetActive(false);
	}

	void ContextMenuUpdate()
	{
		if (isLocked == true)
		{
			contextMenu.Find("Open").gameObject.SetActive(false);
			contextMenu.Find("Locked").gameObject.SetActive(true);
		}
		else
		{
			contextMenu.Find("Open").gameObject.SetActive(true);
			contextMenu.Find("Locked").gameObject.SetActive(false);
		}
	}

	void OpenDoor()
	{
		if (Input.GetKey(KeyCode.E) && (isLocked == false))
		{
			OpenDoorAuto();
			if (musicController != null && music != null)
			{
				//musicController.playMusic(music);
			}
		}
	}
	
	public void OpenDoorAuto()
	{
		animator.SetTrigger("Opening");
		contextMenu.gameObject.SetActive(false);
		isDisabled = true;
	}
}