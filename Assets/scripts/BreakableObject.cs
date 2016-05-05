using UnityEngine;
using System.Collections;

public class BreakableObject : MonoBehaviour {
	// testing git branching
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			var droplet = coll.gameObject.GetComponent<droplet>();
			if (droplet != null) {
				if (droplet.GetDropletState () != droplet.DropletState.Ice)
					return;
				//Disable the collider
				var ourCollider = GetComponentInParent<Collider2D> ();
				ourCollider.enabled = false;
				//Play the break animation
				if (animator != null) {
					animator.SetBool ("isBroken", true);
				}

				Destroy (gameObject, 2.0f);
			}
		}
	}
}
