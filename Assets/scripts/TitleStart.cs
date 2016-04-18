using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleStart : MonoBehaviour {

    public void clickStart()
    {
        SceneManager.LoadScene("Opening 1");
    }
}
