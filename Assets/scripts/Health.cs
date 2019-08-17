using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public float maxHealth = 10f;
	public float health;
	private Animator anim;

    void Start() {
		health = maxHealth;
		anim = GetComponent<Animator>();
    }

	public void TakeDamage(float damage) {
		Debug.Log("damaged", this.gameObject);
		health -= damage;
		if (health <= 0) {
			health = 0;
			anim?.SetTrigger("Died");
		} else {
			anim?.SetTrigger("Hurt");
		}
	}
}
