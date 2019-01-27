using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveDoor : MonoBehaviour {

	public bool isLocked = true;
	public float interactiveRange = 1f;
	public MusicManager musicManager;
	public AudioClip music;
	
	private Transform player;
	private Transform contextMenu;
	private Animator animator;
	
	private Transform itemMenu;
	private ItemMenuManager itemMenuManager;
	Dictionary<String, String> dictPickup;
	Dictionary<String, String> dictLocked;
	Dictionary<String, String> dictUse;
	
	private float distance;
	private bool isDisabled = false;
	private bool shouldMenuBeClosed = true;

	void Start()
	{
		player = GameObject.FindWithTag("Player").transform.GetChild(0);
		contextMenu = transform.Find("ContextMenu");
		animator = transform.parent.GetComponent<Animator>();
		
		itemMenu = GameObject.Find("ItemMenu").transform;
		InitializeItemMenu();
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
		//contextMenu.gameObject.SetActive(true);
		itemMenu.gameObject.SetActive(true);
		itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
		itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.LockedPanel, dictLocked);
		shouldMenuBeClosed = true;
	}

	void OnInteractiveObjExit()
	{
		//contextMenu.gameObject.SetActive(false);
		if (shouldMenuBeClosed)
		{
			itemMenu.gameObject.SetActive(false);
			shouldMenuBeClosed = false;
		}
	}

	void ContextMenuUpdate()
	{
		if (isLocked == true)
		{
			contextMenu.Find("Open").gameObject.SetActive(false);
			contextMenu.Find("Locked").gameObject.SetActive(true);
			
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel,false);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel,true);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel,false);
		}
		else
		{
			contextMenu.Find("Open").gameObject.SetActive(true);
			contextMenu.Find("Locked").gameObject.SetActive(false);
			
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel,true);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel,false);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel,false);
		}
	}

	void OpenDoor()
	{
		if (Input.GetKey(KeyCode.E) && (isLocked == false))
		{
			OpenDoorAuto();
			itemMenu.gameObject.SetActive(false);
			shouldMenuBeClosed = false;
			if (musicManager != null && music != null)
			{
				//musicManager.playMusic(music);
			}
		}
	}
	
	public void OpenDoorAuto()
	{
		animator.SetTrigger("Opening");
		//contextMenu.gameObject.SetActive(false);
		isDisabled = true;
	}
	
	public void CloseDoorAuto()
	{
		animator.SetTrigger("Closing");
		contextMenu.gameObject.SetActive(false);
		isDisabled = true;
	}
	
	private void InitializeItemMenu()
	{
		itemMenuManager = itemMenu.GetComponent<ItemMenuManager>();
		dictPickup = new Dictionary<String, String>();
		dictLocked = new Dictionary<String, String>();
		dictUse = new Dictionary<String, String>();
		
		dictPickup.Add("Text", "Open");
		dictLocked.Add("Text", "The door has been locked.");
	}
}