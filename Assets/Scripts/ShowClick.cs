using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowClick : MonoBehaviour
{

	private Character_Controller thePlayer;

    // Start is called before the first frame update
    void Start()
    {
		thePlayer = GameObject.Find("Player").GetComponent<Character_Controller>(); 
    }

    // Update is called once per frame
    void Update()
    {
    	transform.position = thePlayer.hit.point;
        if(Input.GetMouseButton(0)){
        	transform.localScale = new Vector3(2, 2, 2);
        }else if(Input.GetMouseButtonUp(0)) {
        	transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
