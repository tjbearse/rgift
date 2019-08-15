using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public float maxHealth = 10f;
	public float health;

    void Start() {
		health = maxHealth;
    }

	public void TakeDamage(float damage) {
		health -= damage;
		if (health <= 0) {
			health = 0;
			Debug.Log("died");
		}
	}
}
