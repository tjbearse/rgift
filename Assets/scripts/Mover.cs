using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
	public float speed = 1f;

	private Rigidbody2D _rb;
	private Vector2 velocity;
	private Vector2 facing;
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
		_rb = GetComponent<Rigidbody2D>();
	}

	public void Move(Vector2 dir) {
		if (dir.SqrMagnitude() < .2f) {
			dir = Vector2.zero;
		}
		velocity = dir * speed;
		facing = dir;
	}

	private void OnAnimatorMove() {
		// do root motion by hand to avoid weird y axis behavior
		if (anim != null) {
			// note that this addition is ok because we clobber velocity every frame in fixed update.
			// if/when we don't do that we probably need to rethink how combining these values works.
			_rb.velocity += (Vector2) anim.deltaPosition / Time.deltaTime;
			_rb.angularVelocity = anim.deltaRotation.eulerAngles.z / Time.deltaTime;
		}
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
