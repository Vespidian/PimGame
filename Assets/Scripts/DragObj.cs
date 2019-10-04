using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObj : MonoBehaviour
{
	Quaternion rot;
	Quaternion startRot;

	Rigidbody objRb;

	Vector3 pauseVelocity;
	Vector3 dragObjRel;
	Vector3 playerDirection;
	Vector2 mousePos;
	Vector2 smoothV;
	Vector2 objRotation;
	public float dragSpeed = 20;
	public bool allowDelete = false;
	public bool allowWeld = true;
	public bool allowDragging = true;

	private bool stopRotation = false;
	private bool freezeObject = false;
	private bool toggleHold = false;
	private Vector3 buoyancyLift;

	Vector3 startPosition;
	Vector3 rayHitDifference;

	private Character_Controller thePlayer;
	private Weapons playerTools;
	private CamMouseLook cameraVars;

    // Start is called before the first frame update
    void Start()
    {
    	thePlayer = GameObject.Find("Player").GetComponent<Character_Controller>();
    	playerTools = GameObject.Find("Player").GetComponent<Weapons>();
    	cameraVars = GameObject.Find("Camera").GetComponent<CamMouseLook>();

        objRb = gameObject.GetComponent<Rigidbody>();
        buoyancyLift = -Physics.gravity * (2 - objRb.velocity.y * 5);
    }

    void OnMouseDown() {
    	if(playerTools.weapon == 1){
	    	stopRotation = true;
	    	freezeObject = false;
    		toggleHold = true;

    		rayHitDifference = this.transform.InverseTransformPoint(thePlayer.staticHit.point);

    		Debug.Log(rayHitDifference);
    		Debug.Log(transform.rotation);

    		startRot = transform.rotation;
    		//Transforms this objects position from worldspace to the camera's localspace
    		dragObjRel = GameObject.Find("Camera").transform.InverseTransformPoint(this.transform.position);
		   	thePlayer.scrollDist = dragObjRel.z;

		   	thePlayer.destPoint.transform.localPosition = new Vector3(0, 0, dragObjRel.z);
		   	
    	}
    	if(playerTools.weapon == 2){
    		ShootObject();
    	}

    }
    void OnMouseUp() {
    	if(playerTools.weapon == 1){
	    	stopRotation = false;
	    }
	    toggleHold = false;
	    cameraVars.mouseMove = true;
    }
    void OnMouseOver() {
    	if((Input.GetMouseButton(0)) || (Input.GetMouseButtonDown(1))){
    		if((playerTools.weapon == 3) && (allowDelete == true)){
    			Destroy(this.gameObject);
    		}
    	}
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


		    	if((Input.GetMouseButtonDown(1)) && (toggleHold == true)){
	    			freezeObject = true;
	    		}
			    if(Input.GetKeyDown(KeyCode.F)){
			   		DynamicObject();
			   	}

			   	scrollDestination();
				if((Input.GetKey(KeyCode.E)) && (toggleHold == true)){
					cameraVars.mouseMove = false;
					mousePos = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
					objRotation += mousePos;

					rot = Quaternion.Euler(startRot.x + objRotation.x, startRot.y + -objRotation.y, startRot.z);
					//startRot = Quaternion.Euler(startRot.x + objRotation.x, startRot.y + -objRotation.y, startRot.z);


					transform.RotateAround(transform.position, GameObject.Find("Camera").transform.right, mousePos.y);
					transform.RotateAround(transform.position, GameObject.Find("Camera").transform.up, -mousePos.x);


					//objRb.AddTorque(GameObject.Find("Camera").transform.up * -mousePos.x);
					//objRb.AddTorque(GameObject.Find("Camera").transform.right * mousePos.y);
				}else if(Input.GetKeyUp(KeyCode.E))
				{
					cameraVars.mouseMove = true;
				}
				if(Input.GetMouseButton(0)){

	         	}
			}
			if(playerTools.weapon == 4){
				if(gameObject.GetComponent<FixedJoint>() != null){
					if(thePlayer.hit.collider == gameObject.GetComponent<FixedJoint>().connectedBody.gameObject.GetComponent<Collider>()){
						if(Input.GetMouseButtonDown(1)){
							Destroy(gameObject.GetComponent<FixedJoint>());
						}
					}
				}
			}
		}
    }

    void OnTriggerStay(Collider col){
    	if(col.gameObject.name == "Water"){
    		objRb.AddForceAtPosition(buoyancyLift, transform.position);
    		objRb.drag = 3;
    		//objRb.AddForce(objRb.velocity * -1 * 1);
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

    void scrollDestination(){
    	if(toggleHold == true){
	    	if(Input.GetAxis("Mouse ScrollWheel") < 0){
			   	thePlayer.scrollDist -= thePlayer.scrollSpeed;
			}
		    if(Input.GetAxis("Mouse ScrollWheel") > 0){
			   	thePlayer.scrollDist += thePlayer.scrollSpeed;
			}
			thePlayer.scrollDist = Mathf.Clamp(thePlayer.scrollDist, thePlayer.scrollMin, thePlayer.scrollMax);
			thePlayer.destPoint.transform.localPosition = new Vector3(0, 0, thePlayer.scrollDist);
		}
    }
    void DynamicObject(){
    	objRb.useGravity = true;
		objRb.freezeRotation = false;
	  	objRb.isKinematic = false;

	  	stopRotation = false;
	  	freezeObject = false;
    }
    void HoldObject() {
    	objRb.useGravity = true;
		objRb.freezeRotation = true;
		objRb.isKinematic = false;
		
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
    	objRb.useGravity = false;
	    objRb.freezeRotation = true;
	    objRb.isKinematic = true;
    }
    void ShootObject(){
    	DynamicObject();
	    thePlayer.hit.rigidbody.AddForceAtPosition(GameObject.Find("Camera").transform.forward * thePlayer.pokeForce * 1000, thePlayer.hit.point);
    }
}
