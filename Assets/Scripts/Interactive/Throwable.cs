using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Interactive
{
	public float throwForce = 500f;
	public Transform acceptor;
	public Door door;

	private Transform prevParent;
	private bool isHolding = false;

	protected override void Start()
	{
		base.Start();
		prevParent = transform.parent;
	}
	
	protected override void InitializeItemMenu()
	{
		base.InitializeItemMenu();
		dictPickup.Add("Text", "Pickup");
		dictUse.Add("Function 1", "<color=aqua>LEFT CLICK</color> to Throw");
		dictUse.Add("Exit", "Press <color=aqua>E</color> to Drop");
	}
	
	protected override void ContextMenuUpdate()
	{
		base.ContextMenuUpdate();
		itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
		itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.UsePanel, dictUse);
		
		if (isHolding == true)
		{	
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel,false);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel,false);
			itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel,true);
			
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
		PickUpAndDrop();
		Throw();
	}
	
	private void PickUpAndDrop()
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

	private void Throw()
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
			EventManager.Instance.TriggerEvent("Door");
		}
	}
}