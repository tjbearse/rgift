using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
	public float speed = 1f;

	private Rigidbody2D _rb;
	private Vector2 velocity;

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
	}

	public void Move(Vector2 dir) {
		Debug.Log(dir * speed);
		velocity = dir * speed;
	}

	public void FixedUpdate() {
		_rb.velocity = velocity;
		var facing = Quaternion.LookRotation(Vector3.forward, -velocity);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, facing, 10f);
	}
}
