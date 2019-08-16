using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	[SerializeField] private FoodType values = 0;

	public bool this[FoodType food] {
		get {
			return (values & food) != 0;
		}
		private set {
			if (value) {
				values |= food;
			} else {
				values &= ~(food);
			}
		}
	}

	public void Recieve(FoodType food) {
		this[food] = true;
	}
}
