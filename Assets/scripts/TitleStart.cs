using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleStart : MonoBehaviour {
    public Canvas creditCanvas;
    public void clickStart()
    {
        SceneManager.LoadScene("Opening 1");
    }

    public void clickCredits()
    {
        creditCanvas.enabled = true;
    }
}
