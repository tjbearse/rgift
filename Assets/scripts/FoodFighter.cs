using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FoodFighter : MonoBehaviour {
	public Cooldown comboCool = new Cooldown(.75f);
	public Hitbox hitbox;
	public AttackTree attackTree;
	private Animator anim;

	private Attack.Progress attackInProgress;

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

		// match to attack and start it
		// need a traversal thing to walk the foodtree
		// reset it when combo expires

		if (meal.Count >= 3) {
			Clear();
		}
	}

	void Update() {
		if (meal.Count != 0 && comboCool.cool) {
			Clear();
		}
		if (attackInProgress != null) {
			if (!attackInProgress.Update()) {
				attackInProgress = null;
			}
		}
	}

	void Clear() {
		comboCool.Clear(); // TODO incorporate move timing too?
		Debug.Log(string.Format("meal done: {0}", meal.ToList()));
		// TODO submit meal
		meal.Clear();
	}
}
