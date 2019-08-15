using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour {
	public InputActionAsset inputAction;

	private event Action enableMap;
	private event Action disableMap;
	private const string baseMapName = "base";
	private const string moveName = "move";
	private Vector2 movement = Vector2.zero;
	private FoodFighter foodFighter;

    void Start() {
		foodFighter = GetComponent<FoodFighter>();
		Assert.IsNotNull(foodFighter);

		var baseMap = inputAction.GetActionMap(baseMapName);
		Assert.IsNotNull(baseMap);
		enableMap += () => { baseMap.Enable(); };
		disableMap += () => { baseMap.Disable(); };

		MapFoods(baseMap);

		InputAction move = baseMap.GetAction(moveName);
		enableMap += () => { move.performed += RecieveMovement; };
		disableMap += () => { move.performed -= RecieveMovement; };

		if (enableMap != null) {
			// wasn't setup on our first enable
			enableMap();
		}
    }

	void OnEnable() {
		if (enableMap != null) {
			enableMap();
		}
	}

	void OnDisable() {
		if (disableMap != null) {
			disableMap();
		}
	}

	void Update() {
		// TODO mover.Move(movement);
	}

	private void RecieveMovement(InputAction.CallbackContext cc) {
		movement = cc.ReadValue<Vector2>();
	}

	private void MapFoods(InputActionMap baseMap) {
		foreach (FoodType food in Enum.GetValues(typeof(FoodType))) {
			string name = Enum.GetName(typeof(FoodType), food);
			InputAction action = baseMap.GetAction(name);
			Action<InputAction.CallbackContext> callback = (cc) => {
				foodFighter.Enqueue(food);
			};
			enableMap += () => {
				action.performed += callback;
			};
			disableMap += () => {
				action.performed -= callback;
			};
		}
	}
}
