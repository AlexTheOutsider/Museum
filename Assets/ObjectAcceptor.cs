using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAcceptor : MonoBehaviour
{
	public Material initialMaterial;
	public Material activatedMaterial;
	// Use this for initialization
	void Start ()
	{
		initialMaterial = transform.parent.GetComponent<MeshRenderer>().material;
		//activatedMaterial = transform.parent.GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		print("enter trigger from acceptor!!");
		transform.parent.GetComponent<MeshRenderer>().material = activatedMaterial;
	}

	private void OnTriggerExit(Collider other)
	{
		transform.parent.GetComponent<MeshRenderer>().material = initialMaterial;
	}
}
