using UnityEngine;
using System.Collections;

public class Liquid : MonoBehaviour {

	private BuoyancyEffector2D ourBuoyancyEffector;
	private droplet playerDroplet = null;

	// Use this for initialization
	void Start () {
		ourBuoyancyEffector = GetComponent<BuoyancyEffector2D> ();
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
		ourBuoyancyEffector.enabled = (newState == droplet.DropletState.Ice);
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
