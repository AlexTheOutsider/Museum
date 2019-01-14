using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : Interactive {
	public bool isLocked = true;
	public MusicController musicController;
	public AudioClip music;
	
	private Animator animator;
	private bool isDisabled = false;

	protected override void Start()
	{
		base.Start();
		animator = transform.parent.GetComponent<Animator>();
	}

	protected override void Update ()
	{
		if (isDisabled)
			return;
		base.Update();
	}
		
	protected override  void InitializeItemMenu()
	{
		itemMenuManager = itemMenu.GetComponent<ItemMenuManager>();
		dictPickup = new Dictionary<String, String>();
		dictLocked = new Dictionary<String, String>();
		dictUse = new Dictionary<String, String>();
		
		dictPickup.Add("Text", "Open");
		dictLocked.Add("Text", "The door has been locked.");
	}
	
	protected override void ContextMenuUpdate()
	{
		base.ContextMenuUpdate();
		itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
		itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.LockedPanel, dictLocked);
		
		if (isLocked == true)
		{
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel,false);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel,true);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel,false);
		}
		else
		{
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel,true);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel,false);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel,false);
		}
	}

	protected override void Function()
	{
		base.Function();
		OpenDoor();
	}
	
	private void OpenDoor()
	{
		if (Input.GetKey(KeyCode.E) && (isLocked == false))
		{
			OpenDoorAuto();
			itemMenu.gameObject.SetActive(false);
			shouldMenuBeClosed = false;
			if (musicController != null && music != null)
			{
				//musicController.playMusic(music);
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
		isDisabled = true;
	}
}