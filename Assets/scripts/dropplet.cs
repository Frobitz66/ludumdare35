using UnityEngine;
using System.Collections;


public class dropplet : MonoBehaviour {
	public int temp;
	public int heatstate;
	// Use this for initialization
	void Start () {
		//temp = 100;

	}
	
	// Update is called once per frame
	void Update () {
		//temp = temp - 1;
		//Debug.Log (temp);
		//if (temp == 0) {
		//	Debug.Log ("frozen");

		//}
		Debug.Log(heatstate);

		}
	void OnTriggerStay ( Collider col ){
		Debug.Log (col.name);
		if (col.gameObject.tag == "red_crystal") {
			heatstate = 1;
		} 
		//if (col.gameObject.tag == "blue_crystal")

	}
}
