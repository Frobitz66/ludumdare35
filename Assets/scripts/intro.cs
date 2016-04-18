using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class intro : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			SceneManager.LoadScene("opening 2");    

		}
	}


} 
