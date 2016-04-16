using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {

	public GameObject life1;
	public GameObject life2;
	public GameObject life3;

	public int lives = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (lives < 3) {
			life3.SetActive (false);
		}
		if (lives < 2) {
			life2.SetActive (false);
		}
		if (lives < 1) {
			life1.SetActive (false);
		}
	}

	public void LoseLife() {
		lives--;
	}
		
}
