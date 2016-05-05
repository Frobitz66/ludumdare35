using UnityEngine;
using System.Collections;

public class molpe_easteregg : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			GetComponent<SpriteRenderer> ().enabled = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
