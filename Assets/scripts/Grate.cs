using UnityEngine;
using System.Collections;

public class Grate : MonoBehaviour {

	private droplet playerDroplet;
	// Use this for initialization
	void Start () {
		var player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			playerDroplet = player.GetComponent<droplet> ();
			playerDroplet.OnStateChanged += this.OnPlayerStateChanged;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void OnPlayerStateChanged(droplet.DropletState newState){
		var ourCollider = GetComponentInParent<Collider2D> ();
        if (ourCollider != null)
        {
            ourCollider.enabled = (newState == droplet.DropletState.Ice);
        }

	}

	void OnDisable(){
		Cleanup ();
	}

	void OnDestroy(){
		Cleanup ();
	}

	private void Cleanup(){
		if (playerDroplet != null) {
			playerDroplet.OnStateChanged -= this.OnPlayerStateChanged;
			playerDroplet = null;
		}
	}
}
