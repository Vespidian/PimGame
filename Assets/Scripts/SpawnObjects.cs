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
	private CharController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
    	thePlayer = GameObject.Find("Player").GetComponent<CharController>();
    	//respawn = GameObject.Find("Player").GetComponent<DontFall>();   
    }

    // Update is called once per frame
    void Update()
    {
    	/*if(thePlayer.rayHitting == true){
	    	if(Input.GetKeyDown(KeyCode.Alpha1)){
                Instantiate(prefab1, spawnPoint, Quaternion.identity);
            }
	        if(Input.GetKeyDown(KeyCode.Alpha1)){
	        	Instantiate(prefab1, spawnPoint, Quaternion.identity);
	        }else if(Input.GetKeyDown(KeyCode.Alpha2)){
	        	Instantiate(prefab2, spawnPoint, Quaternion.identity);
	        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
	        	Instantiate(prefab3, spawnPoint, Quaternion.identity);
	        }else if(Input.GetKeyDown(KeyCode.Alpha4)){
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
	    SpawnObject(prefab1);
    }
    public void SpawnPrefab2(){
	    SpawnObject(prefab2);
    }
    public void SpawnPrefab3(){
	    SpawnObject(prefab3);
    }
    public void SpawnPrefab4(){
	    SpawnObject(prefab4);
    }
    public void SpawnPrefab5(){
	    SpawnObject(prefab5);
    }
    public void SpawnPrefab6(){
	    SpawnObject(prefab6);
    }
    /*public void SpawnPrefab7(){
	    SpawnObject(prefab7);
    }*/

    public void SpawnObject(GameObject spawn){
        Instantiate(spawn, thePlayer.hit.point + (thePlayer.hit.normal + Vector3.up)*1, Quaternion.identity);
        thePlayer.numberOfItems++;
        Debug.Log(thePlayer.numberOfItems);
    }
}
