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
	public Cooldown attackCooldown = new Cooldown(1f);
	public Hitbox hitbox;
	private Animator anim;

	private Attack.Progress attackInProgress;

    void Start() {
		Assert.IsNotNull(hitbox, "hitbox unassigned");
		mover = GetComponent<Mover>();
		Assert.IsNotNull(mover, "didn't find a mover");
		anim = GetComponent<Animator>();
		Assert.IsNotNull(anim, "didn't find the animator");
    }

	public void AttackEnd() {
		attackInProgress = null;
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
			if (!flee && attackCooldown.cool) {
				attackInProgress = (new Attack(2f)).Trigger(hitbox, (t) => t.target.CompareTag("Enemy"));
				attackCooldown.Trigger();
				anim.SetTrigger("Attack");
				anim.SetBool("VaryAttack", !anim.GetBool("VaryAttack"));
			}
		}
		attackInProgress?.Update();

		// TODO unflee?
	}
}
