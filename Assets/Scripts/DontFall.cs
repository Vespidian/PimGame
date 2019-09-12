using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontFall : MonoBehaviour
{

	public enum RespawnTypes { FallForever = 0, RespawnPoint = 1};
	public RespawnTypes types = RespawnTypes.RespawnPoint;
	public float spawnHeight = 100;
	public float lowestPossiblePoint = -10;
	Rigidbody rb;
	private SpawnObjects rando;
	public Vector3 spawnPoint = new Vector3(0, 10, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rando = GameObject.Find("Player").GetComponent<SpawnObjects>();

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y <= lowestPossiblePoint){
        	if(types == RespawnTypes.FallForever){

	   			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+spawnHeight, gameObject.transform.position.z);

        	}else if(types == RespawnTypes.RespawnPoint) {

        		rando.randomizeSpawn();
        		spawnPoint = new Vector3(rando.spawnPoint.x, 10, rando.spawnPoint.z);
        		rb.velocity = new Vector3(0, 0, 0);
	   			gameObject.transform.position = spawnPoint;
	   		}
	    }
    }
}
