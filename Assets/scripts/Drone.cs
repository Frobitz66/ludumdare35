using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour {

	public enum DroneDirection{
		Left = -1,
		Right = 1
	}

	public DroneDirection Direction = DroneDirection.Right;
	public float Speed = 1.0f;

	private Rigidbody2D ourRigidBody;


	// Use this for initialization
	void Start () { 
		ourRigidBody = GetComponent<Rigidbody2D> ();
		SetSpriteDirection (Direction);
	}
	
	// Update is called once per frame
	void Update () {
		if (ourRigidBody == null)
			return;
		
		ourRigidBody.velocity = new Vector2((int)Direction * Speed, ourRigidBody.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D coll){
		//If we hit the player, kill 'em.
		if (coll.gameObject.tag == "Player") {
			var droplet = coll.gameObject.GetComponent<droplet> ();
			if (droplet != null) {
				droplet.KillTheDroplet ("Drone");
			}
		} else {
			//Else change direction
			Direction = (Direction == DroneDirection.Left) ? DroneDirection.Right : DroneDirection.Left;
			SetSpriteDirection (Direction);
		}
	}

	private void SetSpriteDirection(DroneDirection direction){
		Vector3 theScale = transform.localScale;
		theScale.x = (float)(((int)direction) * Mathf.Abs(theScale.x));
		transform.localScale = theScale;
	}
}
