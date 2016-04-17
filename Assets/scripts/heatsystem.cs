using UnityEngine;
using System.Collections;

public class heatsystem : MonoBehaviour {

	public float RoomTemperature = 25.0f;
	public float AbsoluteTemperatureChangePerSecond = 10.0f;
	private droplet playerDroplet;

	// Use this for initialization
	void Start () {
		var player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			playerDroplet = player.GetComponent<droplet> ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (playerDroplet != null) {
			float playerTemperature = playerDroplet.GetTemperature ();
			if (playerTemperature == RoomTemperature)
				return;

			//Do we need to go up or down?
			float difference = RoomTemperature - playerTemperature;
			float direction = difference / Mathf.Abs (difference);

			//Either increment by the delta percentage, or by the last tiny increment
			float proposedIncrement = ((AbsoluteTemperatureChangePerSecond * direction) * Time.deltaTime);
			float increment = Mathf.Min (Mathf.Abs(difference), Mathf.Abs(proposedIncrement)) * direction;
			playerDroplet.IncrementTemperature (increment, "Ambient Heat System");
		}
	}


}
