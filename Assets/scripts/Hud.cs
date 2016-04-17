﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

	public GameObject life1;
	public GameObject life2;
	public GameObject life3;
	private droplet playerDroplet = null;
	public Image gradientImage;

	// Use this for initialization
	void Start () {
		var player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			playerDroplet = player.GetComponent<droplet> ();
			playerDroplet.OnLivesChanged += this.LivesChanged;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerDroplet == null)
			return;

		if (gradientImage == null)
			return;

		gradientImage.fillAmount = GetPlayerHealthRatio ();
	}

	private float GetPlayerHealthRatio(){
		if (playerDroplet == null)
			return 0.0f;
		float minAbs = Mathf.Abs (playerDroplet.GetMinTemperature ());
		float entireSpan = minAbs + Mathf.Abs (playerDroplet.GetMaxTemperature ());
		//...right?
		return (minAbs + playerDroplet.GetTemperature ()) / entireSpan;
	}


	public void LivesChanged(int livesLeft){
		if (livesLeft < 3) {
			life3.SetActive (false);
		}
		if (livesLeft < 2) {
			life2.SetActive (false);
		}
		if (livesLeft < 1) {
			life1.SetActive (false);
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
			playerDroplet.OnLivesChanged -= this.LivesChanged;
			playerDroplet = null;
		}
	}

}
