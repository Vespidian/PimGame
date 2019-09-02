using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyPresses : MonoBehaviour
{

	public bool cursorIsLockable = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKeyDown("r")){
    		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    	}

    	if(cursorIsLockable == true){
	    	if(Input.GetMouseButtonDown(0)){
	    		Cursor.lockState = CursorLockMode.Locked;
	        	Cursor.visible = false;
	    	}
	    	if(Input.GetKeyDown("escape")){
			   Cursor.lockState = CursorLockMode.None;
			   Cursor.visible = true;
		   }
    	}
    }
}
