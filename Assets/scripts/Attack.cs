using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Attack {
	public float damage;

	public Attack(float damage) {
		this.damage = damage;
	}

	public Progress Trigger(Hitbox hitbox, Func<HurtBox, bool> exclude) {
		return new Progress(hitbox, exclude);
	}

	public class Progress {
		private Hitbox _hitbox;
		private Func<HurtBox, bool> _exclude;
		private HashSet<HurtBox> _hits;
		public Progress(Hitbox hitbox, Func<HurtBox, bool> exclude) {
			_hitbox = hitbox;
			_exclude = exclude;
			_hits = new HashSet<HurtBox>();
		}

		public void Update() {
			var hits = _hitbox.CheckCollision();
			foreach(var hit in hits) {
				if (!_hits.Contains(hit) && !_exclude(hit)) {
					_hits.Add(hit);
					hit.target.TakeDamage(2f); // TODO based on attack
					Debug.Log(string.Format("Hit {0}, {1}", hit.target, hit.target.health));
				}
			}
		}
	}
}

