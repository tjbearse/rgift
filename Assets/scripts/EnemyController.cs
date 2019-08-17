using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class EnemyController : MonoBehaviour {
	private Transform target;
	private Mover mover;
	public float radius = .5f;
	public bool flee = false;

    void Start() {
		mover = GetComponent<Mover>();
		Assert.IsNotNull(mover, "didn't find a mover");
    }

	void OnEnable() {
		flee = false;
		target = GameObject.FindWithTag("Player").transform;
	}

	void OnDisable() {
		mover.Move(Vector2.zero);
	}

	void Update() {
		Vector2 movement =  target.position - transform.position;
		if (flee) {
			movement *= -1f;
		}
		if (movement.SqrMagnitude() > radius * radius) {
			mover.Move(movement.normalized);
		} else {
			mover.Move(Vector2.zero);
		}

		// TODO unflee?
	}
}
