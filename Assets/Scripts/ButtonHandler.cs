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
		//startBack.disableSplash();
		if(SceneManager.GetActiveScene().name == "SampleScene"){
			playerTools = GameObject.Find("Player").GetComponent<Weapons>();
		}
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
		playerTools = GameObject.Find("Player").GetComponent<Weapons>();
    	playerTools.weapon = 3;
    	playerTools.ToolDescriptor();
    }
    public void SetToolDelete(){
    	playerTools.tool = 1;
    	SetToolGun();
    }
    public void SetToolWeld(){
    	playerTools.tool = 2;
    	SetToolGun();
    }
    public void SetToolThruster(){
    	playerTools.tool = 3;
    	SetToolGun();
    }
    public void SetToolWheel(){
    	playerTools.tool = 4;
    	SetToolGun();
    }
    public void SetToolDuplicator(){
    	playerTools.tool = 5;
    	SetToolGun();
    }
    public void SetToolHinge(){
    	playerTools.tool = 6;
    	SetToolGun();
    }
}
