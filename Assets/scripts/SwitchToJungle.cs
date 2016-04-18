using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SwitchToJungle : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameState.droplet = coll.gameObject;
        SceneManager.LoadScene("jungle");

    }


}
