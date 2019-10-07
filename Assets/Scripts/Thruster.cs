using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{

	Rigidbody parentRb;
    // Start is called before the first frame update
    void Start()
    {
        parentRb = this.transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)){
        	//parentRb.AddForceAtPosition(Vector3.forward * 100, transform.position);
        	parentRb.AddForceAtPosition(transform.TransformDirection (-Vector3.forward)*500, transform.position);
        }else if(Input.GetKey(KeyCode.DownArrow)){
        	//parentRb.AddForceAtPosition(-Vector3.forward * 100, transform.position);
        	parentRb.AddForceAtPosition(transform.TransformDirection (Vector3.forward)*500, transform.position);
        }
    }
}
