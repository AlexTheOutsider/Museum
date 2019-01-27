using UnityEngine;

public class Rotator : MonoBehaviour
{
	public Vector3 selfRotation;

	public float revolutionSpeed;
	public Transform revolutionCenter;
	
	private Vector3 marginPoint;
	private Vector3 centerPoint;
	private Vector3 axis;

	void Start()
	{
		if (revolutionCenter != null)
		{
			marginPoint = new Vector3(revolutionCenter.position.x + revolutionCenter.lossyScale.x, 0,
				revolutionCenter.position.z + revolutionCenter.lossyScale.z);
			centerPoint = revolutionCenter.position - transform.position;
		}
	}

	void Update()
	{
		transform.Rotate(selfRotation * Time.deltaTime);
		
		if (revolutionCenter != null)
		{
			CalculateRotationAxis();
			transform.RotateAround(revolutionCenter.position, axis, revolutionSpeed * Time.deltaTime);
		}
	}

	void CalculateRotationAxis()
	{
		axis = Vector3.Cross(marginPoint, centerPoint);
	}
}