using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SwitchToBeach : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameState.droplet = coll.gameObject;
        AudioClip clip = Resources.Load("sound/ld35_end_v1") as AudioClip;
        GameState.droplet.GetComponent<AudioSource>().Stop();
        GameState.droplet.GetComponent<AudioSource>().clip = clip;
        GameState.droplet.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("beach");

    }
}
