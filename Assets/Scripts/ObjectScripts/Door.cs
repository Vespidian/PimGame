using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public enum DoorType {Openable, Teleport, ChangeScene};
    public DoorType type;

    [Header("Teleport")]
    public Transform destination;


    [Header("Openable")]
	private Animation anim;
	public bool open = false;
	public bool allowReopen = true;
    public bool useTrigger = false;
    public bool stayOpen = true;
	private string animType;

    [Header("Change Scene")]
    public string newSceneName = "StartMenu";

    [Header("Runtime")]
    public string doorTypeText = "";

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        if(type == DoorType.Teleport){
            doorTypeText = "teleport";
        }else if(type == DoorType.Openable){
            doorTypeText = "openable";
        }else if(type == DoorType.ChangeScene){
            doorTypeText = "changescene";
        }

        if(open == true){
        	animType = "Open";
            open = false;
        	Open();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setScene() {
        SceneManager.LoadScene(newSceneName);
    }

    void OnTriggerEnter(Collider col){
        if(type == DoorType.Openable && useTrigger == true){
        	if(allowReopen == true){
        		if(open == false){
        			animType = "Open";
        			Invoke("PlayAnimation", 1);
        		}
        	}
    		if(open == true){
    			animType = "Close";
    			Invoke("PlayAnimation", 1);
    		}
        }
    }

    void PlayAnimation(){
        if(type == DoorType.Openable){
        	if(animType == "Open"){
        			Open();
        	}else if(animType == "Close"){
        		Close();
        	}
        }
    }

    public void Open(){
        if(open == false && anim.IsPlaying("Door|Close") == false){
            open = true;
            anim.Play("Door|Open");
            if(stayOpen == false){
                animType = "Close";
                Invoke("PlayAnimation", 4);
            }
        }
    }
    public void Close(){
        if(open == true && anim.IsPlaying("Door|Open") == false){
            open = false;
            anim.Play("Door|Close");
        }
    }

    public void ToggleState(){
        if(open == true){
            Close();
        }else if(open == false){
            Open();
        }
    }
}
