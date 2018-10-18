using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Inspection : MonoBehaviour
{
	public float inspectFOV = 30;
	public float normalFOV = 60;
	public float zoomInTime = 0.3f;

	void Update () {
		if (Input.GetMouseButtonDown(1))
		{
			StartCoroutine(ChangeFOV(normalFOV, inspectFOV));
		}
		else if (Input.GetMouseButtonUp(1))
		{
			StartCoroutine(ChangeFOV(inspectFOV, normalFOV));
		}
	}

	IEnumerator ChangeFOV(float originalFOV, float targetFOV)
	{
		Camera camera = GetComponent<Camera>();
		float timeAcc = 0;

		while (timeAcc < zoomInTime)
		{
			timeAcc += Time.deltaTime;
			camera.fieldOfView = Mathf.Lerp(originalFOV, targetFOV, timeAcc / zoomInTime);
			yield return null;
		}
	}
}