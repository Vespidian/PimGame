using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
	public bool changingWeapon = true;
	public int weapon = 1;
	private int numOfWeapons = 4;
	private Text weaponText;

    private GameObject weldObj0;
    private GameObject weldObj1;
    private int weldObjectNum;
    private FixedJoint joint;

    private Character_Controller thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player").GetComponent<Character_Controller>();
        weaponText = GameObject.Find("Weapon").GetComponent<Text>();
        changingWeapon = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if weapons is in use do not change
    	if(Input.GetMouseButtonDown(0)){
            if(weapon == 4){
                WeldObjects();
            }
    		changingWeapon = false;
    	}
        if(Input.GetMouseButtonDown(1)){
            if(weapon == 4){
                RemoveWeld();
            }
        }

    	if(Input.GetMouseButtonUp(0)){
    		changingWeapon = true;
    	}
        //Scroll through weapons
	    if(changingWeapon == true){
        	if(Input.GetAxisRaw("Mouse ScrollWheel") < 0){//-
	   			if(weapon != 1){
	   				weapon--;
	   			}else{
	   				weapon = numOfWeapons;
	   			}
	   		}if(Input.GetAxisRaw("Mouse ScrollWheel") > 0){//+
	   			if(weapon != numOfWeapons){
	   				weapon++;
	   			}else{
	   				weapon = 1;
	   			}
	   		}
        }
        CheckWeapon();
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
        /*if(weapon == 5){//THRUSTER CANNON *REMOVED*
            weaponText.text = "Thruster Cannon";  
        }*/
        /*if(weapon == 6){//GRAVITY GUN *BUGGED*
            weaponText.text = "Gravity Gun";  
        }*/
    }

    void PhysicsGun() {
        weaponText.text = "Physics Gun";
    }
    void ImpulseGun() {
        weaponText.text = "Impulse Gun";
    }
    void DeleteTool() {
        weaponText.text = "Delete Tool";
    }
    void WeldTool() {
        weaponText.text = "Weld Tool";
    }


    void WeldObjects() {
        if(thePlayer.hit.collider.gameObject.GetComponent<DragObj>().allowWeld == true){
            if(weldObjectNum == 0){//Selecting first object
                if(thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>() != null){
                    weldObj0 = thePlayer.hit.collider.gameObject;
                    weldObjectNum = 1;
                }
            }else if(weldObjectNum == 1) {//Selecting second object
                if(thePlayer.hit.collider.gameObject.GetComponent<Rigidbody>() != null){
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
}
