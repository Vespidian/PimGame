using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontFall : MonoBehaviour
{

	public enum RespawnTypes { FallForever = 0, RespawnPoint = 1};
	public RespawnTypes types = RespawnTypes.RespawnPoint;
	public float spawnHeight = 100;
	public float lowestPossiblePoint = -10;
	public Vector3 spawnPoint = new Vector3(0, 10, 0);
	Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if(gameObject.transform.position.y <= lowestPossiblePoint){
        	if(types == RespawnTypes.FallForever){
	   			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+spawnHeight, gameObject.transform.position.z);
        	}else if(types == RespawnTypes.RespawnPoint) {
        		rb.velocity = new Vector3(0, 0, 0);
	   			gameObject.transform.position = spawnPoint;
	   		}
	   }
    }
}
