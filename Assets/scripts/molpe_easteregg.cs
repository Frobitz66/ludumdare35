using UnityEngine;
using System.Collections;

public class molpe_easteregg : MonoBehaviour {

	public GameObject MolpeDialog = null;

	// Use this for initialization
	void Start () {
		if (MolpeDialog != null) {
			MolpeDialog.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			GetComponent<SpriteRenderer> ().enabled = true;
			if (MolpeDialog != null) {
				MolpeDialog.GetComponent<SpriteRenderer> ().enabled = true;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
