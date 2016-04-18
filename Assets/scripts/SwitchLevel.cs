using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SwitchLevel : MonoBehaviour {

	public string LevelToSwitchTo = string.Empty;

	void OnTriggerEnter2D(Collider2D coll)
	{
		GameState.droplet = coll.gameObject;
		SceneManager.LoadScene(LevelToSwitchTo);
	}
}
