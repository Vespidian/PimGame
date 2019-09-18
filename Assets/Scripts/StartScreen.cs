using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{

	public RawImage image;
	public RawImage background;
	public Text text;
    private CamMouseLook cameraVars;


    // Start is called before the first frame update
    void Start()
    {
        cameraVars = GameObject.Find("Camera").GetComponent<CamMouseLook>();
        
        cycleSplash();
    }
    public void cycleSplash(){
        enableSplash();
        Invoke("disableSplash", 1);
        //disableSplash();
    }
    void enableSplash(){
        cameraVars.mouseMove = false;
        image.enabled = true;
        background.enabled = true;
        text.enabled = true;
    }

    void disableSplash(){
        cameraVars.mouseMove = true;
    	image.enabled = false;
    	background.enabled = false;
    	text.enabled = false;
    }
    void Update() {

    }
}
