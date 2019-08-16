using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MealList : ScriptableObject, ISerializationCallbackReceiver {
	private Dictionary<int, string> menu;
	public string Grain;
	public string Fruit;
	public string Vegetable;
	public string Dairy;
	public string Meat;
	public string FatOilSugar;

	[SerializeField] private List<MenuEntry> entries = new List<MenuEntry>();

	public void OnBeforeSerialize() {}

    public void OnAfterDeserialize() {
		// entries to menu
		menu = new Dictionary<int, string>();
		foreach(MenuEntry entry in entries) {
			menu.Add(entry.Entry(), entry.name);
		}
    }

	public string Query(FoodType a, FoodType b, FoodType c) {
		int food = (1 << (int) a) | (1 << (int) b) | (1 << (int) c);
		string meal;
		menu.TryGetValue(food, out meal);
		return meal;
	}
}

[Serializable]
public class MenuEntry {
	public string name;
	public bool Grain;
	public bool Fruit;
	public bool Vegetable;
	public bool Dairy;
	public bool Meat;
	public bool FatOilSugar;

	public int Entry() {
		int food = 0;
		bool[] entries = new bool[]{Grain, Fruit, Vegetable, Dairy, Meat, FatOilSugar};
		for(int i = 0; i < entries.Length; i++) {
			if (entries[i]) {
				food |= 1 << i;
			}
		}
		return food;
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
