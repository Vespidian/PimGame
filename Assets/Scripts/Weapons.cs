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

    private GameObject dupeObject;
    private Quaternion dupeRotation;
    private GameObject previewObject;
    private bool previewVisible = false;

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
    Duplicator Tool
    */
    public int tool = 1;

    //SCRIPTS
    private Character_Controller thePlayer;
    private DragObj draggedObjectScript;
    private CamMouseLook cameraVars;
    private PhysicsRestrictions objectRestrictions;

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
        if(thePlayer.hit.collider != null){
            if(cameraVars.mouseMove == true){
                if(Input.GetMouseButtonDown(0)){//PRESSED LEFT MOUSE BUTTON
                    if(weapon == 3){
                        if(tool == 1){
                            DeleteObject();
                        }else if(tool == 2){
                            WeldObjects("weld");
                        }else if(tool == 3){
                            SpawnThruster();
                        }else if(tool == 4){
                            SpawnWheel();
                        }else if(tool == 5){
                            DuplicateObject("left");
                        }
                    }
                    if(weapon == 2){
                        ImpulseObject();
                    }
                }
                if(Input.GetMouseButton(0)){//PRESSING LEFT MOUSE BUTTON
                    if(weapon == 3){
                    }
                    canChangeWeapon = false;
                }else{
                    canChangeWeapon = true;
                }

                if(Input.GetMouseButtonDown(1)){//PRESSED RIGHT MOUSE BUTTON
                    if(weapon == 3){
                        if(tool == 2){
                            WeldObjects("remove");
                        }else if(tool == 5){
                            DuplicateObject("right");
                        }
                    }
                }else if(Input.GetMouseButton(1)){
                    if(weapon == 3){
                        if(tool == 1){
                            DeleteObject();
                        }
                    }
                }
                if(weapon == 3){
                    if(tool == 1){//Delete Tool

                    }else if(tool == 2){// Weld Tool

                    }else if(tool == 3){// Thruster Tool
                        SpawnPreview("thruster");
                    }else if(tool == 4){// Wheel Tool
                        SpawnPreview("wheel");
                    }else if(tool == 5){// Duplicate Tool

                    }
                }
                if(Input.GetKeyDown(KeyCode.R)){
                    if(weapon == 1){
                        if(draggedObjectScript != null){
                            draggedObjectScript.DynamicObject();
                        }
                    }else if(weapon == 3){
                        if(tool == 2){
                            WeldObjects("remove");
                        }
                    }
                }
            }
        }
    }//end Update

    void FixedUpdate(){
        if(thePlayer.hit.collider != null){
            //if(thePlayer.hit.collider.gameObject.GetComponent<DragObj>() != null){
                draggedObjectScript = thePlayer.hit.collider.gameObject.GetComponent<DragObj>();
                objectRestrictions = thePlayer.hit.collider.gameObject.GetComponent<PhysicsRestrictions>();
            //}
            //if(thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>() != null){
                draggedObjectRb = thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>();
            //}
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
    void WeldObjects(string function) {
        if(function == "weld"){
            if(objectRestrictions.allowWeld == true){
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
        }else if(function == "remove"){
            Destroy(thePlayer.hit.collider.gameObject.GetComponent<FixedJoint>());
        }
    }

    void DeleteObject() {
        if(draggedObjectScript != null){
            if(objectRestrictions.allowDelete == true){
                if(objectRestrictions.allowDelete == true){
                    Destroy(thePlayer.hit.collider.gameObject);
                    ShowSelector();
            }
            }
        }
    }

    void ImpulseObject() {
        if(draggedObjectRb != null){
            ShowSelector();
            draggedObjectScript.DynamicObject();
            thePlayer.hit.rigidbody.AddForceAtPosition(GameObject.Find("Camera").transform.forward * thePlayer.pokeForce * 1000, thePlayer.hit.point);
        }
    }

    void SpawnThruster() {
        if(draggedObjectRb != null){
            if(objectRestrictions.allowThruster == true){
                ShowSelector();
                var tmpThrust = Instantiate(thePlayer.thruster, thePlayer.hit.point, Quaternion.LookRotation(thePlayer.hit.normal));
                tmpThrust.transform.parent = thePlayer.hit.collider.gameObject.transform;
            }
        }
    }

    void SpawnWheel() {
        if(draggedObjectRb != null){
            if(objectRestrictions.allowWheel == true){
                ShowSelector();
                var tmpWheel = Instantiate(thePlayer.wheel, thePlayer.hit.point, Quaternion.LookRotation(thePlayer.hit.normal));
                tmpWheel.GetComponent<HingeJoint>().connectedBody = thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>();
            }
        }
    }


    void DuplicateObject(string click) {
        if(click == "left"){
            if(thePlayer.hit.collider != null && draggedObjectRb != null){
                dupeObject = thePlayer.hit.collider.gameObject;
                dupeRotation = dupeObject.transform.rotation;
            }
        }
        if(click == "right" && dupeObject != null){
            Instantiate(dupeObject, thePlayer.hit.point + new Vector3(0, 1, 0), dupeRotation);
        }
    }

    void SpawnPreview(string item) {
        if(thePlayer.hit.collider != null && draggedObjectRb != null){
            if(previewVisible == false){
                if(item == "thruster"){
                    previewVisible = true;
                    previewObject = Instantiate(thePlayer.thrusterPreview, thePlayer.hit.point, Quaternion.LookRotation(thePlayer.hit.normal));
                }else if(item == "wheel"){
                    previewVisible = true;
                    previewObject = Instantiate(thePlayer.wheelPreview, thePlayer.hit.point, Quaternion.LookRotation(thePlayer.hit.normal));
                }
            }
            previewObject.transform.rotation = Quaternion.LookRotation(thePlayer.hit.normal);
            previewObject.transform.position = thePlayer.hit.point;
        }else if(previewObject != null){
            Destroy(previewObject);
            previewVisible = false;
        }
    }

    void ShowSelector() {
        var selector = Instantiate(thePlayer.selector, thePlayer.hit.point, Quaternion.LookRotation(thePlayer.hit.normal));
        Destroy(selector, 0.2f);
    }
}
