using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Telescope : MonoBehaviour
{
	public float interactiveRange = 1f;
	public Transform main;
	public Transform telescope;
	
	private Transform player;
	private Transform contextMenu;
	private float distance;
	private bool isActivated = false;

	void Start()
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
			contextMenu.Find("Observe").gameObject.SetActive(true);
			contextMenu.Find("Exit").gameObject.SetActive(false);
			contextMenu.Find("Zoom").gameObject.SetActive(false);
		}
		else
		{
			contextMenu.Find("Observe").gameObject.SetActive(false);
			contextMenu.Find("Exit").gameObject.SetActive(true);
			contextMenu.Find("Zoom").gameObject.SetActive(true);
		}
	}

	void Activate()
	{
		if (Input.GetKeyDown(KeyCode.E) && (isActivated == false))
		{
			main.parent.gameObject.SetActive(false);
			telescope.gameObject.SetActive(true);
			isActivated = true;
		}
		else if (Input.GetKeyDown(KeyCode.E) && (isActivated == true))
		{
			main.parent.gameObject.SetActive(true);
			telescope.gameObject.SetActive(false);
			isActivated = false;
		}
	}
}