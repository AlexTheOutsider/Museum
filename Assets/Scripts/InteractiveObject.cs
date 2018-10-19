﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
	private float distance;
	private bool isHolding = false;
	private Transform prevParent;

	public float interactiveRange = 1f;
	public float throwForce = 600f;
	public GameObject guide;
	public Canvas promptText;
	public GameObject acceptor;

	private void Start()
	{
		prevParent = transform.parent;
	}

	private void Update ()
	{
		distance = Vector3.Distance(transform.position, guide.transform.position);
		if (distance >= interactiveRange)
		{
			OnInteractiveObjExit();
			
			if(GetComponent<Rigidbody>().velocity.magnitude < 0.01f){
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
		promptText.gameObject.SetActive(true);
	}
	
	public void OnInteractiveObjExit()
	{
		promptText.gameObject.SetActive(false);
	}
	
	void ContextMenuUpdate()
	{
		if (isHolding == true)
		{
			promptText.transform.Find("Pickup").gameObject.SetActive(false);
			promptText.transform.Find("Drop").gameObject.SetActive(true);
			promptText.transform.Find("Throw").gameObject.SetActive(true);
		}
		else
		{
			promptText.transform.Find("Pickup").gameObject.SetActive(true);
			promptText.transform.Find("Drop").gameObject.SetActive(false);
			promptText.transform.Find("Throw").gameObject.SetActive(false);
		}
	}

	void PickUpAndDrop()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (isHolding == false)
			{
				transform.SetParent(guide.transform);
				transform.GetComponent<Rigidbody>().isKinematic = true;
				transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
				transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
				isHolding = true;
			}
			else
			{
				transform.SetParent(prevParent);
				transform.GetComponent<Rigidbody>().isKinematic = false;
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
				Debug.Log("Throw away!");
				transform.SetParent(prevParent);
				transform.GetComponent<Rigidbody>().isKinematic = false;
					
				//transform.parent.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * throwForce);
				Vector3 direction = transform.position - guide.transform.parent.parent.transform.position;
				transform.GetComponent<Rigidbody>().AddForce(direction * throwForce);
					
				isHolding = false;
			}
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == acceptor.transform.GetChild(0).name)
		{
			Transform acceptorTrigger = acceptor.transform.GetChild(0);
			
			//print("Acceptor matched!");
			transform.SetParent(acceptorTrigger);
			transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			transform.position = acceptorTrigger.position;
			transform.rotation = Quaternion.identity;
			isHolding = false;
		}
	}
}