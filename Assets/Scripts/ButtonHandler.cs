using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

	private StartScreen startBack;

	void Start() {
		startBack = GameObject.Find("EventSystem").GetComponent<StartScreen>();
		//startBack.disableSplash();
	}

    public void quitGame() {
    	Application.Quit();
    }
    public void startGame() {
    	startBack.enableSplash();
    	Invoke("loadInitialScene", 1);
    }
    void loadInitialScene() {
    	SceneManager.LoadScene("SampleScene");
    }

    public void startMenu() {
    	SceneManager.LoadScene("StartMenu");
    }
    public void helpLink() {
    	Application.OpenURL("https://github.com/Vespidian/Unity-Gmod-Physgun/blob/master/README.md");
    }
}
