using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public void quitGame() {
    	Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    	Debug.Log("exited");
    	Application.Quit();
    }
    void OnMouseDown() {
    	Debug.Log("exited");
    	Application.Quit();
    }
}
