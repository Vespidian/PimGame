using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class KeyPresses : MonoBehaviour
{

	public bool cursorIsLockable = true;
	private GameObject helpButton;
	private GameObject crosshair;
	private bool locked = true;

	private CamMouseLook cameraVars;

    // Start is called before the first frame update
    void Start()
    {
    	cameraVars = GameObject.Find("Camera").GetComponent<CamMouseLook>();

    	helpButton = GameObject.Find("helpButton");
    	crosshair = GameObject.Find("crosshair");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        helpButton.SetActive(false);
        crosshair.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKeyDown("r")){
    		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    	}
    	if(Input.GetKey(KeyCode.Z)){//Zoom in by pressing z
    		Camera.main.fieldOfView = 25;
    	}else if(Input.GetKeyUp(KeyCode.Z)){
    		Camera.main.fieldOfView = 72;
    	}
    	if(cursorIsLockable == true){
	    	if(Input.GetMouseButtonDown(0) && (cameraVars.mouseMove == false)){
	    		if(!EventSystem.current.IsPointerOverGameObject()){
		    		focused();
		        	locked = true;
	        	}
	    	}
	    	if(Input.GetKeyDown("escape")){
	    		if(locked == true){
				   	unFocused();
				 	locked = false;
			    }else if(locked == false){
				   	focused();
		        	locked = true;
			    }
		    }
    	}
    }
    void focused() {
    	cameraVars.mouseMove = true;
		Cursor.lockState = CursorLockMode.Locked;
	  	Cursor.visible = false;
	 	helpButton.SetActive(false);
		crosshair.SetActive(true);
    	Debug.Log("focused");
    }
    void unFocused() {
    	cameraVars.mouseMove = false;
		Cursor.lockState = CursorLockMode.None;
	  	Cursor.visible = true;
	 	helpButton.SetActive(true);
		crosshair.SetActive(false);
		Debug.Log("unfocused");
    }
}
