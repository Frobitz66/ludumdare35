using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SwitchToBeach : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameState.droplet = coll.gameObject;
        SceneManager.LoadScene("beach");

    }
}
