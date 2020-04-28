using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class KeyPresses : MonoBehaviour
{

	public bool cursorIsLockable = true;
	private GameObject buttons;
	private GameObject crosshair;
	private GameObject spawnSelection;
	private GameObject toolSelection;
	private Transform customKeys;
	private bool locked = true;


	//CUSTOM KEYS
	/*private KeyCode customKey;
	private bool waitingForKey = false;
	private Text buttonText;
	private Event keyEvent;*/

	//SCRIPTS
	private CamMouseLook cameraVars;
	private CharController thePlayer;
	private PlayerSounds sounds;
	private UIFade slomoFade;

    // Start is called before the first frame update
    void Start()
    {
    	cameraVars = GameObject.Find("Camera").GetComponent<CamMouseLook>();
		thePlayer = GameObject.Find("Player").GetComponent<CharController>();
		sounds = GameObject.Find("Camera").GetComponent<PlayerSounds>();
    	slomoFade = GameObject.Find("SlomoEffect").GetComponent<UIFade>();

    	buttons = GameObject.Find("buttons");
    	crosshair = GameObject.Find("crosshair");
    	spawnSelection = GameObject.Find("Q-Menu");

    	//customKeys = GameObject.Find("CustomKeys").transform;

    	DefaultUIState();

    	/*for(int i = 0; i < customKeys.childCount; i++){
    		if(customKeys.GetChild(i).name == "forwardKeyButton"){
    			customKeys.GetChild(i).GetComponentInChildren<Text>().text = "UpArrow";
    		}else if(customKeys.GetChild(i).name == "backwardKeyButton"){
    			customKeys.GetChild(i).GetComponentInChildren<Text>().text = "DownArrow";
    		}
    	}*/
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKeyDown("t")){
    		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    	}
    	if(Input.GetKey(KeyCode.Z)){//Zoom in by pressing z
    		Camera.main.fieldOfView = 25;
    		cameraVars.zoom = true;
    	}else if(Input.GetKeyUp(KeyCode.Z)){
    		Camera.main.fieldOfView = 72;
    		cameraVars.zoom = false;
    	}
    	if(cursorIsLockable == true){
	    	if(Input.GetMouseButtonDown(0) && (cameraVars.mouseMove == false)){
	    		if(!EventSystem.current.IsPointerOverGameObject()){
	    			UnPauseGame();
	        	}
	    	}
	    	if(Input.GetKeyDown("escape")){
	    		if(locked == true){
				   	PauseGame();
			    }else if(locked == false){
				   	UnPauseGame();
			    }
		    }

		    if(Input.GetKey(KeyCode.Q)){
		    	spawnSelection.SetActive(true);
		    	UnFocusMouse();
		    }
		    if(Input.GetKeyUp(KeyCode.Q)){
		    	spawnSelection.SetActive(false);
		    	FocusMouse();
		    }
		    if(Input.GetKeyDown(KeyCode.F1)){
		    	if(spawnSelection.activeSelf == true){
		    		spawnSelection.SetActive(false);
		    		FocusMouse();
		    	}else if(spawnSelection.activeSelf == false){
		    		spawnSelection.SetActive(true);
		    		UnFocusMouse();
		    	}
		    }
		    if(Input.GetKeyDown(KeyCode.X)){
		    	if(thePlayer.allowSlomo == true){
			    	if(Time.timeScale == 1){
			    		SlomoTime();
			    	}else if(Time.timeScale == 0.5){
			    		NormalTime();
			    	}
			    	slomoFade.Fade();
			    }
			}
    	}

    	/*keyEvent = Event.current;
    	if(keyEvent.isKey && waitingForKey == true){
    		customKey = keyEvent.keyCode;
    		waitingForKey = false;
    	}*/
    }

    /*void KeyAssignment(string keyName) {
    	if(waitingForKey == false){

    	}
    }*/

    void PauseGame(){
    	buttons.SetActive(true);
		crosshair.SetActive(false);
		thePlayer.pauseGame = true;
		FreezeTime();
		UnFocusMouse();
    }
    void UnPauseGame(){
    	buttons.SetActive(false);
		crosshair.SetActive(true);
		thePlayer.pauseGame = false;
		NormalTime();
		FocusMouse();
    }
    void NormalTime(){
		Time.timeScale = 1;
		sounds.AudioChoice("speedtime");
    }
    void FreezeTime(){
		Time.timeScale = 0;
    }
    void SlomoTime(){
    	Time.timeScale = 0.5f;
		sounds.AudioChoice("slowtime");
    }

    void UnFocusMouse(){
    	cameraVars.mouseMove = false;
		Cursor.lockState = CursorLockMode.None;
	  	Cursor.visible = true;
	  	thePlayer.allowTools = false;
		locked = false;
    }
    void FocusMouse(){
    	cameraVars.mouseMove = true;
		Cursor.lockState = CursorLockMode.Locked;
	  	Cursor.visible = false;
	  	thePlayer.allowTools = true;
		locked = true;
    }

    void DefaultUIState(){
    	Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        buttons.SetActive(false);
        crosshair.SetActive(true);
        spawnSelection.SetActive(false);
    }
}
