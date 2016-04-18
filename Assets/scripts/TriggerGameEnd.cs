using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TriggerGameEnd : MonoBehaviour {

	public Canvas GameEndCanvas;

	private droplet playerDroplet = null;
	// Use this for initialization
	void Start () {
		if (GameEndCanvas != null)
			GameEndCanvas.enabled = false;
		
		playerDroplet = (GameState.droplet != null) ? GameState.droplet.GetComponent<droplet> () : null;
		if (playerDroplet == null) {
			var player = GameObject.FindGameObjectWithTag ("Player");
			playerDroplet = player.GetComponent<droplet> ();
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag != "Player")
			return;

		playerDroplet.SetCanMove (false);
		if(GameEndCanvas != null)
			GameEndCanvas.enabled = true;
	}
}
