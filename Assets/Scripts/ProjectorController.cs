using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorController : MonoBehaviour
{
	public int activatedNum = 0;
	public InteractiveDoor door;

	void Update ()
	{
		if (activatedNum == 4)
		{
			StartCoroutine(StartProject());
			door.isLocked = false;
		}
	}

	IEnumerator StartProject()
	{
		GetComponent<Light>().enabled = true;
		yield return new WaitForSeconds(2);
		
		GetComponent<Light>().enabled = false;
		GetComponent<Projector>().enabled = true;
		GetComponent<Rotator>().enabled = true;
	}

	public void ActivateSwitch()
	{
		if (activatedNum < 4)
		{
			activatedNum++;
		}
	}
}