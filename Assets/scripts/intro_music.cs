using UnityEngine;
using System.Collections;

public class intro_music : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameState.introMusic = this.gameObject;
        DontDestroyOnLoad(this.gameObject);
    }
	

}
