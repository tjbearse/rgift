using System;
using UnityEngine;

[Serializable]
public class Cooldown {
	[SerializeField] private float dur;
	private float next = Mathf.NegativeInfinity;

	public Cooldown(float timeInSeconds) {
		dur = timeInSeconds;
	}

	public void Pause() {
		next = Mathf.Infinity;
	}

	public void Trigger() {
		next = Time.time + dur;
	}

	public void Clear() {
		next = Time.time;
	}

	public bool cool {
		get {
			return Time.time >= next;
		}
	}

	public bool hot {
		get {
			return Time.time < next;
		}
	}
}
