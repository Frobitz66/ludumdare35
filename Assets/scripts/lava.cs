using UnityEngine;
using System.Collections;

public class lava : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        if (GameState.droplet != null)
        {
            Debug.Log("Setting droplet start pos: " + GetComponent<Transform>().position);
            GameState.droplet.transform.position = GetComponent<Transform>().position;
            Camera.main.GetComponent<SmoothCamera>().target = GameState.droplet;
        }


    }
}
