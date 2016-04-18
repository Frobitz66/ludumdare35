using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameState.droplet != null)
		{
			Debug.Log("Setting droplet start pos: " + GetComponent<Transform>().position);
            GameState.droplet.GetComponent<droplet>().SpawnAt(this.gameObject);
		}
	}
}
