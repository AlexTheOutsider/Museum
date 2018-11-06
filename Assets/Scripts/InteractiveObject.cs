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
	
	private float distance;
	private bool isHolding = false;

	private void Start()
	{
		player = GameObject.FindWithTag("Player").transform.GetChild(0);
		contextMenu = transform.Find("ContextMenu");
		prevParent = transform.parent;
	}

	private void Update ()
	{
		distance = Vector3.Distance(transform.position, player.Find("Guide").position);
		//print("Distance: " + distance);
		if (distance >= interactiveRange)
		{
			OnInteractiveObjExit();
			if( GetComponent<Rigidbody>().velocity.magnitude < 0.01f){
				 GetComponent<Rigidbody>().isKinematic = true;
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
		contextMenu.gameObject.SetActive(true);
	}
	
	public void OnInteractiveObjExit()
	{
		contextMenu.gameObject.SetActive(false);
	}
	
	void ContextMenuUpdate()
	{
		if (isHolding == true)
		{
			contextMenu.Find("Pickup").gameObject.SetActive(false);
			contextMenu.Find("Drop").gameObject.SetActive(true);
			contextMenu.Find("Throw").gameObject.SetActive(true);
		}
		else
		{
			contextMenu.Find("Pickup").gameObject.SetActive(true);
			contextMenu.Find("Drop").gameObject.SetActive(false);
			contextMenu.Find("Throw").gameObject.SetActive(false);
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
}