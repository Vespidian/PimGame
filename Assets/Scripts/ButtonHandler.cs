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


    void SetToolGun() {
    	playerTools.weapon = 3;
    }
    public void SetToolDelete(){
    	SetToolGun();
    	playerTools.tool = 1;
    }
    public void SetToolWeld(){
    	SetToolGun();
    	playerTools.tool = 2;
    }
    public void SetToolThruster(){
    	SetToolGun();
    	playerTools.tool = 3;
    }
    public void SetToolWheel(){
    	SetToolGun();
    	playerTools.tool = 4;
    }
    public void SetToolDuplicator(){
    	SetToolGun();
    	playerTools.tool = 5;
    }
}
