using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float x;
	public float y;
	public float z;

	public float degree;
	public Transform center;
	
	private Vector3 marginPoint;
	private Vector3 centerPoint;
	private Vector3 axis;

	void Start()
	{
		if (center != null)
		{
			marginPoint = new Vector3(center.position.x + center.lossyScale.x, 0,
				center.position.z + center.lossyScale.z);
			centerPoint = center.position - transform.position;
		}
	}

	void Update()
	{
		transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);
		
		if (center != null)
		{
			CalculateRotationAxis();
			transform.RotateAround(center.position, axis, degree * Time.deltaTime);
		}
	}

	void CalculateRotationAxis()
	{
		axis = Vector3.Cross(marginPoint, centerPoint);
	}
}