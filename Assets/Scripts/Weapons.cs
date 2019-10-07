using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    Rigidbody draggedObjectRb;

	public bool changingWeapon = true;
	public int weapon = 1;
	private int numOfWeapons = 6;
	private Text weaponTextMain;
    private Text weaponTextPrev;
    private Text weaponTextNext;

    private GameObject weldObj0;
    private GameObject weldObj1;
    private int weldObjectNum;
    private FixedJoint joint;

    string weapon1 = "Physics Gun";
    string weapon2 = "Impulse Gun";
    string weapon3 = "Delete Tool";
    string weapon4 = "Weld Tool";
    string weapon5 = "Thruster Cannon";
    string weapon6 = "Wheel Placer";
    //string weapon7 = "";

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
        //if weapons is in use do not change
        if(cameraVars.mouseMove == true){
        	if(Input.GetMouseButtonDown(0)){
                if(weapon == 4){
                    WeldObjects();
                }
                if(weapon == 5){
                    SpawnThruster();
                }else if(weapon == 6){
                    SpawnWheel();
                }
        	}
            if(Input.GetMouseButtonDown(1)){
                if(weapon == 4){
                    RemoveWeld();
                }
            }

        	if(Input.GetMouseButton(0)){
                if(weapon == 2){
                    ImpulseObject();
                }
                if(weapon == 3){
                    DeleteObject();
                }
        		changingWeapon = false;
        	}else{
                changingWeapon = true;
            }
        }
        //Scroll through weapons
	    if(changingWeapon == true){
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
    }

    void FixedUpdate(){
        if(thePlayer.hit.collider != null){
            if(thePlayer.hit.collider.gameObject.GetComponent<DragObj>() != null){
                draggedObjectScript = thePlayer.hit.collider.gameObject.GetComponent<DragObj>();
            }
            if(thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>() != null){
                draggedObjectRb = thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>();
            }
        }
    }

    void CheckWeapon(){
    	if(weapon == 1){//PHYSICS GUN
    		PhysicsGun();
    	}
    	if(weapon == 2){//IMPULSE
    		ImpulseGun();
    	}
        if(weapon == 3){//DELETE OBJECTS
            DeleteTool();
        }
        if(weapon == 4){//WELD TOOL
            WeldTool();
        }
        if(weapon == 5){//THRUSTER SPAWNER
            ThrusterSpawner();  
        }
        if(weapon == 6){//Wheel Tool
            WheelSpawner();  
        }
    }

    void PhysicsGun() {
        weaponTextPrev.text = weapon6;//CHANGE THIS WHEN ADDING WEAPONS

        weaponTextMain.text = weapon1;
        weaponTextNext.text = weapon2;
    }
    void ImpulseGun() {
        weaponTextPrev.text = weapon1;
        weaponTextMain.text = weapon2;
        weaponTextNext.text = weapon3;
        //ImpulseObject();
    }
    void DeleteTool() {
        weaponTextPrev.text = weapon2;
        weaponTextMain.text = weapon3;
        weaponTextNext.text = weapon4;
        //DeleteObject();
    }
    void WeldTool() {
        weaponTextPrev.text = weapon3;
        weaponTextMain.text = weapon4;
        weaponTextNext.text = weapon5;
    }
    void ThrusterSpawner() {
        weaponTextPrev.text = weapon4;
        weaponTextMain.text = weapon5;
        weaponTextNext.text = weapon6;
    }
    void WheelSpawner() {
        weaponTextPrev.text = weapon5;
        weaponTextMain.text = weapon6;

        weaponTextNext.text = weapon1;//CHANGE THIS WHEN ADDING WEAPONS
    }


    void WeldObjects() {
        if(draggedObjectScript.allowWeld == true){
            if(weldObjectNum == 0){//Selecting first object
                if(draggedObjectRb != null){
                    weldObj0 = thePlayer.hit.collider.gameObject;
                    weldObjectNum = 1;
                }
            }else if(weldObjectNum == 1) {//Selecting second object
                if(draggedObjectRb != null){
                    weldObj1 = thePlayer.hit.collider.gameObject;
                    weldObjectNum = 0;

                    if(weldObj0 != weldObj1){
                        joint = weldObj0.AddComponent<FixedJoint>();
                        joint.connectedBody = weldObj1.GetComponent<Rigidbody>();
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
                }
            }
        }
    }

    void ImpulseObject() {
        if(Input.GetMouseButtonDown(0)){
            draggedObjectScript.DynamicObject();
            thePlayer.hit.rigidbody.AddForceAtPosition(GameObject.Find("Camera").transform.forward * thePlayer.pokeForce * 1000, thePlayer.hit.point);
        }
    }

    void SpawnThruster() {
        if(thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>() != null){
            var tmpThrust = Instantiate(thePlayer.thruster, thePlayer.hit.point, Quaternion.LookRotation(thePlayer.hit.normal));
            tmpThrust.transform.parent = thePlayer.hit.collider.gameObject.transform;
        }
    }

    void SpawnWheel(){
        if(thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>() != null){
            var tmpWheel = Instantiate(thePlayer.wheel, thePlayer.hit.point, Quaternion.LookRotation(thePlayer.hit.normal));
            tmpWheel.GetComponent<HingeJoint>().connectedBody = thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>();
        }
    }
}
