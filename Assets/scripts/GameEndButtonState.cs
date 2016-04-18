using UnityEngine;
using System.Collections;

public class GameEndButtonState : ButtonState {

	public Canvas CreditsCanvas;

	void Start(){
		if (CreditsCanvas != null) {
			CreditsCanvas.enabled = false;
		}
	}

	public override void Click ()
	{
		if (CreditsCanvas != null)
			CreditsCanvas.enabled = true;
	}
}
