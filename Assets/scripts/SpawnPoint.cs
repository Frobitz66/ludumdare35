using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public AudioClip LevelAmbientSound;

	// Use this for initialization
	void Start () {
		var playerDroplet = GameState.GetPlayerDroplet();
		if (playerDroplet != null)
		{
			Debug.Log("Setting droplet start pos: " + GetComponent<Transform>().position);
			AudioSource[] audios = playerDroplet.gameObject.GetComponents<AudioSource>();
			audios[0].clip = LevelAmbientSound;
			audios[0].Play();
			playerDroplet.SpawnAt(this.gameObject);
		}
	}
}
