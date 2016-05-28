using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SwitchLevel : MonoBehaviour {

	public string LevelToSwitchTo = string.Empty;
	public bool SwitchOnClick = false;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (SwitchOnClick)
			return;

		SwitchLevel.SwitchToLevel(coll.gameObject, LevelToSwitchTo);
	}

	public static void SwitchToLevel(GameObject playerObject, string level)
	{
		GameState.SetPlayerDroplet(playerObject);
		GameState.GetPlayerDroplet().StopAllAudio();
		SceneManager.LoadScene(level);
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0) && SwitchOnClick)
		{
			SceneManager.LoadScene(LevelToSwitchTo);    
		}
	}
}
