using UnityEngine;
using System.Collections;

public class LoadLevelButtonState : ButtonState{

	public string LevelToLoad;
	public bool DestroyPlayer = false;
	public override void Click (){
		if(DestroyPlayer){
			var player = GameState.GetPlayerDroplet();
			if(player != null)
				DestroyObject (player.gameObject);
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene (LevelToLoad);
	}
}
