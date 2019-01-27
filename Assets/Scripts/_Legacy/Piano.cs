using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using Random = System.Random;

public class Piano : MonoBehaviour
{
	public float interactiveRange = 2f;
	public AudioClip[] keyNote;
	
	private Transform player;
	private Transform contextMenu;
	
	private Transform itemMenu;
	private ItemMenuManager itemMenuManager;
	Dictionary<String, String> dictPickup;
	Dictionary<String, String> dictLocked;
	Dictionary<String, String> dictUse;
	
	private float distance;
	private bool shouldMenuBeClosed = true;

	private void Start()
	{
		player = GameObject.FindWithTag("Player").transform.GetChild(0);
		contextMenu = transform.Find("ContextMenu");
		
		itemMenu = GameObject.Find("ItemMenu").transform;
		InitializeItemMenu();
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
			ContextMenuUpdate();
			Play();
		}
	}
	
	void OnInteractiveObjEnter()
	{
		//contextMenu.gameObject.SetActive(true);

		itemMenu.gameObject.SetActive(true);
		itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
		shouldMenuBeClosed = true;
	}
	
	public void OnInteractiveObjExit()
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
		itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel, true);
		itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel, false);
		itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel, false);
	}

	void Play()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			GetComponent<AudioSource>().clip = keyNote[UnityEngine.Random.Range(1, 3)];
			GetComponent<AudioSource>().Play();
		}
	}
	
	private void InitializeItemMenu()
	{
		itemMenuManager = itemMenu.GetComponent<ItemMenuManager>();
		dictPickup = new Dictionary<String, String>();
		dictLocked = new Dictionary<String, String>();
		dictUse = new Dictionary<String, String>();
		
		dictPickup.Add("Text", "Play");
	}
}