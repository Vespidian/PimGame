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

    // Start is called before the first frame update
    void Start()
    {
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
    	if(cursorIsLockable == true){
	    	if(Input.GetMouseButtonDown(0)){
	    		if(!EventSystem.current.IsPointerOverGameObject()){
		    		unFocused();
		        	//locked = true;
	        	}
	    	}
	    	if(Input.GetKeyDown("escape")){
	    		//if(locked == true){
				   	focused();
				/* 	locked = false;
			    }
			    if(locked == false){
				   	unFocused();
		        	locked = true;
			    }*/
		    }
    	}
    }
    void unFocused() {
    	Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    	helpButton.SetActive(false);
		crosshair.SetActive(true);
    }
    void focused() {
		Cursor.lockState = CursorLockMode.None;
	  	Cursor.visible = true;
	 	helpButton.SetActive(true);
		crosshair.SetActive(false);
    }
}
