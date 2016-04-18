using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class crystal : MonoBehaviour {
	public int TemperatureChangePerSecond = 0;
	//This assumes that there will only ever be one player.
	private droplet playerDroplet;
	private GameObject playerGameObject;

	// Use this for initialization
	void Start () {
		//JH: This would have been awesome, had it worked.
//		SpriteRenderer renderer = GetComponentInParent<SpriteRenderer> ();
//		if (renderer) {
//			if (TemperatureChangePerSecond == 0.0f) {
//				renderer.color = Color.white;
//			} else {
//				renderer.color = (TemperatureChangePerSecond > 0) ? Color.red : Color.blue;
//			}
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerDroplet != null) {
			float increment = TemperatureChangePerSecond * Time.deltaTime;
			playerDroplet.IncrementTemperature (increment, "Temperature Crystal");
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		//Debug.Log ("TriggerEnter2D: " + col.name);
		if (col.gameObject.tag == "Player"){
			var droplet = col.gameObject.GetComponent<droplet>();
			if (droplet != null) {
				playerGameObject = col.gameObject;
				playerDroplet = droplet;
			}
		} 
	}

	void OnTriggerExit2D(Collider2D col){
		//Debug.Log ("TriggerExit2D: " + col.name);
		if (col.gameObject.tag == "Player"){
			if (col.gameObject == playerGameObject) {
				playerGameObject = null;
				playerDroplet = null;
			}
		} 
	}
}
