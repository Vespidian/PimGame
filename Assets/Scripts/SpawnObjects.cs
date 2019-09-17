using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{

	private DontFall respawn;

	public GameObject prefab1;
	public GameObject prefab2;
	public GameObject prefab3;
	public GameObject prefab4;
	public Vector3 spawnPoint = new Vector3(0, 10, 0);
	public int coordRange = 5;
	private Character_Controller thePlayer;


    // Start is called before the first frame update
    void Start()
    {
    	thePlayer = GameObject.Find("Player").GetComponent<Character_Controller>();
    	respawn = GameObject.Find("Player").GetComponent<DontFall>();   
    }

    // Update is called once per frame
    void Update()
    {
    	if(thePlayer.rayHitting == true){
	    	
	        if(Input.GetKeyDown(KeyCode.Alpha1)){
	        	randomizeSpawn();
	        	Instantiate(prefab1, spawnPoint, Quaternion.identity);
	        }else if(Input.GetKeyDown(KeyCode.Alpha2)){
	        	randomizeSpawn();
	        	Instantiate(prefab2, spawnPoint, Quaternion.identity);
	        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
	        	randomizeSpawn();
	        	Instantiate(prefab3, spawnPoint, Quaternion.identity);
	        }else if(Input.GetKeyDown(KeyCode.Alpha4)){
	        	randomizeSpawn();
	        	Instantiate(prefab4, spawnPoint, Quaternion.identity);
	        }
    	}
    }
    public void randomizeSpawn() {
    	spawnPoint.x = Random.Range(-coordRange, coordRange);
	    spawnPoint.z = Random.Range(-coordRange, coordRange);
        spawnPoint = thePlayer.hit.point + new Vector3(spawnPoint.x, 1, spawnPoint.z);
    }
}
