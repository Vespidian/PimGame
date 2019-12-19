using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PhysicsRestrictions))]

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
	private Vector3 relVelocity;

	//OUTER-SCRIPT VARIABLES
	public bool dragging = false;

	//SCRIPTS
	private CharController thePlayer;
	private Weapons playerTools;
	private CamMouseLook cameraVars;
	private PhysicsRestrictions selfRestrictions;// Get self restrictions

    // Start is called before the first frame update
    void Start()
    {
    	thePlayer = GameObject.Find("Player").GetComponent<CharController>();
    	playerTools = GameObject.Find("Player").GetComponent<Weapons>();
    	cameraVars = GameObject.Find("Camera").GetComponent<CamMouseLook>();
    	selfRestrictions = this.gameObject.GetComponent<PhysicsRestrictions>();

    	if(gameObject.GetComponent<Rigidbody>() != null){
        	objRb = gameObject.GetComponent<Rigidbody>();
        	buoyancyLift = -Physics.gravity * (2 - objRb.velocity.y * 5);
    	}else{
    		selfRestrictions.allowDragging = false;
    	}
    }

    void Update() {
    	if(selfRestrictions.wheel == true && GetComponent<HingeJoint>().connectedBody == null){
    		Destroy(gameObject);
    	}else if(selfRestrictions.hinge == true && GetComponent<FixedJoint>().connectedBody == null){
    		Destroy(gameObject);
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
    	if(Input.GetMouseButtonDown(0)){
    		dragging = true;
    	}else if(Input.GetMouseButtonUp(0)){
    		dragging = false;
		}
		if(playerTools.weapon == 3 && playerTools.weapon == 2){
			if(Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.R)){
				if(gameObject.GetComponent<FixedJoint>() != null){
					if(thePlayer.hit.collider == gameObject.GetComponent<FixedJoint>().connectedBody.gameObject.GetComponent<Collider>()){
						Destroy(gameObject.GetComponent<FixedJoint>());
						playerTools.ShowSelector();
					}
				}
			}
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
	    	if((playerTools.weapon == 1) && (selfRestrictions.allowDragging == true)){
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
		}
		if(selfRestrictions.reverseGravity == true && selfRestrictions.balloon == true){
			objRb.useGravity = false;
			objRb.AddForceAtPosition(new Vector3(0, 49.05f, 0), transform.TransformPoint(new Vector3(0, 0, 1.25f)), ForceMode.Acceleration);
		}else if(selfRestrictions.reverseGravity == true){
			objRb.useGravity = false;
			objRb.AddForce(new Vector3(0, 9.8f, 0), ForceMode.Acceleration);
		}
    }

    void OnTriggerStay(Collider col){
    	if(selfRestrictions.enableBuoyancy == true){
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
    		Instantiate(thePlayer.smokeParticle, collision.contacts[0].point, Quaternion.identity);
    	}
    }

    public void DynamicObject(){
    	//objRb.useGravity = true;
		objRb.freezeRotation = false;
	  	objRb.isKinematic = false;
	  	dragging = false;

	  	stopRotation = false;
	  	freezeObject = false;
    }
    void HoldObject() {
    	//objRb.useGravity = true;
		objRb.freezeRotation = true;
		objRb.isKinematic = false;
		dragging = true;
		
		//objRb.velocity = (thePlayer.destPoint.position - this.transform.position) * dragSpeed;

		//objRb.rotation = startRot;
		//objRb.rotation = Quaternion.Euler(startRot.x, startRot.y + GameObject.Find("Player").transform.rotation.y, startRot.z);

		/*if(startRot != this.transform.rotation){
			objRb.AddTorque((Vector3.right * (startRot.x - transform.rotation.x)) * 100);
			objRb.AddTorque((Vector3.up * (startRot.y - transform.rotation.y)) * 100);
			objRb.AddTorque((Vector3.forward * (startRot.z - transform.rotation.z)) * 100);
			//objRb.AddTorque(startRot.up - transform.rotation.up);
		}*/

		//Debug.Log(rayHitDifference);



		objRb.velocity = (thePlayer.destPoint.position - transform.TransformDirection(rayHitDifference) - this.transform.position) * dragSpeed;
		//relVelocity = transform.InverseTransformDirection((thePlayer.destPoint.position - rayHitDifference - this.transform.position) * dragSpeed);
		//objRb.velocity = transform.TransformDirection(relVelocity);

		//objRb.AddRelativeForce((thePlayer.destPoint.position - rayHitDifference - this.transform.position) * dragSpeed);
		//objRb.AddForce((thePlayer.destPoint.position - rayHitDifference - this.transform.position) * dragSpeed);

    }
    void FreezeObject() {
    	if(dragging == true){
	    	//objRb.useGravity = false;
		    objRb.freezeRotation = true;
		    objRb.isKinematic = true;
		    dragging = false;
		}
    }
}
