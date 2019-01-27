using UnityEngine;

public class Door : Interactive {
/*	public MusicManager musicManager;
	public AudioClip music;*/
	
	private Animator animator;
	[SerializeField] private bool isDisabled = false;
	[SerializeField] private bool isLocked = true;

	private void OnEnable()
	{
		EventManager.Instance.StartListening("DoorOpen",OpenDoorAuto);
		EventManager.Instance.StartListening("DoorClose",CloseDoorAuto);
		EventManager.Instance.StartListening("DoorUnlock",UnlockDoor);
	}

	private void OnDisable()
	{
		EventManager.Instance.StopListening("DoorOpen",OpenDoorAuto);
		EventManager.Instance.StopListening("DoorClose",CloseDoorAuto);
		EventManager.Instance.StopListening("DoorUnlock",UnlockDoor);
	}
	
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
		
	protected override void InitializeItemMenu()
	{
		base.InitializeItemMenu();
		
		dictPickup.Add("Text", "Open");
		dictLocked.Add("Text", "The door has been locked.");
	}
	
	protected override void ContextMenuUpdate()
	{
		base.ContextMenuUpdate();
		ItemMenuManager.Instance.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
		ItemMenuManager.Instance.UpdateMenu(ItemMenuManager.ItemMenuType.LockedPanel, dictLocked);
		
		if (isLocked == true)
		{
			ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel,false);
			ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel,true);
			ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel,false);
		}
		else
		{
			ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel,true);
			ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel,false);
			ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel,false);
		}
	}

	protected override void Function()
	{
		base.Function();
		if (Input.GetKey(KeyCode.E))
		{
			OpenDoor();
		}
	}
	
	private void OpenDoor()
	{
		if (isLocked == false)
		{
			OpenDoorAuto();
			
			itemMenu.gameObject.SetActive(false);
			shouldMenuBeClosed = false;
/*			if (musicManager != null && music != null)
			{
				//musicManager.playMusic(music);
			}*/
		}
	}
	
	public void OpenDoorAuto()
	{
		//print("open door!");
		animator.SetTrigger("Opening");
		//contextMenu.gameObject.SetActive(false);
		isDisabled = true;
	}
	
	public void CloseDoorAuto()
	{
		//print("close door!");
		animator.SetTrigger("Closing");
		isDisabled = true;
	}

	public void UnlockDoor()
	{
		isLocked = false;
	}
}