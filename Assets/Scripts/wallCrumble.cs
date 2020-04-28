using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallCrumble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	for(int i = 0; i < transform.childCount; i++){
    		transform.GetChild(i).GetComponent<Rigidbody>().Sleep();
    	}    
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
