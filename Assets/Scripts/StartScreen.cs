using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{

	public RawImage image;
	public RawImage background;
	public Text text;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("disableSplash", 2);
        //disableSplash();
    }

    public void disableSplash(){
    	image.enabled = false;
    	background.enabled = false;
    	text.enabled = false;
    }
}
