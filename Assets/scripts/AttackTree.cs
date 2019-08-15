using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackTree : ScriptableObject {
	public string GrainName;
	public string FruitName;
	public string VegetableName;
	public string DairyName;
	public string MeatName;
	public string FatOilSugarName;

	public AttackTransition trans;

	public struct Traversal {
		private AttackTransition trans;
		public Traversal(AttackTree at) {
			trans = at.trans;
		}
		public AttackNode Next(FoodType food) {
			return trans[food];
		}
	}
}

[Serializable]
public class AttackNode {
	public Attack attack;
	public string name;
	public AttackTransition trans;
}
