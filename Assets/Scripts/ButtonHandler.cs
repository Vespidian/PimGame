using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

	public StartScreen startBack;

	void start() {
		startBack = GameObject.Find("EventSystem").GetComponent<StartScreen>();
	}

    public void quitGame() {
    	Application.Quit();
    }
    public void startGame() {
    	SceneManager.LoadScene("SampleScene");
    	Invoke("startBack.cycleSplash", 1);
    	//startBack.cycleSplash();
    }
    public void startMenu() {
    	SceneManager.LoadScene("StartMenu");
    }
    public void helpLink() {
    	Application.OpenURL("https://github.com/Vespidian/Unity-Gmod-Physgun/blob/master/README.md");
    }
}
