﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor: MonoBehaviour {

	public Animator Slide;
	public bool open;
	public Transform Player;

	void Start (){
		open = false;
	}

	void OnMouseOver (){
		{
			if (Player) {
				float dist = Vector3.Distance (Player.position, transform.position);
				if (dist < 15) {
					if (open == false) {
						if (Input.GetMouseButtonDown (0)) {
							StartCoroutine (opening ());
						}
					} else {
						if (open == true) {
							if (Input.GetMouseButtonDown (0)) {
								StartCoroutine (closing ());
							}
						}

					}

				}
			}

		}

	}

	IEnumerator opening(){
		print ("you are opening the door");
		Slide.Play ("SlideOpen");
		open = true;
		yield return new WaitForSeconds (.5f);
	}

	IEnumerator closing(){
		print ("you are closing the door");
		Slide.Play ("SlideClose");
		open = false;
		yield return new WaitForSeconds (.5f);
	}


}

