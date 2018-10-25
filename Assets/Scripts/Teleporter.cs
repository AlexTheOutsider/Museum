using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour {
	public GameObject guide;
	public Canvas promptText;

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == guide.name)
		{
			promptText.gameObject.SetActive(true);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.name == guide.name)
		{
			Teleport();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.name == guide.name)
		{
			promptText.gameObject.SetActive(false);
		}
	}
	
	void Teleport()	
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			SceneManager.LoadScene(0);
		}
	}
}
