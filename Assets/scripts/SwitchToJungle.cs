using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SwitchToJungle : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameState.droplet = coll.gameObject;
        AudioClip clip = Resources.Load("sound/ld35_jungle_v2") as AudioClip;
        GameState.droplet.GetComponent<AudioSource>().Stop();
        GameState.droplet.GetComponent<AudioSource>().clip = clip;
        GameState.droplet.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("jungle");

    }


}
