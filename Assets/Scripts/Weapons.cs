using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
	public bool changingWeapon = true;
	public int weapon = 1;
	private int numOfWeapons = 3;
	Text weaponText;

    // Start is called before the first frame update
    void Start()
    {
        weaponText = GameObject.Find("Weapon").GetComponent<Text>();
        changingWeapon = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if weapons is in use do not change
    	if(Input.GetMouseButtonDown(0)){
    		changingWeapon = false;
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
    		weaponText.text = "Physics Gun";
    	}
    	if(weapon == 2){//IMPULSE
    		weaponText.text = "Impulse Gun";
    	}
        if(weapon == 3){//DELETE OBJECTS
            weaponText.text = "Delete Tool";
        }
        /*if(weapon == 5){//THRUSTER CANNON *REMOVED*
            weaponText.text = "Thruster Cannon";  
        }*/
        /*if(weapon == 6){//GRAVITY GUN *BUGGED*
            weaponText.text = "Gravity Gun";  
        }*/
    }
}
