using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MealList : ScriptableObject {
	public Menu menu;

	public string Query(FoodType a, FoodType b, FoodType c) {
		return menu[a][b][c];
	}
}

// quick hacking just for unity inspector :)
[Serializable]
public class Menu : FoodMap<MenuA> {}
[Serializable]
public class MenuA : FoodMap<MenuB> {}
[Serializable]
public class MenuB : FoodMap<string> {}

[Serializable]
public class FoodMap<T> {
	public T Grain;
	public T Fruit;
	public T Vegetable;
	public T Dairy;
	public T Meat;
	public T FatOilSugar;

	public T this[FoodType index] {  
		get {
			return new T[]{Grain, Fruit, Vegetable, Dairy, Meat, FatOilSugar}[(int) index];
		}
	}  
}
