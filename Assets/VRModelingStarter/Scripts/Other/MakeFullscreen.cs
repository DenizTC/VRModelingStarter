using UnityEngine;
using System.Collections;

public class MakeFullscreen : MonoBehaviour {
	
	void Update () {
        if (Input.GetKeyDown("space")) {
            ToggleFullscreen();
            Debug.Log("fullscreen toggled");
        }

    }

    void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
