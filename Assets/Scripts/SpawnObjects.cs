using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjects : MonoBehaviour
{

	//private DontFall respawn;

	public GameObject prefab1;
	public GameObject prefab2;
	public GameObject prefab3;
	public GameObject prefab4;
	public GameObject prefab5;
	public GameObject prefab6;
	public GameObject prefab7;
	public Vector3 spawnPoint = new Vector3(0, 10, 0);
	public int coordRange = 5;
	
	//SCRIPTS
	private Character_Controller thePlayer;

    // Start is called before the first frame update
    void Start()
    {
    	thePlayer = GameObject.Find("Player").GetComponent<Character_Controller>();
    	//respawn = GameObject.Find("Player").GetComponent<DontFall>();   
    }

    // Update is called once per frame
    void Update()
    {
    	/*if(thePlayer.rayHitting == true){
	    	
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
    	}*/
    }

    public void randomizeSpawn() {
    	spawnPoint.x = Random.Range(-coordRange, coordRange);
	    spawnPoint.z = Random.Range(-coordRange, coordRange);
        spawnPoint = thePlayer.hit.point + new Vector3(spawnPoint.x, 1, spawnPoint.z);
    }

    public void SpawnPrefab1(){
    	randomizeSpawn();
	    Instantiate(prefab1, spawnPoint, Quaternion.identity);
    }
    public void SpawnPrefab2(){
    	randomizeSpawn();
	    Instantiate(prefab2, spawnPoint, Quaternion.identity);
    }
    public void SpawnPrefab3(){
    	randomizeSpawn();
	    Instantiate(prefab3, spawnPoint, Quaternion.identity);
    }
    public void SpawnPrefab4(){
    	randomizeSpawn();
	    Instantiate(prefab4, spawnPoint, Quaternion.identity);
    }
    public void SpawnPrefab5(){
    	randomizeSpawn();
	    Instantiate(prefab5, spawnPoint, Quaternion.identity);
    }
    public void SpawnPrefab6(){
    	randomizeSpawn();
	    Instantiate(prefab6, spawnPoint, Quaternion.identity);
    }
    /*public void SpawnPrefab7(){
    	randomizeSpawn();
	    Instantiate(prefab7, spawnPoint, Quaternion.identity);
    }*/
}
