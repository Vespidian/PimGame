using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{

	public RawImage image;
	public RawImage background;
	public Text text;

    public Text FPS;

    // Start is called before the first frame update
    void Start()
    {
        enableSplash();
        //Invoke("disableSplash", 2);
        disableSplash();
    }
    void enableSplash(){
        image.enabled = true;
        background.enabled = true;
        text.enabled = true;
    }

    void disableSplash(){
    	image.enabled = false;
    	background.enabled = false;
    	text.enabled = false;
    }
    void Update() {
        FPS.text = (1f / Time.unscaledDeltaTime).ToString();
    }
}
