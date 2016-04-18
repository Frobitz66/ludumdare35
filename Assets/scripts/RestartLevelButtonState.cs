using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RestartLevelButtonState : ButtonState {

    public override void Click()
    {
		droplet playerDroplet = GameState.GetPlayerDroplet();
		if(playerDroplet == null)
			return;

		SwitchLevel.SwitchToLevel(playerDroplet.gameObject, SceneManager.GetActiveScene().name);
    }
}
