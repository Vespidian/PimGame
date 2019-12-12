using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontFall : MonoBehaviour
{

	public enum RespawnTypes { FallForever = 0, RespawnPoint = 1};
	public RespawnTypes types = RespawnTypes.RespawnPoint;
	public float spawnHeight = 100;
	private Rigidbody rb;
	private Vector3 spawnPoint = new Vector3(0, 20, 0);

	//SCRIPTS
	private CharController thePlayer;
	private SpawnObjects spawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        thePlayer = GameObject.Find("Player").GetComponent<CharController>();

        spawnPoint = thePlayer.objSpwnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y <= thePlayer.lowestPossiblePoint){
        	if(types == RespawnTypes.FallForever){

	   			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+spawnHeight, gameObject.transform.position.z);

        	}else if(types == RespawnTypes.RespawnPoint) {

        		RandoSpawn();
        		spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
        		rb.velocity = new Vector3(0, 0, 0);
	   			gameObject.transform.position = spawnPoint;
	   		}
	    }
    }
    void RandoSpawn() {
	    spawnPoint.x = Random.Range(-thePlayer.objSpawnRange, thePlayer.objSpawnRange);
	    spawnPoint.z = Random.Range(-thePlayer.objSpawnRange, thePlayer.objSpawnRange);
    }
}
