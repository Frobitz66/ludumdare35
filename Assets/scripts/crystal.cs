using UnityEngine;
using System.Collections;

public class crystal : MonoBehaviour {
	public int TemperatureChange = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("TriggerEnter2D: " + col.name);
		if (col.gameObject.tag == "Player"){
			var droplet = (droplet)col.gameObject.GetComponent<droplet>();
			//var droplet = (dropplet)col.gameObject.Find ("Dropplet");

			droplet.IncrementTemperature(TemperatureChange);
		} 
	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("CollisionEnter2D: " + col.collider.name);
	}
}
