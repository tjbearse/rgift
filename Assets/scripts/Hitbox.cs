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
	
	public IEnumerable<HurtBox> CheckCollision() {
		Collider[] colliders = Physics.OverlapBox(transform.position, boxSize, transform.rotation, mask);

		if (colliders.Length > 0) {
			Debug.Log("We hit something");
		}
		return colliders.Select(c => c.GetComponent<HurtBox>()).Where(hb => hb != null);
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1,0,0,.25f);
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
		Gizmos.DrawCube(Vector3.zero, new Vector3(boxSize.x * 2, boxSize.y * 2, boxSize.z * 2)); // Because size is halfExtents
	}
}
