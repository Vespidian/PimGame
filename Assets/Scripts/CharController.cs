using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
	private Rigidbody rb;
	private LineRenderer line;
	private Vector3 lineDistance;
	private Vector3 direction;

	public Vector3 spawnPoint = new Vector3(0, 20, 0);
	public RaycastHit hit;
	public RaycastHit staticHit;

	[Header("Tool Settings")]
    public int numberOfItems = 0;
	public Transform destPoint;
	public GameObject selector;
	public float scrollSpeed = 0.3f;
	public float scrollDist = 5;
	public float scrollMin = 2;
	public float scrollMax = 500;
	public int pokeForce = 7;
	public bool rayHitting = false;
	public bool allowTools = true;
	public bool toggleHold = false;

	[Header("Movement Settings")]
	public float speed = 6;
	public float sprint = 14;
	public float crouch = 4;
	private float tempSpeed;
	public float jumpSpeedIncrease = 6;
	public float stallSpeed = 2;
	public float jumpHeight = 7;

	private float speedCopy;
	private float sprintCopy;
	private float crouchCopy;
	private int walkType = 0;//0 = walk / 1 = sprint / 2 = crouch
	private bool landed;
	private bool flying = false;
	private Vector3 upLift;
	public bool pauseGame = false;
	public bool sitting = false;
	private GameObject chairObject;
	public bool allowMovement = true;
	public bool allowFlight = true;
	public bool allowSlomo = true;
	private int jumpWalkType;

	[Header("Object Respawn Settings")]
	public int objSpawnRange = 5;
	public Vector3 objSpwnPoint = new Vector3(0, 20, 0);
	public float lowestPossiblePoint = -30;

	[Header("Misc")]
	private int collisionCount = 0;
	private CamMouseLook cameraVars;
	public GameObject smokeParticle;

	public GameObject thrusterPreview;
	public GameObject wheelPreview;
	public GameObject thruster;
	public GameObject wheel;
	public GameObject steering;
	public GameObject balloon;

	private float translation;
	private float strafe;

	//private bool init = false;

	private Weapons playerTools;
    private PhysicsRestrictions physRestricts;

    private Door doorScript;
	//private SpawnObjects spawningObj;
    // Start is called before the first frame update
    void Start()
    {
    	cameraVars = GameObject.Find("Camera").GetComponent<CamMouseLook>();
		playerTools = GameObject.Find("Player").GetComponent<Weapons>();
		//spawningObj = GameObject.Find("SpawnSelect").GetComponent<SpawnObjects>();


		speedCopy = speed;
		sprintCopy = sprint;
		crouchCopy = crouch;

		rb = this.gameObject.GetComponent<Rigidbody>();
		line = GameObject.Find("Pistol").GetComponent<LineRenderer>();
		upLift = -Physics.gravity * (2 - rb.velocity.y * 5);

		line.SetPosition(0, Vector3.zero);
		Physics.Raycast(GameObject.Find("Camera").transform.position, GameObject.Find("Camera").transform.forward, out staticHit, Mathf.Infinity);
    }

    // Update is called once per frame
    void Update(){
    	if(hit.collider != null && hit.collider.gameObject.GetComponent<PhysicsRestrictions>() != null){
			physRestricts = hit.collider.gameObject.GetComponent<PhysicsRestrictions>();
    	}

    	if(pauseGame == false){
    		if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0){
    			walkType = -1;
    		}else{
    			walkType = 0;
    		}

	    	if(Input.GetKey(KeyCode.LeftShift)){
	    		walkType = 1;
	    	}else if(Input.GetKeyUp(KeyCode.LeftShift)){
	    		walkType = 0;
	    	}

	    	if(Input.GetKey(KeyCode.LeftControl)){
	    		walkType = 2;
	    		GameObject.Find("Camera").transform.localPosition = new Vector3(0, 0, 0);
	    		GetComponent<CapsuleCollider>().height = 2;
	    		GetComponent<CapsuleCollider>().center = new Vector3(0, -0.5f, 0);

	    		transform.GetChild(0).GetComponent<CapsuleCollider>().height = 1.7f;
	    		transform.GetChild(0).GetComponent<CapsuleCollider>().center = new Vector3(0, -0.3f, 0);


	    	}else if(Input.GetKeyUp(KeyCode.LeftControl)){
	    		walkType = 0;
	    		GameObject.Find("Camera").transform.localPosition = new Vector3(0, 1, 0);
	    		GetComponent<CapsuleCollider>().height = 3;
	    		GetComponent<CapsuleCollider>().center = new Vector3(0, 0, 0);

	    		transform.GetChild(0).GetComponent<CapsuleCollider>().height = 2.7f;
	    		transform.GetChild(0).GetComponent<CapsuleCollider>().center = new Vector3(0, 0.2f, 0);
	    	}

	    	if(Input.GetKeyDown(KeyCode.Space)){
	    		jump();
	    	}

	    	if(Input.GetKeyDown(KeyCode.C)){//Toggle flight
	    		if(allowFlight == true){
		    		if(flying == true){//disable
		    			flying = false;
		    			setFlight();
		    		}else if(flying == false){//enable
		    			flying = true;
		    			setFlight();
		    		}
	    		}
	    	}

	    	if(Input.GetKeyDown(KeyCode.LeftShift) && toggleHold == false){
	    		ToggleSit("unsit");
	    	}

	    	if(Input.GetKeyDown(KeyCode.E) && toggleHold == false){
	    		ToggleSit("toggle");

	    		if(hit.collider.gameObject.GetComponent<Steering>() != null){
	    			if(physRestricts.hinge == true){
		    			int hingeDirection = hit.collider.gameObject.GetComponent<Steering>().steerDir;
		    			if(hingeDirection == 1){
		    				hingeDirection = 2;
		    			}else if(hingeDirection == 2){
		    				hingeDirection = 1;
		    			}
		    			hit.collider.gameObject.GetComponent<Steering>().steerDir = hingeDirection;
	    			}
	    		}
	    		if(hit.collider.gameObject.GetComponent<Door>() != null){
	    			doorScript = hit.collider.gameObject.GetComponent<Door>();
	    			if(doorScript.doorTypeText == "teleport"){
	    				rb.position = doorScript.destination.transform.position + new Vector3(0, 2, 0);
	    				//transform.localRotation = Quaternion.AngleAxis(doorScript.destination.transform.rotation.y, transform.up);
	    			}else if(doorScript.doorTypeText == "changescene"){
	    				doorScript.setScene();
	    			}else if(doorScript.doorTypeText == "openable"){
	    				doorScript.ToggleState();
	    			}
	    		}
	    	}
	    	if(sitting){
				gameObject.transform.position = chairObject.transform.position + new Vector3(0, 1, 0);
	    	}
			scrollDestination();
    	}
    	if(gameObject.transform.position.y <= -30){
	        spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
	        rb.velocity = new Vector3(0, 0, 0);
		   	gameObject.transform.position = spawnPoint;
		}
    }

    void FixedUpdate() {
    	if(pauseGame == false && allowMovement == true){
    		destPoint.rotation = Quaternion.Euler(0, 0, 0);

	    	if(walkType == 0){
				translation = Input.GetAxis("Vertical") * speedCopy; 
			   	strafe = Input.GetAxis("Horizontal") * speedCopy;
			}else if(walkType == 1){
				translation = Input.GetAxis("Vertical") * sprintCopy; 
			   	strafe = Input.GetAxis("Horizontal") * sprintCopy;
			}else if(walkType == 2){
				translation = Input.GetAxis("Vertical") * crouchCopy; 
			   	strafe = Input.GetAxis("Horizontal") * crouchCopy;
			}
		    if(walkType == 0 || walkType == 1 || walkType == 2){//WALK
			    direction = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
			    direction.x = strafe;
			    //direction.y = 0;
			    direction.z = translation;

			    //rb.velocity = transform.TransformDirection(direction);

			    Vector3 forceToAdd = (transform.TransformDirection(direction) - rb.velocity)*20;
			    rb.AddForce(new Vector3(forceToAdd.x, 0, forceToAdd.z), ForceMode.Acceleration);
			}else{
				//rb.velocity = new Vector3(0, rb.velocity.y, 0);
				//rb.velocity = Vector3.zero;
				//rb.velocity = rb.velocity;
			}

			if(allowFlight == true){
				if(flying == true) {
					speedCopy = speed;
					sprintCopy = sprint;
					crouchCopy = crouch;
					foreach(Collider col in GetComponents<Collider>()){
						col.enabled = false;
					}
					//GetComponent<Collider>().enabled = false;
					//GetComponentInChildren<Collider>().enabled = false;

					rb.velocity = (GameObject.Find("Camera").transform.forward * translation*2) + (GameObject.Find("Camera").transform.right * strafe*2);
					if((Input.GetKeyUp(KeyCode.W)) || (Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.D))){
						rb.velocity = new Vector3(0, 0, 0);
					}
					if(Input.GetKey(KeyCode.Space)){
						rb.velocity = new Vector3(rb.velocity.x, sprintCopy, rb.velocity.z);
					}else if(Input.GetKeyUp(KeyCode.Space)){
						rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
					}
				}else{
					foreach(Collider col in GetComponents<Collider>()){
						col.enabled = true;
					}
					//GetComponent<Collider>().enabled = true;
					//GetComponentInChildren<Collider>().enabled = false;
				}
			}
		}
		    if (Physics.Raycast(GameObject.Find("Camera").transform.position, GameObject.Find("Camera").transform.forward, out hit, Mathf.Infinity)){
		    	rayHitting = true;
		   		Debug.DrawRay(GameObject.Find("Camera").transform.position, GameObject.Find("Camera").transform.forward * hit.distance, Color.red);
		   		
		   		if(Input.GetMouseButton(0) && cameraVars.mouseMove == true && playerTools.weapon == 1 && toggleHold == true){
			   		line.SetPosition(1, GameObject.Find("Pistol").transform.InverseTransformPoint(hit.point));
		   		}else{
		   			line.SetPosition(1, Vector3.zero);
		   		}
		   	}else{
		   		rayHitting = false;
		   		if(Input.GetMouseButtonUp(0)){
		   			line.SetPosition(1, Vector3.zero);
		   		}
		   	}
			if(Input.GetMouseButtonDown(0)){
				Physics.Raycast(GameObject.Find("Camera").transform.position, GameObject.Find("Camera").transform.forward, out staticHit, Mathf.Infinity);
		    	destPoint.transform.localPosition = new Vector3(0, 0, hit.distance);
		    }
    }

    void ToggleSit(string typeSit){
    	if(typeSit == "toggle" || typeSit == "unsit"){
	    	if(sitting == true){
				sitting = false;
				rb.useGravity = true;
				GetComponent<CapsuleCollider>().enabled = true;
				allowMovement = true;
				rb.velocity = new Vector3(0, 0, 0);
				if(chairObject.GetComponent<Rigidbody>() != null){
					chairObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
				}
			}
		}
		if(typeSit == "toggle" || typeSit == "sit"){
			if(hit.collider != null && hit.collider.gameObject.tag == "Sittable"){
				sitting = true;
				chairObject = hit.collider.gameObject;

				GetComponent<CapsuleCollider>().enabled = false;
				rb.useGravity = false;
				allowMovement = false;
			}
		}
    }

    void scrollDestination(){
    	if(toggleHold == true){
	    	if(Input.GetAxis("Mouse ScrollWheel") < 0){
			   	scrollDist -= scrollSpeed;
			}
		    if(Input.GetAxis("Mouse ScrollWheel") > 0){
			   	scrollDist += scrollSpeed;
			}
			scrollDist = Mathf.Clamp(scrollDist, scrollMin, scrollMax);
			destPoint.transform.localPosition = new Vector3(0, 0, scrollDist);
		}
    }

    void jump(){
    	if(collisionCount >= 3){//Exclude the 2 colliders on the player body colliding constantly
    		landed = true;
    	}
    	if(landed == true){
    		rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);

    		jumpWalkType = walkType;

	    	landed = false;
    		if(jumpWalkType == 1){
    			sprintCopy += jumpSpeedIncrease;
    		}else if(jumpWalkType == 0){
    			speedCopy += jumpSpeedIncrease;
    		}
    		if(flying != true){
	    		if(Mathf.Abs(rb.velocity.x) <= 0 || Mathf.Abs(rb.velocity.z) <= 0){
	    			speedCopy = 2;
	    			sprintCopy = 2;
	    			crouchCopy = 2;
	    		}
    		}
	    }
    }
    
    void OnCollisionEnter(Collision col){
    	collisionCount++;

		speedCopy = speed;
		sprintCopy = sprint;
		crouchCopy = crouch;
		if(col.gameObject.layer != gameObject.layer){//Stop self collisions
    		landed = true;
    	}
    }
    void OnCollisionExit(){
    	collisionCount--;
    	if(collisionCount == 0){
    		landed = false;
    	}
    }

    void setFlight() {
    	if(flying == true){
    		rb.useGravity = false;
    	}else if (flying == false){
    		rb.useGravity = true;
    	}
    }


    void OnTriggerStay(Collider col){
    	if(col.gameObject.tag == "Water"){
    		rb.AddForceAtPosition(upLift/2, transform.position);
    	}
    }
}