using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SwitchToBeach : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameState.droplet = coll.gameObject;
        AudioClip clip = Resources.Load("sound/ld35_end_v1") as AudioClip;
        AudioSource[] audios = GameState.droplet.GetComponents<AudioSource>();

        audios[0].Stop();
        audios[0].clip = clip;
        audios[0].Play();
        SceneManager.LoadScene("beach");

    }
}
