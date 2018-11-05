using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public class Inspection : MonoBehaviour
{
	public float inspectFOV = 30;
	public float normalFOV = 60;
	public float zoomInTime = 0.1f;
	public float smooth = 5f;
	public float angularVelocity = 5f;

	private Camera camera;
	private Coroutine coroutine;

	private void Start()
	{
		camera = GetComponent<Camera>();
	}

	void Update ()
	{
		ZoomIn();
		AdjustPerspective();
	}

	IEnumerator ChangeFOV(float originalFOV, float targetFOV)
	{
		float timeAcc = 0;
		while (timeAcc <= zoomInTime)
		{
			timeAcc += Time.deltaTime;
			camera.fieldOfView = Mathf.Lerp(originalFOV, targetFOV, timeAcc / zoomInTime);
			yield return null;
		}
	}

	void ZoomIn()
	{
		/*if (Input.GetMouseButtonDown(1))
		{
			if (coroutine != null)
			{
				StopCoroutine(coroutine);
			}

			coroutine = StartCoroutine(ChangeFOV(camera.fieldOfView, inspectFOV));
		}
		else if (Input.GetMouseButtonUp(1))
		{
			if (coroutine != null)
			{
				StopCoroutine(coroutine);
			}
			coroutine = StartCoroutine(ChangeFOV(camera.fieldOfView, normalFOV));
		}*/

		float target = inspectFOV * (Input.GetAxis("Fire2")) + normalFOV * (1 - Input.GetAxis("Fire2"));
		camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, target, Time.deltaTime * smooth);
	}
	
	void AdjustPerspective()
	{
		print("Vector3: " + transform.rotation.eulerAngles);
		if (Input.GetKey(KeyCode.W))
		{
			transform.Rotate(-angularVelocity * Time.deltaTime, 0f, 0f);
			if (Mathf.Abs( transform.rotation.eulerAngles.x - 340) < 0.1f)
			{
				transform.rotation = Quaternion.Euler(340,
					transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
			}
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.Rotate(angularVelocity * Time.deltaTime, 0f, 0f);
			if (Mathf.Abs( transform.rotation.eulerAngles.x - 20) < 0.1f)
			{
				transform.rotation = Quaternion.Euler(20,
					transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
			}
		}
		
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0f, -angularVelocity * Time.deltaTime, 0f, Space.World);
			if (Mathf.Abs( transform.rotation.eulerAngles.y - 330) < 0.1f)
			{
				 transform.rotation = Quaternion.Euler( transform.rotation.eulerAngles.x,
					330,  transform.rotation.eulerAngles.z);
			}
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0f, angularVelocity * Time.deltaTime, 0f, Space.World);
			if (Mathf.Abs( transform.rotation.eulerAngles.y - 30) < 0.1f)
			{
				transform.rotation = Quaternion.Euler( transform.rotation.eulerAngles.x,
					30,  transform.rotation.eulerAngles.z);
			}
		}
	}
}