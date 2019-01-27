using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverridingTest : MonoBehaviour {

	void Start () {
		Humanoid orc = new Orc();
		orc.Yell();
		((Orc)orc).Yell();
	}
}
