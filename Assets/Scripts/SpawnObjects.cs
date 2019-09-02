using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{

	public DontFall respawn;

	public GameObject prefab1;
	public GameObject prefab2;
	public GameObject prefab3;
	public GameObject prefab4;
	public Vector3 spawnPoint = new Vector3(0, 10, 0);

    // Start is called before the first frame update
    void Start()
    {
    	respawn = GameObject.Find("Player").GetComponent<DontFall>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
        	Instantiate(prefab1, spawnPoint, Quaternion.identity);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
        	Instantiate(prefab2, spawnPoint, Quaternion.identity);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
        	Instantiate(prefab3, spawnPoint, Quaternion.identity);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
        	Instantiate(prefab4, spawnPoint, Quaternion.identity);
        }
    }
}
