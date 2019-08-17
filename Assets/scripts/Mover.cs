using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
	public float speed = 1f;

	private Rigidbody2D _rb;
	private Vector2 velocity;
	private Vector2 facing;

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
	}

	public void Move(Vector2 dir) {
		if (dir.SqrMagnitude() < .2f) {
			dir = Vector2.zero;
		}
		velocity = dir * speed;
		facing = dir;
	}

	public void Move(Vector2 dir, Vector2 _facing) {
		if (dir.SqrMagnitude() < .2f) {
			dir = Vector2.zero;
		}
		velocity = dir * speed;
		facing = _facing;
	}

	public void FixedUpdate() {
		_rb.velocity = velocity;
		if (facing.SqrMagnitude() > .5f) {
			Quaternion turnTarg = Quaternion.LookRotation(Vector3.forward, -facing);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, turnTarg, 10f);
		}
	}
}
