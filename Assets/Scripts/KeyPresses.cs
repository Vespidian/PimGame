using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class KeyPresses : MonoBehaviour
{

	public bool cursorIsLockable = true;
	private GameObject buttons;
	private GameObject crosshair;
	private GameObject spawnSelection;
	private GameObject toolSelection;
	private bool locked = true;

	//SCRIPTS
	private CamMouseLook cameraVars;
	private Character_Controller thePlayer;

    // Start is called before the first frame update
    void Start()
    {
    	cameraVars = GameObject.Find("Camera").GetComponent<CamMouseLook>();
		thePlayer = GameObject.Find("Player").GetComponent<Character_Controller>();


    	buttons = GameObject.Find("buttons");
    	crosshair = GameObject.Find("crosshair");
    	spawnSelection = GameObject.Find("ToolSelect");

    	DefaultUIState();
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
		    }else if(Input.GetKeyUp(KeyCode.Q)){
		    	spawnSelection.SetActive(false);
		    	FocusMouse();
		    }
    	}
    }

    void PauseGame(){
    	buttons.SetActive(true);
		crosshair.SetActive(false);
		thePlayer.pauseGame = true;
		Time.timeScale = 0;
		UnFocusMouse();
    }
    void UnPauseGame(){
    	buttons.SetActive(false);
		crosshair.SetActive(true);
		thePlayer.pauseGame = false;
		Time.timeScale = 1;
		FocusMouse();
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
