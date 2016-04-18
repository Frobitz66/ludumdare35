using UnityEngine;
using System.Collections;

public class LoadLevelButtonState : ButtonState{

	public string LevelToLoad;
	public override void Click (){
		var player = GameObject.FindGameObjectWithTag ("Player");
		DestroyObject (player);
		UnityEngine.SceneManagement.SceneManager.LoadScene (LevelToLoad);
	}
}
