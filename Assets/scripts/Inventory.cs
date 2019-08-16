using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	private bool[] values = new bool[Enum.GetNames(typeof(FoodType)).Length];
	public FoodType[] starting = new FoodType[0];

	void Start() {
		foreach(var food in starting) {
			this[food] = true;
		}
	}

	public bool this[FoodType index] {  
		get {
			return values[(int) index];
		}
		private set {
			values[(int) index] = value;
		}
	}

	public void Recieve(FoodType food) {
		this[food] = true;
	}
}
