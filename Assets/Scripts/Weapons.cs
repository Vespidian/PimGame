using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    Rigidbody draggedObjectRb;

	private Text weaponTextMain;
    private Text weaponTextPrev;
    private Text weaponTextNext;
	private int numOfWeapons = 3;
	private bool canChangeWeapon = true;

	public int weapon = 1;

    private GameObject weldObj0;
    private GameObject weldObj1;
    private int weldObjectNum;
    private FixedJoint joint;

    //WEAPONS
    string weapon1 = "Physics Gun";
    string weapon2 = "Impulse Gun";
    string weapon3 = "Tool Gun";
    /*
    == List of tools in order starting with 1 ==
    
    Delete Tool
    Weld Tool
    Thruster Tool
    Wheel Tool
    */
    public int tool = 1;

    //SCRIPTS
    private Character_Controller thePlayer;
    private DragObj draggedObjectScript;
    private CamMouseLook cameraVars;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player").GetComponent<Character_Controller>();
        cameraVars = GameObject.Find("Camera").GetComponent<CamMouseLook>();
        weaponTextPrev = GameObject.Find("WeaponPrev").GetComponent<Text>();
        weaponTextMain = GameObject.Find("WeaponMain").GetComponent<Text>();
        weaponTextNext = GameObject.Find("WeaponNext").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Scroll through weapons
	    if(canChangeWeapon == true){
        	if(Input.GetAxisRaw("Mouse ScrollWheel") > 0){//-
	   			if(weapon != 1){
	   				weapon--;
	   			}else{
	   				weapon = numOfWeapons;
	   			}
	   		}if(Input.GetAxisRaw("Mouse ScrollWheel") < 0){//+
	   			if(weapon != numOfWeapons){
	   				weapon++;
	   			}else{
	   				weapon = 1;
	   			}
	   		}
        }
        CheckWeapon();
    }//end Update

    void FixedUpdate(){
        if(thePlayer.hit.collider != null){
            if(thePlayer.hit.collider.gameObject.GetComponent<DragObj>() != null){
                draggedObjectScript = thePlayer.hit.collider.gameObject.GetComponent<DragObj>();
            }
            if(thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>() != null){
                draggedObjectRb = thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>();
            }
        }
        if(draggedObjectScript != null){
            if(cameraVars.mouseMove == true){
                if(Input.GetMouseButtonDown(0)){//PRESSED LEFT MOUSE BUTTON
                    if(weapon == 3){
                        if(tool == 2){
                            WeldObjects();
                        }
                        if(tool == 3){
                            SpawnThruster();
                        }else if(tool == 4){
                            SpawnWheel();
                        }
                    }
                    if(weapon == 2){
                        ImpulseObject();
                    }
                }

                if(Input.GetMouseButtonDown(1)){//PRESSED RIGHT MOUSE BUTTON
                    if(weapon == 3){
                        if(tool == 2){
                            RemoveWeld();
                        }
                    }
                }

                if(Input.GetMouseButton(0)){//PRESSING LEFT MOUSE BUTTON
                    if(weapon == 3){
                        if(tool == 1){
                            DeleteObject();
                        }
                    }
                    canChangeWeapon = false;
                }else{
                    canChangeWeapon = true;
                }
            }
        }
    }//end FixedUpdate

    void CheckWeapon(){
    	if(weapon == 1){//PHYSICS GUN
    		PhysicsGun();
    	}
    	if(weapon == 2){//IMPULSE
    		ImpulseGun();
    	}
        if(weapon == 3){
            ToolGun();
        }
    }
    //WEAPON UI
    void PhysicsGun() {
        weaponTextPrev.text = weapon3;//CHANGE THIS WHEN ADDING WEAPONS

        weaponTextMain.text = weapon1;
        weaponTextNext.text = weapon2;
    }
    void ImpulseGun() {
        weaponTextPrev.text = weapon1;
        weaponTextMain.text = weapon2;
        weaponTextNext.text = weapon3;
        //ImpulseObject();
    }
    void ToolGun() {
        weaponTextPrev.text = weapon2;
        weaponTextMain.text = weapon3;

        weaponTextNext.text = weapon1;//CHANGE THIS WHEN ADDING WEAPONS
    }//END WEAPON UI

    //TOOL FUNCTIONS
    void WeldObjects() {
        if(draggedObjectScript.allowWeld == true){
            if(weldObjectNum == 0){//Selecting first object
                if(draggedObjectRb != null){
                    weldObj0 = thePlayer.hit.collider.gameObject;
                    weldObjectNum = 1;
                    ShowSelector();
                }
            }else if(weldObjectNum == 1) {//Selecting second object
                if(draggedObjectRb != null){
                    weldObj1 = thePlayer.hit.collider.gameObject;
                    weldObjectNum = 0;

                    if(weldObj0 != weldObj1){
                        joint = weldObj0.AddComponent<FixedJoint>();
                        joint.connectedBody = weldObj1.GetComponent<Rigidbody>();
                        ShowSelector();
                    }
                }
            }
        }
    }
    void RemoveWeld() {
        Destroy(thePlayer.hit.collider.gameObject.GetComponent<FixedJoint>());
    }

    void DeleteObject(){
        if(Input.GetMouseButton(1) || Input.GetMouseButtonDown(0)){
            if(thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>() != null){
                if(draggedObjectScript.allowDelete == true){
                    Destroy(thePlayer.hit.collider.gameObject);
                    ShowSelector();
                }
            }
        }
    }

    void ImpulseObject() {
        if(draggedObjectRb != null){
            draggedObjectScript.DynamicObject();
            thePlayer.hit.rigidbody.AddForceAtPosition(GameObject.Find("Camera").transform.forward * thePlayer.pokeForce * 1000, thePlayer.hit.point);
            ShowSelector();
        }
    }

    void SpawnThruster() {
        if(draggedObjectRb != null){
            var tmpThrust = Instantiate(thePlayer.thruster, thePlayer.hit.point, Quaternion.LookRotation(thePlayer.hit.normal));
            tmpThrust.transform.parent = thePlayer.hit.collider.gameObject.transform;
            ShowSelector();
        }
    }

    void SpawnWheel(){
        if(draggedObjectRb != null){
            var tmpWheel = Instantiate(thePlayer.wheel, thePlayer.hit.point, Quaternion.LookRotation(thePlayer.hit.normal));
            tmpWheel.GetComponent<HingeJoint>().connectedBody = thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>();
            ShowSelector();
        }
    }

    void ShowSelector() {
        var selector = Instantiate(thePlayer.selector, thePlayer.hit.point, Quaternion.LookRotation(thePlayer.hit.normal));
        Destroy(selector, 0.1f);
    }
}
