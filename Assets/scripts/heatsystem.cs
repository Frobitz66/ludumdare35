using UnityEngine;
using System.Collections;

public class heatsystem : MonoBehaviour {
 public int temp;
	public int room;
	public float timeSenseUpdate = 0;

	// Use this for initialization
	void Start () {

	}



	// Update is called once per frame
	void Update () {
		timeSenseUpdate += Time.deltaTime;
		if (timeSenseUpdate >= 1) {
			//temp -= 1;
			timeSenseUpdate = 0;
			if (temp < room) {
				temp++;
			}
			if (temp > room) {
				temp--;
			}
		}

		Debug.Log (temp);
	}


}
