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
		
		GameState.droplet = coll.gameObject;
		SceneManager.LoadScene(LevelToSwitchTo);
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0) && SwitchOnClick)
		{
			SceneManager.LoadScene(LevelToSwitchTo);    

		}
	}
}
