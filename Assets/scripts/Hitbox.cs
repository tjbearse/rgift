using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// heavily influenced by
// https://www.gamasutra.com/blogs/NahuelGladstein/20180514/318031/Hitboxes_and_Hurtboxes_in_Unity.php

// marks an area where damage is dealt
public class Hitbox : MonoBehaviour {
	public LayerMask mask;
	public Vector3 boxSize = Vector3.one / 2f;

	public bool active = false;
	
	public IEnumerable<HurtBox> CheckCollision() {
		if (!active) {
			return new HurtBox[0];
		}
		Vector3 box = transform.rotation * boxSize/2f;
		Vector2 a = transform.position - box;
		Vector2 b = transform.position + box;
		// need to rotate?
		Collider2D[] colliders = Physics2D.OverlapAreaAll(a, b, mask);
		return colliders.Select(c => c.GetComponent<HurtBox>()).Where(hb => hb != null);
	}

	private void OnDrawGizmosSelected() {
		if (active) {
			Gizmos.color = new Color(1,0,0,.25f);
		} else {
			Gizmos.color = new Color(.5f,.5f,0,.25f);
		}
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
		Gizmos.DrawCube(Vector3.zero, boxSize);

	}
}
