using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu]
public class MealList : ScriptableObject, ISerializationCallbackReceiver {
	private Dictionary<FoodType, string> menu;
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
		menu = new Dictionary<FoodType, string>();
		foreach(MenuEntry entry in entries) {
			menu.Add(entry.food, entry.name);
		}
    }

	public string Query(FoodType a, FoodType b, FoodType c) {
		FoodType food = a | b | c;
		string meal;
		menu.TryGetValue(food, out meal);
		return meal;
	}
}

[Serializable]
public class MenuEntry {
	public string name;
	public FoodType food;
}
