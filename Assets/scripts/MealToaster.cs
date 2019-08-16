using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextToast))]
public class MealToaster : MonoBehaviour {
	private TextToast _ttoast;
	public Transform template;

	private static MealToaster _instance;

    void Start() {
		_instance = this;
		_ttoast = GetComponent<TextToast>();
    }

	public static void SToast(Vector3 worldPos, string meal) {
		_instance?.Toast(worldPos, meal);
	}

	public void Toast(Vector3 worldPos, string meal) {
		_ttoast.Toast(template, worldPos, meal);
	}
}
