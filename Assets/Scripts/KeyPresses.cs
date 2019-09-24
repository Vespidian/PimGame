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
	private bool locked = true;

	private CamMouseLook cameraVars;

    // Start is called before the first frame update
    void Start()
    {
    	cameraVars = GameObject.Find("Camera").GetComponent<CamMouseLook>();

    	buttons = GameObject.Find("buttons");
    	crosshair = GameObject.Find("crosshair");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        buttons.SetActive(false);
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
    		cameraVars.zoom = true;
    	}else if(Input.GetKeyUp(KeyCode.Z)){
    		Camera.main.fieldOfView = 72;
    		cameraVars.zoom = false;
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
			    }else if(locked == false){
				   	focused();
			    }
		    }
    	}
    }
    public void focused() {
    	cameraVars.mouseMove = true;
		Cursor.lockState = CursorLockMode.Locked;
	  	Cursor.visible = false;
	 	buttons.SetActive(false);
		crosshair.SetActive(true);
		locked = true;
    	//Debug.Log("focused");
    }
    void unFocused() {
    	cameraVars.mouseMove = false;
		Cursor.lockState = CursorLockMode.None;
	  	Cursor.visible = true;
	 	buttons.SetActive(true);
		crosshair.SetActive(false);
		locked = false;
		//Debug.Log("unfocused");
    }
}
