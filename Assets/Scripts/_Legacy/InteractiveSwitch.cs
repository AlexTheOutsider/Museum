using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractiveSwitch : MonoBehaviour
{
	public float interactiveRange = 1f;
	public Projector projector;
	
	private Transform player;
	private Transform contextMenu;
	
	private float distance;
	private bool isActivated = false;

	private void Start()
	{
		player = GameObject.FindWithTag("Player").transform.GetChild(0);
		contextMenu = transform.Find("ContextMenu");
	}
	
	void Update ()
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
			Activate();
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
		if (isActivated == false)
		{
			contextMenu.Find("Activate").gameObject.SetActive(true);
		}
		else
		{
			contextMenu.Find("Activate").gameObject.SetActive(false);
		}
	}

	void Activate()
	{
		if (Input.GetKey(KeyCode.E) && (isActivated == false))
		{
			//projector.ActivateSwitch();
			
			gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
			transform.GetChild(0).GetComponent<Light>().color = Color.blue;
			isActivated = true;
		}
	}
}