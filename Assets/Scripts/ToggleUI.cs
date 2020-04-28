using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
	[Header("Tool Buttons")]
	public GameObject deleteTool;
	public GameObject weldTool;
	public GameObject thrusterTool;
	public GameObject wheelTool;
	public GameObject duplicatorTool;
	public GameObject hingeTool;
	public GameObject gravityTool;
	public GameObject balloonTool;

	[Header("Misc")]
	public GameObject weaponDescriptor;

	[Header("Sections")]
	public GameObject AlwaysShown;
	public GameObject baubles;
	public GameObject qMenu;
	public GameObject buttons;

	public bool AlwaysShownbool;
	public bool baublesbool;
	public bool qMenubool;
	public bool buttonsbool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void changeStates(){
        if(AlwaysShownbool == true){
        	AlwaysShown.GetComponent<CanvasGroup>().alpha = 1;
        	AlwaysShown.GetComponent<CanvasGroup>().interactable = true;
        }else if(AlwaysShownbool == false){
        	AlwaysShown.GetComponent<CanvasGroup>().alpha = 0;
        	AlwaysShown.GetComponent<CanvasGroup>().interactable = false;
        }

        if(baublesbool == true){
        	baubles.GetComponent<CanvasGroup>().alpha = 1;
        	baubles.GetComponent<CanvasGroup>().interactable = true;
        }else if(baublesbool == false){
        	baubles.GetComponent<CanvasGroup>().alpha = 0;
        	baubles.GetComponent<CanvasGroup>().interactable = false;
        }
    }
}
