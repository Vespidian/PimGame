using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObj : MonoBehaviour
{
	//PRIVATE VARIABLES
	Quaternion rot;
	Quaternion startRot;

	Rigidbody objRb;

	Vector3 buoyancyLift;
	Vector3 startPosition;
	Vector3 rayHitDifference;
	Vector3 dragObjRel;
	Vector2 mousePos;
	Vector2 smoothV;
	Vector2 objRotation;
	private bool stopRotation = false;
	private bool freezeObject = false;

	//EDITOR VARIABLES
	public float dragSpeed = 20;
	public bool allowDelete = false;
	public bool allowWeld = true;
	public bool allowDragging = true;
	public bool enableBuoyancy = true;

	//OUTER-SCRIPT VARIABLES
	public bool dragging = false;

	//SCRIPTS
	private Character_Controller thePlayer;
	private Weapons playerTools;
	private CamMouseLook cameraVars;

    // Start is called before the first frame update
    void Start()
    {
    	thePlayer = GameObject.Find("Player").GetComponent<Character_Controller>();
    	playerTools = GameObject.Find("Player").GetComponent<Weapons>();
    	cameraVars = GameObject.Find("Camera").GetComponent<CamMouseLook>();

    	if(gameObject.GetComponent<Rigidbody>() != null){
        	objRb = gameObject.GetComponent<Rigidbody>();
        	buoyancyLift = -Physics.gravity * (2 - objRb.velocity.y * 5);
    	}else{
    		allowDragging = false;
    	}
    }

    void OnMouseDown() {
    	if(playerTools.weapon == 1){
	    	stopRotation = true;
	    	freezeObject = false;
    		thePlayer.toggleHold = true;
    		dragging = true;

    		rayHitDifference = this.transform.InverseTransformPoint(thePlayer.staticHit.point);

    		//Debug.Log(rayHitDifference);
    		//Debug.Log(transform.rotation);

    		startRot = transform.rotation;
    		//Transforms this objects position from worldspace to the camera's localspace
    		dragObjRel = GameObject.Find("Camera").transform.InverseTransformPoint(this.transform.position);
		   	thePlayer.scrollDist = dragObjRel.z;

		   	thePlayer.destPoint.transform.localPosition = new Vector3(0, 0, dragObjRel.z);
		   	
    	}
    }

    void OnMouseOver() {
    	if(Input.GetMouseButton(0)){
    		dragging = true;
    	}else if(Input.GetMouseButtonUp(0)){
    		dragging = false;
		}
    }

    void OnMouseUp() {
    	if(playerTools.weapon == 1){
	    	stopRotation = false;
	    	dragging = false;
	    }
	    thePlayer.toggleHold = false;
	    cameraVars.mouseMove = true;
    }

    void FixedUpdate(){
    	if(thePlayer.allowTools == true){
	    	if((playerTools.weapon == 1) && (allowDragging == true)){
		    	if((stopRotation == true) && (freezeObject == false)){
		    		HoldObject();

		    	}else if((stopRotation == false) && (freezeObject == false)){
		    		if(cameraVars.mouseMove == true){
			   			DynamicObject();
			   		}

			    }else if(freezeObject == true){
		    		FreezeObject();
		    	}


		    	if((Input.GetMouseButtonDown(1)) && (thePlayer.toggleHold == true)){
	    			freezeObject = true;
	    		}
			    if(Input.GetKeyDown(KeyCode.F)){
			   		DynamicObject();
			   	}

				if((Input.GetKey(KeyCode.E)) && (thePlayer.toggleHold == true) && (dragging == true)) {
					cameraVars.mouseMove = false;
					mousePos = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
					objRotation += mousePos;

					rot = Quaternion.Euler(startRot.x + objRotation.x, startRot.y + -objRotation.y, startRot.z);
					//startRot = Quaternion.Euler(startRot.x + objRotation.x, startRot.y + -objRotation.y, startRot.z);


					this.transform.RotateAround(transform.position, GameObject.Find("Camera").transform.right, mousePos.y);
					this.transform.RotateAround(transform.position, GameObject.Find("Camera").transform.up, -mousePos.x);


					//objRb.AddTorque(GameObject.Find("Camera").transform.up * -mousePos.x);
					//objRb.AddTorque(GameObject.Find("Camera").transform.right * mousePos.y);
				}else if(Input.GetKeyUp(KeyCode.E))
				{
					cameraVars.mouseMove = true;
				}
			}
			if(playerTools.weapon == 4){
				if(Input.GetMouseButtonDown(1)){
					if(gameObject.GetComponent<FixedJoint>() != null){
						if(thePlayer.hit.collider == gameObject.GetComponent<FixedJoint>().connectedBody.gameObject.GetComponent<Collider>()){
							Destroy(gameObject.GetComponent<FixedJoint>());
						}
					}
				}
			}
		}
    }

    void OnTriggerStay(Collider col){
    	if(enableBuoyancy == true){
    		if(col.gameObject.name == "Water"){
    			objRb.AddForceAtPosition(buoyancyLift, transform.position);
    			objRb.drag = 3;
    			//objRb.AddForce(objRb.velocity * -1 * 1);
    		}
    	}
    }
    void OnTriggerExit(Collider col) {
    	if(col.gameObject.name == "Water"){
    		objRb.drag = 1;
    	}
    }
    void OnCollisionEnter(Collision collision){
    	if(collision.relativeVelocity.magnitude > 10){
    		Instantiate(thePlayer.smokeParticle, new Vector3(this.transform.position.x, collision.contacts[0].point.y + 0.2f, this.transform.position.z), Quaternion.identity);
    	}
    }

    public void DynamicObject(){
    	objRb.useGravity = true;
		objRb.freezeRotation = false;
	  	objRb.isKinematic = false;
	  	dragging = false;

	  	stopRotation = false;
	  	freezeObject = false;
    }
    void HoldObject() {
    	objRb.useGravity = true;
		objRb.freezeRotation = true;
		objRb.isKinematic = false;
		dragging = true;
		
		objRb.velocity = (thePlayer.destPoint.position - this.transform.position) * dragSpeed;

		/*if(startRot != this.transform.rotation){
			objRb.AddTorque((Vector3.right * (startRot.x - transform.rotation.x)) * 100);
			objRb.AddTorque((Vector3.up * (startRot.y - transform.rotation.y)) * 100);
			objRb.AddTorque((Vector3.forward * (startRot.z - transform.rotation.z)) * 100);
			//objRb.AddTorque(startRot.up - transform.rotation.up);
		}*/

		//Debug.Log(rayHitDifference);

		//objRb.velocity = (thePlayer.destPoint.position - rayHitDifference - this.transform.position) * dragSpeed;

    }
    void FreezeObject() {
    	if(dragging == true){
	    	objRb.useGravity = false;
		    objRb.freezeRotation = true;
		    objRb.isKinematic = true;
		    dragging = false;
		}
    }
}
