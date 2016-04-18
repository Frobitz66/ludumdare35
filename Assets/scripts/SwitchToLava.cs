using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SwitchToLava : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Exiting to lava level");
        GameState.droplet = coll.gameObject;
        AudioClip clip = Resources.Load("sound/ld35_volcano_v1") as AudioClip;
        AudioSource[] audios = GameState.droplet.GetComponents<AudioSource>();

        audios[0].Stop();
        audios[0].clip = clip;
        audios[0].Play();
        SceneManager.LoadScene("lavaLevel");

    }


}
