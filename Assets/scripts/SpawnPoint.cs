using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public AudioClip LevelAmbientSound;

	// Use this for initialization
	void Start () {
		var playerDroplet = GameState.GetPlayerDroplet();
		if (playerDroplet != null)
		{
			//Debug.Log("Setting droplet start pos: " + GetComponent<Transform>().position);
			playerDroplet.StopAllAudio();
			playerDroplet.PlayAudio(LevelAmbientSound);
			playerDroplet.SpawnAt(this.gameObject);

			var otherPlayers = GameObject.FindObjectsOfType<droplet>();
			Debug.Log(string.Format("Amount of droplets in scene: {0}", otherPlayers.Length));
			for(int i = 0; i < otherPlayers.Length; ++i){
				var otherPlayer = otherPlayers[i];
				if(otherPlayer != playerDroplet){
					Debug.Log(string.Format("Destroying object: {0} because it's not object: {1}", otherPlayer.gameObject.name, playerDroplet.gameObject.name));
					Destroy(otherPlayer.gameObject);
				}
			}
		}
	}
}
