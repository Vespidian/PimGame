using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public void quitGame() {
    	Application.Quit();
    }
    public void helpLink() {
    	Application.OpenURL("https://github.com/Vespidian/Unity-Gmod-Physgun/blob/master/README.md");
    }
}
