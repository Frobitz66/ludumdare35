using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointNClickBase : MonoBehaviour   {
	private Text txt;
	private Canvas canvas;
    String msg;

	void Start () {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        txt = GameObject.Find("Text White").GetComponent<Text>();
        
        if (txt != null)
        {
            txt.text = GameState.loadMsg;
        }


    }



    IEnumerator transitionRoom(float waitTime, String msg, String scene) 
	{

		yield return new WaitForSeconds(waitTime);
		SceneManager.LoadScene(scene);	
	}

	public void waitAndDisplay(String firstMsg, String lastMsg, String nextScene) {
		waitAndDisplay (GameState.TRANSTIME, firstMsg, lastMsg, nextScene);
	}

	public void waitAndDisplay(float waitTime, String firstMsg, String lastMsg, String nextScene) {
		txt.text = firstMsg;
        msg = lastMsg;
        GameState.loadMsg = msg;

		Debug.Log("Setting the transition");

		StartCoroutine (transitionRoom (waitTime, lastMsg, nextScene));
	}

	public void display(String msg){
		txt.text = msg; 

	}

    public void zoom(String gameObjectName)
    {
        show(gameObjectName);
        GameState.zoomImage = gameObjectName;
    }
	public void show (String gameObjectName)
	{
		GameObject go = GameObject.Find(gameObjectName);
		go.GetComponent<SpriteRenderer>().enabled = true;
		go.GetComponent<BoxCollider2D>().enabled = true;
	}

	public void hide (String gameObjectName)
	{
		GameObject go = GameObject.Find(gameObjectName);
		go.GetComponent<SpriteRenderer>().enabled = false;
		go.GetComponent<BoxCollider2D>().enabled = false;
	}

    public void backgroundOnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameState.zoomImage.Length > 0)
            {
                hide(GameState.zoomImage);
                GameState.zoomImage = "";
            }

        }
    }
}


