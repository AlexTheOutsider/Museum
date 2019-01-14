using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
	public float interactiveRange = 2f;
	public float throwForce = 500f;
	public Transform acceptor;
	public InteractiveDoor door;
	
	private Transform player;
	private Transform contextMenu;
	private Transform prevParent;
	
	private Transform itemMenu;
	private ItemMenuManager itemMenuManager;
	Dictionary<String, String> dictPickup;
	Dictionary<String, String> dictLocked;
	Dictionary<String, String> dictUse;
	
	private float distance;
	private bool isHolding = false;
	private bool shouldMenuBeClosed = true;

	private void Start()
	{
		player = GameObject.FindWithTag("Player").transform.GetChild(0);
		contextMenu = transform.Find("ContextMenu");
		prevParent = transform.parent;
		
		itemMenu = GameObject.Find("ItemMenu").transform;
		InitializeItemMenu();
	}

	private void Update ()
	{
		distance = Vector3.Distance(transform.position, player.Find("Guide").position);
		//print("Distance: " + distance);
		if (distance >= interactiveRange)
		{
			OnInteractiveObjExit();
			if( GetComponent<Rigidbody>().velocity.magnitude < 0.01f){
				 //GetComponent<Rigidbody>().isKinematic = true;
			}
		}
		else
		{
			OnInteractiveObjEnter();
			ContextMenuUpdate();
			PickUpAndDrop();
			Throw();
		}
	}
	
	void OnInteractiveObjEnter()
	{
		//contextMenu.gameObject.SetActive(true);

		itemMenu.gameObject.SetActive(true);
		itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
		itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.UsePanel, dictUse);
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
		if (isHolding == true)
		{
			contextMenu.Find("Pickup").gameObject.SetActive(false);
			contextMenu.Find("Drop").gameObject.SetActive(true);
			contextMenu.Find("Throw").gameObject.SetActive(true);
			
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel,false);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel,false);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel,true);
			
		}
		else
		{
			contextMenu.Find("Pickup").gameObject.SetActive(true);
			contextMenu.Find("Drop").gameObject.SetActive(false);
			contextMenu.Find("Throw").gameObject.SetActive(false);
			
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel,true);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel,false);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel,false);
		}
	}

	void PickUpAndDrop()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (isHolding == false)
			{
				transform.SetParent(player.Find("Guide"));
				transform.position = player.Find("Pickup").position;
				transform.rotation = player.Find("Pickup").rotation;
				 GetComponent<Rigidbody>().isKinematic = true;
				 GetComponent<Rigidbody>().velocity = Vector3.zero;
				 GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
				isHolding = true;
			}
			else
			{
				transform.SetParent(prevParent);
				 GetComponent<Rigidbody>().isKinematic = false;
				isHolding = false;
			}
		}
	}

	void Throw()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (isHolding == true)
			{
				transform.SetParent(prevParent);
				 GetComponent<Rigidbody>().isKinematic = false;
				
				// GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * throwForce);
				Vector3 direction = player.Find("Aim").position - transform.position;
				 GetComponent<Rigidbody>().AddForce(direction.normalized * throwForce);

				isHolding = false;
			}
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == acceptor.GetChild(0).name)
		{
			acceptor.gameObject.SetActive(false);

			 GetComponent<Rigidbody>().velocity = Vector3.zero;
			 GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			transform.position = acceptor.position;
			transform.rotation = acceptor.rotation;
			transform.SetParent(prevParent);
			 GetComponent<Rigidbody>().isKinematic = true;
			isHolding = false;

			door.isLocked = false;
			door.OpenDoorAuto();
		}
	}

	private void InitializeItemMenu()
	{
		itemMenuManager = itemMenu.GetComponent<ItemMenuManager>();
		dictPickup = new Dictionary<String, String>();
		dictLocked = new Dictionary<String, String>();
		dictUse = new Dictionary<String, String>();
		
		dictPickup.Add("Text", "Pickup");
		dictUse.Add("Function 1", "<color=aqua>LEFT CLICK</color> to Throw");
		dictUse.Add("Exit", "Press <color=aqua>E</color> to Drop");
	}
}