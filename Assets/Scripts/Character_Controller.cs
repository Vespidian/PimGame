using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
	Rigidbody rb;

	Vector3 direction;
	public Transform destPoint;
	public float speed = 6.0f;
	public float sprint = 10.0f;
	public float crouch = 4.0f;
	public float scrollSpeed = 0.3f;
	public float scrollDist = 5.0f;
	public float scrollMin = 2;
	public float scrollMax = 60;
	public float jumpHeight = 5.0f;
	public RaycastHit hit;

	/*GameObject weldSelect1;
	GameObject weldSelect2;
	private int selectObject = 0;*/

	public int pokeForce = 40;
	private int walkType;//0 = walk / 1 = sprint / 2 = crouch
	private bool landed;

	float translation;
	float strafe;
	
    // Start is called before the first frame update
    void Start()
    {
    	walkType = 0;
		rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKey(KeyCode.LeftShift)){
    		walkType = 1;
    	}else if(Input.GetKeyUp(KeyCode.LeftShift)){
    		walkType = 0;
    	}

    	if(Input.GetKey(KeyCode.LeftControl)){
    		walkType = 2;
    	}else if(Input.GetKeyUp(KeyCode.LeftControl)){
    		walkType = 0;
    	}

    	if(Input.GetKeyDown(KeyCode.Space)){
    		if(landed == true){
    			jump();
    		}
    		landed = false;
    	}
    }

    void FixedUpdate() {
	   if(walkType == 0){
		    translation = Input.GetAxis("Vertical") * speed; 
		    strafe = Input.GetAxis("Horizontal") * speed;
		   
		    direction = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
		    direction.z = translation;
		    direction.x = strafe;
		    rb.velocity = transform.TransformDirection(direction);
		}
		if(walkType == 1) {
			translation = Input.GetAxis("Vertical") * sprint; 
		    strafe = Input.GetAxis("Horizontal") * sprint;
		   
		    direction = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
		    direction.z = translation;
		    direction.x = strafe;
			rb.velocity = transform.TransformDirection(direction);
		}
		if(walkType == 2) {
			translation = Input.GetAxis("Vertical") * crouch; 
		    strafe = Input.GetAxis("Horizontal") * crouch;
		   
		    direction = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
		    direction.z = translation;
		    direction.x = strafe;
			rb.velocity = transform.TransformDirection(direction);
		}

	    	if (Physics.Raycast(GameObject.Find("Camera").transform.position, GameObject.Find("Camera").transform.forward, out hit, Mathf.Infinity)){
	    		Debug.DrawRay(GameObject.Find("Camera").transform.position, GameObject.Find("Camera").transform.forward * hit.distance, Color.red);
	    	}
		if(Input.GetMouseButtonDown(0)){
	    	destPoint.transform.localPosition = new Vector3(0, 0, hit.distance);
	    	/*if(playerTools.weapon == 3){
				if(selectObject == 0){
					selectObject++;
					weldSelect1 = hit.transform.gameObject;
				}
				if(selectObject == 1){
					selectObject++;
					weldSelect2 = hit.transform.gameObject;
				}
				if(selectObject == 2){
					selectObject -= 2;

				}
			}*/
	    }
    }

    void jump(){
    	//rb.velocity = new Vector3(0, jumpHeight, 0);
    	rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision col){
    	landed = true;
    }

}