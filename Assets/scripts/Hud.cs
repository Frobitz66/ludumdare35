using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

	public GameObject life1;
	public GameObject life2;
	public GameObject life3;
	private droplet playerDroplet = null;
	public Image gradientImage;
	public Text deathMessageText;
	public Canvas GameOverHUD;

	// Use this for initialization
	void Start () {
		var player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			playerDroplet = player.GetComponent<droplet> ();
			playerDroplet.OnLivesChanged += this.LivesChanged;
			playerDroplet.OnPlayerKilled += this.PlayerKilled;
			playerDroplet.OnPlayerSpawned += this.PlayerSpawned;
			playerDroplet.OnGameOver += this.GameOver;
		}
		if (deathMessageText != null) {
			deathMessageText.enabled = false;
		}
		if (GameOverHUD != null) {
			GameOverHUD.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerDroplet == null)
			return;

		if (gradientImage == null)
			return;

		float minWarning = playerDroplet.GetWarningTemperatureMin ();
		float maxWarning = playerDroplet.GetWarningTemperatureMax ();

		float currentTemp = playerDroplet.GetTemperature ();

		//Are we in danger of dying?
		if (currentTemp <= minWarning || currentTemp >= maxWarning) {
			//If this is true, then you show the sprite at the lower end of the gauge.
			//If not, then show it at the upper end.  If you want to do that sort of thing.
			bool showAtLowerEnd = (currentTemp <= minWarning);


			switch (playerDroplet.GetDropletState ()) {
			case droplet.DropletState.Gas:
				//Set warning sign for gas here
				break;
			case droplet.DropletState.Ice:
				//Set warning sign for ice here
				break;
			case droplet.DropletState.Water:
				//Set warning sign for water here
				break;
			}
		}

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

	public void PlayerKilled(string killerName){
		if (deathMessageText == null)
			return;
		deathMessageText.text = string.Format("You were killed by {0}", killerName);
		deathMessageText.enabled = true;
	}

	public void PlayerSpawned(){
		if (deathMessageText == null)
			return;
		deathMessageText.enabled = false;
	}

	public void GameOver(){
		if (GameOverHUD == null)
			return;

		GameOverHUD.enabled = true;
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
			playerDroplet.OnPlayerKilled -= this.PlayerKilled;
			playerDroplet.OnPlayerSpawned -= this.PlayerSpawned;
			playerDroplet.OnGameOver -= this.GameOver;
			playerDroplet = null;
		}
	}

}
