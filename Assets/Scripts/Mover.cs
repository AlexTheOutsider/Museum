using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public int times;
	// Use this for initialization
	void Start ()
	{
		transform.DOMoveX(10, 5).SetEase(Ease.Linear)/*.onComplete(WhenDone())*/;
	}

	void WhenDone()
	{
		times++;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
