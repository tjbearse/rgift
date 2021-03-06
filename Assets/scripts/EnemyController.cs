using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class EnemyController : MonoBehaviour {
	public float radius = .5f;
	public Cooldown attackCooldown = new Cooldown(1f);
	public Hitbox hitbox;
	public Camera cam;

	private Transform target;
	private Mover mover;
	private Animator anim;
	private Health health;

	public State state = State.Seeking;
	public enum State {
		Seeking,
		Anim,
		Flee,
	}


	private Attack.Progress attackInProgress;

    void Start() {
		Assert.IsNotNull(hitbox, "hitbox unassigned");
		mover = GetComponent<Mover>();
		Assert.IsNotNull(mover, "didn't find a mover");
		anim = GetComponent<Animator>();
		Assert.IsNotNull(anim, "didn't find the animator");
		health = GetComponent<Health>();
    }

	public void AttackEnd() {
		attackInProgress = null;
	}

	void OnEnable() {
		state = State.Seeking;
		target = GameObject.FindWithTag("Player").transform;
	}

	void OnDisable() {
		mover.Move(Vector2.zero);
	}

	void FixedUpdate() {
		attackInProgress?.Update();
	}

	void Update() {
		Vector2 movement =  target.position - transform.position;
		switch(state) {
			case State.Flee:
				mover.Move(-movement.normalized);
				// TODO move faster and clear when off screen
				if (cam != null) {
					var screenPos = cam.WorldToScreenPoint(transform.position);
					if (!cam.pixelRect.Contains(screenPos)) {
						state = State.Seeking;
						health.Heal();
					}
				}
			break;
			case State.Seeking:
				if (movement.SqrMagnitude() > radius * radius) {
					mover.Move(movement.normalized);
				} else {
					mover.Move(Vector2.zero, movement.normalized);
					if (attackCooldown.cool) {
						attackInProgress = (new Attack(2f)).Trigger(hitbox, (t) => t.target.CompareTag("Enemy"));
						attackCooldown.Trigger();
						anim.SetTrigger("Attack");
						anim.SetBool("VaryAttack", !anim.GetBool("VaryAttack"));
					}
				}
			break;
			case State.Anim:
				mover.Move(Vector2.zero);
			break;
		}
	}
}
