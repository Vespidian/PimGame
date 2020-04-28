using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
	public Text FPS;

	//SCRIPTS
	private StartScreen startBack;
	private Weapons playerTools;


	void Start() {
		startBack = GameObject.Find("EventSystem").GetComponent<StartScreen>();
		//startBack.disableSplash();
		if(SceneManager.GetActiveScene().name != "StartMenu"){
			playerTools = GameObject.Find("Player").GetComponent<Weapons>();
		}
	}

	void Update(){
		if(SceneManager.GetActiveScene().name != "StartMenu"){
			FPS.GetComponent<Text>().text = (1f / Time.unscaledDeltaTime).ToString();
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
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
    public void SetToolGravToggle(){
    	playerTools.tool = 7;
    	SetToolGun();
    }
    public void SetToolBalloon(){
    	playerTools.tool = 8;
    	SetToolGun();
    }
}
