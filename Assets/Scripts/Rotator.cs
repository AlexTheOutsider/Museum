using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
	
	float delta;
	
	public Quaternion rotation = Quaternion.identity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		delta += 1 * Time.deltaTime;
		//transform.rotation = Quaternion.Euler(0, delta, 0);
		rotation.eulerAngles = new Vector3(0, delta, 0);
		transform.rotation = rotation;
		//print(transform.rotation.eulerAngles.y);
	}
}
