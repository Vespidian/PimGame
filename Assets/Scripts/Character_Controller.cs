using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
	Rigidbody rb;

	Vector3 direction;

	public Vector3 spawnPoint;
	public RaycastHit hit;
	public RaycastHit staticHit;

	[Header("Tool Settings")]
	public Transform destPoint;
	public float scrollSpeed = 0.3f;
	public float scrollDist = 5.0f;
	public float scrollMin = 2;
	public float scrollMax = 60;
	public int pokeForce = 40;

	[Header("Movement Settings")]
	public float speed = 6.0f;
	public float sprint = 10.0f;
	public float crouch = 4.0f;
	public float jumpHeight = 5.0f;
	private int walkType;//0 = walk / 1 = sprint / 2 = crouch
	private bool landed;
	private bool flying = false;

	[Header("Object Respawn Settings")]
	public int objSpawnRange = 5;
	public Vector3 objSpwnPoint = new Vector3(0, 20, 0);

	public bool rayHitting = false;

	private CamMouseLook cameraVars;

	float translation;
	float strafe;
	
    // Start is called before the first frame update
    void Start()
    {
    	spawnPoint = new Vector3(0, 20, 0);
    	cameraVars = GameObject.Find("Camera").GetComponent<CamMouseLook>();

    	walkType = 0;
		rb = this.gameObject.GetComponent<Rigidbody>();
		Application.targetFrameRate = 70;
    }

    // Update is called once per frame
    void Update(){
    	if(cameraVars.mouseMove == true){
	    	if(Input.GetKey(KeyCode.LeftShift)){
	    		walkType = 1;
	    	}else if(Input.GetKeyUp(KeyCode.LeftShift)){
	    		walkType = 0;
	    	}

	    	if(Input.GetKey(KeyCode.LeftControl)){
	    		walkType = 2;
	    		GameObject.Find("Camera").transform.localPosition = new Vector3(0, 0, 0);
	    	}else if(Input.GetKeyUp(KeyCode.LeftControl)){
	    		walkType = 0;
	    		GameObject.Find("Camera").transform.localPosition = new Vector3(0, 1, 0);
	    	}

	    	if(Input.GetKeyDown(KeyCode.Space)){
	    		if(landed == true){
	    			jump();
	    		}
	    		landed = false;
	    	}

	    	if(Input.GetKeyDown(KeyCode.X)){//Toggle flight
	    		if(flying == true){//disable
	    			flying = false;
	    			setFlight();
	    		}else if(flying == false){//enable
	    			flying = true;
	    			setFlight();
	    		}
	    	}
    	}
    	if(gameObject.transform.position.y <= -20){
	        spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
	        rb.velocity = new Vector3(0, 0, 0);
		   	gameObject.transform.position = spawnPoint;
		}
    }

    void FixedUpdate() {
    	if(cameraVars.mouseMove == true){
	    	if(walkType == 0){
				translation = Input.GetAxis("Vertical") * speed; 
			   	strafe = Input.GetAxis("Horizontal") * speed;
			}else if(walkType == 1){
				translation = Input.GetAxis("Vertical") * sprint; 
			   	strafe = Input.GetAxis("Horizontal") * sprint;
			}else if(walkType == 2){
				translation = Input.GetAxis("Vertical") * crouch; 
			   	strafe = Input.GetAxis("Horizontal") * crouch;
			}
		    if(walkType == 0){//WALK
			    direction = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
			    direction.z = translation;
			    direction.x = strafe;
			    rb.velocity = transform.TransformDirection(direction);
			}else if(walkType == 1) {//SPRINT
			    direction = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
			    direction.z = translation;
			    direction.x = strafe;
				rb.velocity = transform.TransformDirection(direction);
			}else if(walkType == 2) {//CROUCH
			    direction = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
			    direction.z = translation;
			    direction.x = strafe;
				rb.velocity = transform.TransformDirection(direction);
			}else{
				rb.velocity = Vector3.zero;
			}

			if(flying == true) {
				GetComponent<Collider>().enabled = false;

				rb.velocity = (GameObject.Find("Camera").transform.forward * translation*2) + (GameObject.Find("Camera").transform.right * strafe*2);
				if((Input.GetKeyUp(KeyCode.W)) || (Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.D))){
					rb.velocity = new Vector3(0, 0, 0);
				}
				if(Input.GetKey(KeyCode.Space)){
					rb.velocity = new Vector3(rb.velocity.x, sprint, rb.velocity.z);
				}else if(Input.GetKeyUp(KeyCode.Space)){
					rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
				}
			}else{
				GetComponent<Collider>().enabled = true;
			}

		    if (Physics.Raycast(GameObject.Find("Camera").transform.position, GameObject.Find("Camera").transform.forward, out hit, Mathf.Infinity)){
		    	rayHitting = true;
		   		Debug.DrawRay(GameObject.Find("Camera").transform.position, GameObject.Find("Camera").transform.forward * hit.distance, Color.red);
		   	}else{
		   		rayHitting = false;
		   	}
			if(Input.GetMouseButtonDown(0)){
				Physics.Raycast(GameObject.Find("Camera").transform.position, GameObject.Find("Camera").transform.forward, out staticHit, Mathf.Infinity);
		    	destPoint.transform.localPosition = new Vector3(0, 0, hit.distance);
		    }
		}
    }

    void jump(){
    	rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision col){
    	landed = true;
    }

    void setFlight() {
    	if(flying == true){
    		rb.useGravity = false;
    	}else if (flying == false){
    		rb.useGravity = true;
    	}
    }
}