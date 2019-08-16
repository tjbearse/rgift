using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextToast : MonoBehaviour {
	[SerializeField] private Camera cam;
	void start() {
		if (cam == null) {
			cam = Camera.main;
		}
	}

	public void Toast(Transform template, Vector3 worldPos, string text) {
		Transform newguy = Instantiate(template, this.transform);
		Text t = newguy.GetComponentInChildren<Text>();
		if (t != null) {
			t.text = text;
		}
		newguy.transform.position = cam.WorldToScreenPoint(worldPos);
	}
}
