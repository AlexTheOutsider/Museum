using UnityEngine;

public class PianoNew : Interactive
{
	public AudioClip[] keyNote;
	
	protected override void InitializeItemMenu()
	{
		base.InitializeItemMenu();
		dictPickup.Add("Text", "Play");
	}
	
	protected override void ContextMenuUpdate()
	{			
		base.ContextMenuUpdate();
		ItemMenuManager.Instance.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
		
		ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel, true);
		ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel, false);
		ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel, false);
	}

	protected override void Function()
	{
		base.Function();
		
		if (Input.GetKeyDown(KeyCode.E))
		{
			GetComponent<AudioSource>().clip = keyNote[Random.Range(1, 3)];
			GetComponent<AudioSource>().Play();
		}
	}
}