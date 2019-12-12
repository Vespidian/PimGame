using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
	public int steerDir = 1;
	HingeJoint steeringJoint;
	JointSpring steeringSpring;
    // Start is called before the first frame update
    void Start()
    {
    	steeringJoint = GetComponent<HingeJoint>();
    	steeringSpring = steeringJoint.spring;
    }

    // Update is called once per frame
    void Update()
    {
        steeringSpring.targetPosition = 0;
        if(steerDir == 1){
	        if(Input.GetKey(KeyCode.RightArrow)){
	        	steeringSpring.targetPosition = -30;
	        }else if(Input.GetKey(KeyCode.LeftArrow)){
	        	steeringSpring.targetPosition = 30;
	        }
    	}else if(steerDir == 2){
	        if(Input.GetKey(KeyCode.RightArrow)){
	        	steeringSpring.targetPosition = 30;
	        }else if(Input.GetKey(KeyCode.LeftArrow)){
	        	steeringSpring.targetPosition = -30;
	        }
    	}
        steeringJoint.spring = steeringSpring;
    }
}
