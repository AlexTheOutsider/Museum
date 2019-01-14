using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using Random = System.Random;

public class PianoNew : Interactive
{
	public AudioClip[] keyNote;

	protected override void ContextMenuUpdate()
	{			
		base.ContextMenuUpdate();
		itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
		
		itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel, true);
		itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel, false);
		itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel, false);
	}

	protected override void Function()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			GetComponent<AudioSource>().clip = keyNote[UnityEngine.Random.Range(1, 3)];
			GetComponent<AudioSource>().Play();
		}
	}
	
	protected override void InitializeItemMenu()
	{
		base.InitializeItemMenu();
		dictPickup.Add("Text", "Play");
	}
}