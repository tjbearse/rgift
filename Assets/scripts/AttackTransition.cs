using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackTransition : ScriptableObject {
	public AttackNode Grain;
	public AttackNode Fruit;
	public AttackNode Vegetable;
	public AttackNode Dairy;
	public AttackNode Meat;
	public AttackNode FatOilSugar;

	public AttackNode this[FoodType index] {  
		get {
			return new AttackNode[]{Grain, Fruit, Vegetable, Dairy, Meat, FatOilSugar}[(int) index];
		}
	}  
}
