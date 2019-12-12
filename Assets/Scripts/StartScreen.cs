using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    public GameObject startScreen;

    //SCRIPTS
    private CamMouseLook cameraVars;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void cycleSplash(){
        enableSplash();
        Invoke("disableSplash", 1);
    }
    
    public void enableSplash(){
        startScreen.SetActive(true);
        //Debug.Log("enable splash");
    }

    public void disableSplash(){
        startScreen.SetActive(false);
        //Debug.Log("Disable splash");
    }
    void Update() {

    }
}
