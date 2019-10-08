using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

	//SCRIPTS
	private StartScreen startBack;
	private Weapons playerTools;

	void Start() {
		startBack = GameObject.Find("EventSystem").GetComponent<StartScreen>();
		playerTools = GameObject.Find("Player").GetComponent<Weapons>();
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

    public void SetToolDelete(){
    	playerTools.weapon = 3;
    	playerTools.tool = 1;
    }
    public void SetToolWeld(){
    	playerTools.weapon = 3;
    	playerTools.tool = 2;
    }
    public void SetToolThruster(){
    	playerTools.weapon = 3;
    	playerTools.tool = 3;
    }
    public void SetToolWheel(){
    	playerTools.weapon = 3;
    	playerTools.tool = 4;
    }
}
