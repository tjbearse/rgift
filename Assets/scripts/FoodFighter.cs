using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

public class FoodFighter : MonoBehaviour {
	public Cooldown comboCool = new Cooldown(.75f);
	public Hitbox hitbox;
	private Animator anim;
	private Inventory inventory;

	public MealList meals;

	private Attack.Progress attackInProgress;

	private Queue<FoodType> meal;
	
    void Start() {
		meal = new Queue<FoodType>();
		anim = GetComponentInChildren<Animator>();
		inventory = GetComponent<Inventory>();
		Assert.IsNotNull(hitbox, "no hitbox!");
    }

	public void Enqueue(FoodType food) {
		if (!inventory[food]) {
			Debug.Log(string.Format("discarding, {0}", food));
			return;
		} else {
			Debug.Log(string.Format("got a food, {0}", food));
		}
			

		// TODO check that food is active
		// TODO eat input at some point if coming in too fast

		// TODO start attacking
		comboCool.Trigger();
		meal.Enqueue(food);

		anim?.SetTrigger(Enum.GetName(typeof(FoodType), food));
		// TODO attack gets made somewhere else
		attackInProgress = (new Attack(2f)).Trigger(hitbox, (t) => t.target.CompareTag("Player"));

		if (meal.Count >= 3) {
			if (meals != null) {
				var a = meal.Dequeue();
				var b = meal.Dequeue();
				var c = meal.Dequeue();
				string mealTitle = meals.Query(a,b,c);
				if (mealTitle != "") {
					Debug.Log(string.Format("made {0}", mealTitle));
					var pos = this.transform.position + Vector3.up;
					MealToaster.SToast(pos, mealTitle);
				}
			}
			Clear();
		}
	}

	public void AttackEnd() {
		attackInProgress = null;
	}

	void Update() {
		if (meal.Count != 0 && comboCool.cool) {
			Clear();
		}

		attackInProgress?.Update();
	}

	public void Clear() {
		comboCool.Clear(); // TODO incorporate move timing too?
		Debug.Log(string.Format("meal done: {0}", meal.ToList()));
		// TODO submit meal
		meal.Clear();
	}
}
