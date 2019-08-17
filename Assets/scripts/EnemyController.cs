using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class EnemyController : MonoBehaviour {
	private Transform target;
	private Mover mover;
	public float radius = .5f;

    void Start() {
		mover = GetComponent<Mover>();
		Assert.IsNotNull(mover, "didn't find a mover");
    }

	void OnEnable() {
		target = GameObject.FindWithTag("Player").transform;
	}

	void Update() {
		Vector2 movement =  target.position - transform.position;
		if (movement.SqrMagnitude() > radius * radius) {
			mover.Move(movement.normalized);
		} else {
			mover.Move(Vector2.zero);
		}
	}
}
