using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SwitchToLava : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Exiting to lava level");
        GameState.droplet = coll.gameObject;
        SceneManager.LoadScene("lavaLevel");

    }


}
