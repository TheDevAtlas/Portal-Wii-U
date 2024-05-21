using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

	public Animator animator;

	public Trigger[] triggers;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool openDoor = true;

		foreach (Trigger t in triggers) {
			if (!t.isPressed) {
				openDoor = false;
			}
		}

		animator.SetBool ("Open", openDoor);
	}
}
