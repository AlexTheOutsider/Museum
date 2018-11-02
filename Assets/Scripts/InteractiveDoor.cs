using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveDoor : MonoBehaviour {

	public bool isLocked = true;
	public Canvas contextMenu;
	public float interactiveRange = 2f;
	public GameObject player;
	public Animator animator;
	
	private float distance;

	void Update () {
		distance = Vector3.Distance(transform.position, player.transform.Find("Guide").position);
		//print(distance);
		if (distance >= interactiveRange)
		{
			OnInteractiveObjExit();
		}
		else
		{
			OnInteractiveObjEnter();
			ContextMenuUpdate();

			OpenDoor();
		}
	}

	void OnInteractiveObjEnter()
	{
		contextMenu.gameObject.SetActive(true);
	}

	void OnInteractiveObjExit()
	{
		contextMenu.gameObject.SetActive(false);
	}

	void ContextMenuUpdate()
	{
		if (isLocked == true)
		{
			contextMenu.transform.Find("Open").gameObject.SetActive(false);
			contextMenu.transform.Find("Locked").gameObject.SetActive(true);
		}
		else
		{
			contextMenu.transform.Find("Open").gameObject.SetActive(true);
			contextMenu.transform.Find("Locked").gameObject.SetActive(false);
		}
	}

	void OpenDoor()
	{
		if (Input.GetKey(KeyCode.E) && (isLocked == false))
		{
			contextMenu.gameObject.SetActive(false);
			print("Door opened!");
			animator.SetTrigger("Opening");
		}
	}
}
