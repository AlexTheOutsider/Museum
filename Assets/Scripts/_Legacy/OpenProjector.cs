using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenProjector : MonoBehaviour
{
	public UnityEngine.Projector projector;
	public GameObject door;
	
	private bool firstTime = true;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (firstTime)
		{
			projector.enabled = true;
			door.GetComponent<InteractiveDoor>().isLocked = false;
			firstTime = false;
			print("Projector opened!");
		}
	}
}
