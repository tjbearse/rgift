using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FoodFighter : MonoBehaviour {
	public Cooldown comboCool = new Cooldown(.75f);
	public Hitbox hitbox;
	private Animator anim;

	private Queue<FoodType> meal;
	
    void Start() {
		meal = new Queue<FoodType>();
		anim = GetComponent<Animator>();
    }

	public void Enqueue(FoodType food) {
		Debug.Log(string.Format("got a food, {0}", food));

		// TODO check that food is active
		// TODO eat input at some point if coming in too fast

		// TODO start attacking
		comboCool.Trigger();
		meal.Enqueue(food);
		TriggerAttack();
		if (meal.Count >= 3) {
			Clear();
		}
	}

	void Update() {
		if (meal.Count != 0 && comboCool.cool) {
			Clear();
		}
	}

	void TriggerAttack() {
		anim?.SetTrigger("Attack");
		var hits = hitbox.CheckCollision();
		foreach(var hit in hits) {
			if (!hit.target.CompareTag("Player")) {
				Debug.Log(string.Format("Hit {0}", hit.target));
			}
		}
	}

	void Clear() {
		comboCool.Clear();
		Debug.Log(string.Format("meal done: {0}", meal.ToList()));
		// TODO submit meal
		meal.Clear();
	}
}
